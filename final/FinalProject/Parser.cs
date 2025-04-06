using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

using System.Threading.Tasks;
using System.Text.Json.Serialization.Metadata;

public class Parser
{
	private static string token = Environment.GetEnvironmentVariable("CANVAS_API_TOKEN");
	private static string baseUrl = "https://byui.instructure.com/api/v1";

	private static JsonSerializerOptions ops = new JsonSerializerOptions {};

	public static async Task<List<Course>> FetchCourses(bool debug = false, bool excludeNullScoreClass = true)
	{
		List<Course> courses = new List<Course>();

		using HttpClient client = new HttpClient();
		client.BaseAddress = new Uri(baseUrl);
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		// Construct URL with parameters
		string key1 = "enrollment_state";
		string value1 = "active";
		string key2 = "include[]";
		string value2 = "total_scores";
		string url = $"{baseUrl}/courses?{Uri.EscapeDataString(key1)}={Uri.EscapeDataString(value1)}&{key2}={value2}";

		HttpResponseMessage response = await client.GetAsync(url);
		Console.WriteLine(response.StatusCode);

		if (response.IsSuccessStatusCode)
		{
			Debug.WriteLine("API Request successful...");
			string responseBody = await response.Content.ReadAsStringAsync();

			using JsonDocument doc = JsonDocument.Parse(responseBody);
			JsonElement root = doc.RootElement;

			// Loop over each course object in the array
			foreach (JsonElement courseElement in root.EnumerateArray())
			{
				// Get the course id and name (assuming they are always present)
				string courseId = courseElement.GetProperty("id").ToString();
				string courseName = courseElement.GetProperty("name").ToString();

				// Default score value in case computed_final_score is null or not available
				double courseScore = -1.0;

				// Look for enrollments property
				if (courseElement.TryGetProperty("enrollments", out JsonElement enrollmentsElement))
				{
					// Assume we want the first enrollment's computed_final_score
					if (enrollmentsElement.GetArrayLength() > 0)
					{
						JsonElement enrollment = enrollmentsElement[0];
						if (enrollment.TryGetProperty("computed_final_score", out JsonElement scoreElement))
						{
							if (scoreElement.ValueKind != JsonValueKind.Null &&
								scoreElement.TryGetDouble(out double score))
							{
								courseScore = score;
							}
							else if (debug)
							{
								Debug.WriteLine("computed_final_score is null or not a valid number.");
							}
						}
					}
				}

				// Create a new Course instance with the extracted values
				if (excludeNullScoreClass && courseScore > 0)
				{
					Course course = new Course(courseName, courseScore, courseId);

					courses.Add(course);
					if (debug)
					{
						Debug.WriteLine(course.ToString());
					}
				}

			}

			return courses;
		}
		else
		{
			Console.WriteLine($"Request failed: {response.StatusCode}");
			string errorDetails = await response.Content.ReadAsStringAsync();
			Console.WriteLine(errorDetails);
			throw new Exception("API error: " + errorDetails);
		}
	}

	public static async Task<List<Assignment>> FetchAssignments(string id)
	{
		List<Assignment> assignmentList = [];

		List<Course> courses = new List<Course>();

		using HttpClient client = new HttpClient();
		client.BaseAddress = new Uri(baseUrl);
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		// Construct URL with parameters
		string url = $"{baseUrl}/courses/{id}/assignments";

		HttpResponseMessage response = await client.GetAsync(url);
		Console.WriteLine(response.StatusCode);

		if (response.IsSuccessStatusCode)
		{
			Debug.WriteLine("API Request successful...");
			string responseBody = await response.Content.ReadAsStringAsync();

			using JsonDocument doc = JsonDocument.Parse(responseBody);
			JsonElement root = doc.RootElement;

			List<JsonObject> assignmentArray = [];
			foreach (JsonElement elem in root.EnumerateArray())
			{

				string assignmentId = elem.GetProperty("id").ToString();
				string assignmentName = elem.GetProperty("name").ToString();
				string assignmentPts = elem.GetProperty("points_possible").ToString();
				
				assignmentList.Add(new Assignment(assignmentId, assignmentName, assignmentPts)); 
				

			}
			foreach (JsonObject assignment in assignmentArray)
			{
			}


		}



		return assignmentList;
	}

}
