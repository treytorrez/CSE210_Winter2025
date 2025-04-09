using System.Diagnostics;
using System.Text.Json;

namespace Homeworktriage.Parsers;

public class AssignmentGroupParser : Parser
{
    // Fetch assignment groups for a specific course
    public static async Task<Dictionary<string, Tuple<string, float>>> FetchAssignmentGroups(string courseId)
    {
        Dictionary<string, Tuple<string, float>> fetchedGroups = new Dictionary<string, Tuple<string, float>>();
        string url = $"{BaseUrl}/courses/{courseId}/assignment_groups?per_page=20";

        try
        {
            Debug.WriteLine($"[DEBUG] Fetching assignment groups for course ID: {courseId}.");
            HttpResponseMessage response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(responseBody);
            JsonElement root = doc.RootElement;

            foreach (JsonElement element in root.EnumerateArray())
            {
                string groupId = element.GetProperty("id").ToString();
                string groupName = element.GetProperty("name").ToString();
                float groupGradeWeight = float.Parse(element.GetProperty("group_weight").ToString());

                fetchedGroups.Add(groupId, new Tuple<string, float>(groupName, groupGradeWeight));
                Debug.WriteLine($"[DEBUG] Added assignment group: {groupName} with weight: {groupGradeWeight}%");
            }

            Debug.WriteLine("[DEBUG] Successfully fetched and processed assignment groups.");
            return fetchedGroups;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[ERROR] FetchAssignmentGroups: {ex.Message}");
            throw;
        }
    }
}