using System.Diagnostics;
using System.Text.Json;

namespace Homeworktriage.Parsers;

public class CourseParser : Parser
{
    // Fetch active courses with total scores included
    public static async Task<List<Course>> FetchCourses(bool debug = false, bool excludeNullScoreClass = true)
    {
        List<Course> courses = new List<Course>();
        string url = $"{BaseUrl}/courses?enrollment_state=active&include[]=total_scores";

        try
        {
            Debug.WriteLine("[DEBUG] Fetching courses from Canvas API.");
            HttpResponseMessage response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(responseBody);
            JsonElement root = doc.RootElement;
            Debug.WriteLine($"[DEBUG] JSON Root: {root.ToString()}");

            foreach (JsonElement courseElement in root.EnumerateArray())
            {
                string courseId = courseElement.GetProperty("id").ToString();
                string courseName = courseElement.GetProperty("name").ToString();
                double courseScore = -1.0;

                if (courseElement.TryGetProperty("enrollments", out JsonElement enrollmentsElement) &&
                    enrollmentsElement.GetArrayLength() > 0)
                {
                    JsonElement enrollment = enrollmentsElement[0];
                    if (enrollment.TryGetProperty("computed_final_score", out JsonElement scoreElement) &&
                        scoreElement.ValueKind != JsonValueKind.Null &&
                        scoreElement.TryGetDouble(out double score))
                    {
                        courseScore = score;
                    }
                }

                if (excludeNullScoreClass && courseScore > 0)
                {
                    courses.Add(new Course(courseName, courseScore, courseId));
                    Debug.WriteLine($"[DEBUG] Added course: {courseName} with score: {courseScore}");
                }
            }
            Debug.WriteLine($"[DEBUG] Returning {courses.Count} courses.");
            return courses;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[ERROR] FetchCourses: {ex.Message}");
            throw;
        }
    }
}