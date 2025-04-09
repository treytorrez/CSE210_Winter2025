using System;
using System.Collections.Generic;
using System.Linq;

namespace Homeworktriage
{
    public class Prioritizer
    {

        PriorityQueue<Assignment, float> assignmentWeights = new PriorityQueue<Assignment, float>(); // assignment object, weight of assignment
        public PriorityQueue<Assignment, float> RankAssignments(List<Assignment> assignments)
        {
            // Placeholder logic for ranking assignments based on due date and grade impact.
            foreach(Assignment a in assignments)
            {
                float weight = 0.0f;
                double hoursTillDue = a.DueDate.HasValue 
                                    ? (a.DueDate.Value - DateTime.Now).TotalHours 
                                    : double.NegativeInfinity; // I want to use this in the future
                double daysTillDue = a.DueDate.HasValue 
                                    ? (a.DueDate.Value - DateTime.Now.AddDays(-45)).TotalDays 
                                    : double.NegativeInfinity; // Calculate time until due date
                if (a.PercentFinalGrade.HasValue)
                {
                    weight = a.PercentFinalGrade * 100 ?? float.MinValue; // Assign weight based on grade impact
                }
                if (daysTillDue > 0)
                {
                    // weight = (float)(1.0 / Math.Pow(daysTillDue, 2) + 1);
                    weight *= (float)(-.1 * daysTillDue * daysTillDue + 30); // Assign weight based on urgency (closer due dates get higher weight) 
                    // Assign weight based on urgency (closer due dates get higher weight)
                }
                else
                {
                    weight = float.MinValue; 
                    // Assign a very low weight for overdue assignments
                    // a very high value could also be used to prioritize overdue assignments
                    // but this is a sort of placeholder for now
                    //TODO: make this a user settable value; maybe a config file or something
                    
                }
                assignmentWeights.Enqueue(a, -weight);
            } // Avoid division by zero
            
            return assignmentWeights;
        }

        public void SetCustomWeights(Dictionary<string, float> weights)
        {
            // Placeholder for user-defined weight adjustments.
        }

        public void CalculateAssignmentWeights(Course course)
        {
            if (course.Assignments == null || course.AssignmentGroups == null)
                return;

            // Calculate total points for each category
            var categoryPointTotals = course.Assignments
                .GroupBy(a => a.groupId)
                .ToDictionary(g => g.Key, g => g.Sum(a => a.pointValue));

            // Calculate the percent each point is worth in each category
            foreach (var group in course.AssignmentGroups)
            {
                string categoryId = group.Key;
                float categoryWeight = group.Value.Item2;

                if (categoryPointTotals.TryGetValue(categoryId, out float totalPoints) && totalPoints > 0)
                {
                    float percentPerPoint = categoryWeight / totalPoints;

                    // Assign the percent value to each assignment in the category
                    foreach (var assignment in course.Assignments.Where(a => a.groupId == categoryId))
                    {
                        assignment.PercentFinalGrade = assignment.pointValue * percentPerPoint;
                    }
                }
            }
        }
    }
}