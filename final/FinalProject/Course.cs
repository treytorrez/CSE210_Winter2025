using System.Diagnostics;
using System.Text.Json;

namespace Homeworktriage;

public class Course
{
    public string Name { get; private set; } // Use properties
    public double Grade { get; private set; }
    public string Id { get; private set; }
    public List<Assignment> Assignments { get; private set; }
    public Dictionary<string, Tuple<string, float>> AssignmentGroups { get; private set; } //{ID of group, (nameOfGroup, weightOfGroupz)}
    public double? PercentFinalGrade { get; private set; } // Percentage of the final grade that this course contributes to.

    public Course(string name, double grade, string id)
    {
        Name = name;
        Grade = grade;
        Id = id;
    }

    public override string ToString() => $"Course: {Name,-35} Grade: {Grade,-8} ID: {Id,-6}";

    public void AddAssignment(Assignment assignment)
    {
        if (Assignments == null)
            Assignments = new List<Assignment>();
        Assignments.Add(assignment);
    }

    public void RemoveAssignment(Assignment assignment)
    {
        Assignments?.Remove(assignment);
    }

    public void AddAssignmentGroup(string groupId, Tuple<string, float> group)
    {
        if (AssignmentGroups == null)
            AssignmentGroups = new Dictionary<string, Tuple<string, float>>();
        AssignmentGroups[groupId] = group;
    }

    public void RemoveAssignmentGroup(string groupId)
    {
        AssignmentGroups?.Remove(groupId);
    }
}