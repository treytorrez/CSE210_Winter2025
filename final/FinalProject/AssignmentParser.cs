using System.Diagnostics;
using System.Text.Json;

namespace Homeworktriage.Parsers;

public class AssignmentParser : Parser
{
    // Fetch assignments for a specific course
    public static async Task<List<Assignment>> FetchAssignments(string courseId)
    {
        List<Assignment> assignments = new List<Assignment>();
        string url = $"{BaseUrl}/courses/{courseId}/assignments?per_page=100";

        try
        {
            Debug.WriteLine($"[DEBUG] Fetching assignments for course ID: {courseId}.");
            HttpResponseMessage response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(responseBody);
            JsonElement root = doc.RootElement;

            foreach (JsonElement element in root.EnumerateArray())
            {
                string assignmentId = element.GetProperty("id").ToString();
                string assignmentName = element.GetProperty("name").ToString();
                string pointsPossible = element.GetProperty("points_possible").ToString();
                string groupId = element.GetProperty("assignment_group_id").ToString();
                DateTime? dueDate = element
                                    .TryGetProperty("due_at", out JsonElement dueAtElement) 
                                    && dueAtElement.ValueKind != JsonValueKind.Null
                                    ? dueAtElement.GetDateTime()
                                    : null;

                assignments.Add(new Assignment(
                                                assignmentId, 
                                                assignmentName, 
                                                pointsPossible, 
                                                groupId, 
                                                courseId, 
                                                dueDate
                                                ));
                Debug.WriteLine($"[DEBUG] Added assignment: {assignmentName} with points: {pointsPossible}");
            }

            Debug.WriteLine("[DEBUG] Successfully fetched and processed assignments.");
            return assignments;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[ERROR] FetchAssignments: {ex.Message}");
            throw;
        }
    }
}