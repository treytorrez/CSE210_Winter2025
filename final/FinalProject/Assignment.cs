using System.Text.Json.Serialization;

public class Assignment
{
    
    [JsonPropertyName("name")]
    private string name; //
    
    [JsonPropertyName("id")]
    private string id; //
    
    [JsonPropertyName("points_possible")]
    private string pointValue; //
    
    [JsonPropertyName("group_category_id")]
    private string? categoryId = null; //
    
    [JsonPropertyName("course_id")]
    private Course? courseId = null; //
    
    [JsonPropertyName("due_at")]
    private System.DateTime? dueDate = null; //
    private System.TimeSpan? durationHours = null;
    private float? percentFinalGrade = null;
    
    [JsonPropertyName("has_submitted_submissions")]
    private bool? hasSubmitted = null; //
    
    [JsonPropertyName("html_url")]
    private string? url = null; //


    public Assignment(string id, string name, string points_possible)
    {

        this.id = id;
        this.name = name;
        this.pointValue = points_possible;        
    }

    public override string ToString()
    {
        return $"{id, -10}|{name, -40} |{pointValue, 6}";
    }
}