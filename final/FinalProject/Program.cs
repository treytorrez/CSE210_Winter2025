#nullable enable

using System.Diagnostics;
using groupDict = System.Collections.Generic.Dictionary<string, System.Tuple<string, float>>;
using System.Data;
using Homeworktriage.Parsers;
using System.Diagnostics.CodeAnalysis;

namespace Homeworktriage
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to HomeworkTriage!");

            string? canvasApiToken = Environment.GetEnvironmentVariable("CANVAS_API_TOKEN");
            if (string.IsNullOrEmpty(canvasApiToken))
            {
                Console.WriteLine("No API key detected! Please save your API key to the environment variable 'CANVAS_API_TOKEN'.");
                return;
            }
            Debug.WriteLine($"API key found");

            Console.WriteLine("Enter your name: "); //TODO: Add GPA input and use the name and GPA somewhere
            Student student = new Student(Console.ReadLine() ?? "Student", 4.0f); 

            try
            {
                List<Course> fetchedCourses = await CourseParser.FetchCourses(debug: true);
                fetchedCourses = fetchedCourses.Where(course => course.Grade >= 0).ToList();
                fetchedCourses.ForEach(course => student.AddCourse(course));

                foreach (var course in student.Courses)
                {
                    var fetchedGroups = await AssignmentGroupParser.FetchAssignmentGroups(course.Id);
                    fetchedGroups.ToList().ForEach(group => course.AddAssignmentGroup(group.Key, group.Value));

                    var fetchedAssignments = await AssignmentParser.FetchAssignments(course.Id);
                    fetchedAssignments = fetchedAssignments.Where(assignment => assignment.pointValue != 0).ToList();
                    fetchedAssignments.ForEach(assignment => course.AddAssignment(assignment));
                    fetchedAssignments.ForEach(assignment => student.AddAssignment(assignment));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return;
            }

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Display Classes");
                Console.WriteLine("2. Display Assignment Groups for Each Class");
                Console.WriteLine("3. Display Assignments in Each Class");
                Console.WriteLine("4. Display Point Totals for Each Category");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\nClasses:");
                        foreach (var course in student.Courses)
                        {
                            Console.WriteLine(course);
                        }
                        break;

                    case "2":
                        Console.WriteLine("\nAssignment Groups:");
                        foreach (var course in student.Courses)
                        {
                            Console.WriteLine($"Course: {course.Name}");
                            foreach (var group in course.AssignmentGroups)
                            {
                                Console.WriteLine($"  Group: {group.Value.Item1}, Weight: {group.Value.Item2}%");
                            }
                        }
                        break;

                    case "3":
                        Console.WriteLine("\nAssignments:");
                        foreach (var course in student.Courses)
                        {
                            Console.WriteLine($"Course: {course.Name}");
                            foreach (var assignment in course.Assignments)
                            {
                                Console.WriteLine($"  {assignment}");
                            }
                        }
                        break;

                    case "4":
                        Console.WriteLine("\nPoint Totals for Each Category:");
                        foreach (Course course in student.Courses)
                        {
                            Console.WriteLine($"Course: {course.Name}");
                            var categoryPointTotals = course.Assignments
                                .GroupBy(assignment => assignment.groupId)
                                .ToDictionary(
                                    group => group.Key,
                                    group => group.Sum(assignment => assignment.pointValue)
                                );

                            foreach (var group in course.AssignmentGroups)
                            {
                                string categoryId = group.Key;
                                foreach (Assignment assignment in course.Assignments.Where(a => a.groupId == categoryId))
                                {
                                    assignment.PercentFinalGrade = (
                                        assignment.pointValue / categoryPointTotals[assignment.groupId]) *
                                        (course.AssignmentGroups[assignment.groupId].Item2 / 100);
                                }
                                if (categoryPointTotals.TryGetValue(categoryId, out float totalPoints))
                                {
                                    Console.WriteLine($"  Group: {group.Value.Item1}, Total Points: {totalPoints}");
                                }
                            }
                        }
                        break;

                    case "5":
                        Console.WriteLine("Exiting program. Goodbye!");
                        return;

                    case "6":
                        Console.WriteLine("\nRanked Assignments:");
                        Prioritizer prioritizer = new Prioritizer();
                        PriorityQueue<Assignment, float> rankedAssignments = prioritizer.RankAssignments(student.Assignments.ToList());
                        if (student.Assignments.Count == 0)
                        {
                            Console.WriteLine("No assignments found.");
                            break;
                        }
                        Console.WriteLine($"Total Assignments: {student.Assignments.Count}");
                        Console.WriteLine($"Ranked Assignments (Top 10):");
                        for (int i = 0; i < Math.Min(10, student.Assignments.Count); i++)
                        {
                            Assignment assignment = rankedAssignments.TryDequeue(out assignment, out float priority) ? assignment : null;
                            Console.WriteLine($"Rank {i + 1}: {assignment.name} {assignment.PercentFinalGrade * 100}%  || priority {priority}");
                        }
                        break;

                    case "7":
                        Console.WriteLine("\nAssignment Percent of Final Grade:");
                        foreach (var course in student.Courses)
                        {
                            Console.WriteLine($"Course: {course.Name}");
                            foreach (var assignment in course.Assignments)
                            {
                                Console.WriteLine($"  {assignment.name}: {assignment.PercentFinalGrade * 100}%");
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
