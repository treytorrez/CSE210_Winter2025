#nullable enable
namespace Homeworktriage;
public class Assignment


{
    public string name { get; private set; }

    public string id { get; private set; }

    public float pointValue { get; private set; }

    public string groupId { get; private set; }

    public string courseId { get; private set; }

    public DateTime? DueDate { get; private set; }


    //TODO: make a class that estimates time. maybe think of a hacky way to do it? AI API call? user set times?
    // private TimeSpan? durationHours = null;
    private float? percentFinalGrade = null;

    //TODO: implement submission bool for filtering out assignments that are already completed
    // private bool? hasSubmitted = null;

    //TODO: only if you have extra time
    // private string? url = null;
    private TimeSpan? durationHours = null;
    private bool? hasSubmitted = null;
    private string? url = null;

    public TimeSpan? DurationHours { get => durationHours; private set => durationHours = value; }
    public bool? HasSubmitted { get => hasSubmitted; private set => hasSubmitted = value; }
    public string? Url { get => url; private set => url = value; }
    public float? PercentFinalGrade { get => percentFinalGrade; set => percentFinalGrade = value; }


    public Assignment(string id, string name, string points_possible, string group_category_id, string course_id, DateTime? due_at)
    {

        this.id = id;
        this.name = name.Trim();
        this.pointValue = float.Parse(points_possible);
        this.groupId = group_category_id;
        this.courseId = course_id;
        this.DueDate = due_at;

        // foreach(Course course in student)
    }

    public override string ToString()
    {
        return $"{(name.Length > 35 ? name[..35] + "..." : name.PadRight(35))} | {pointValue,-6} pts | Due(UTC): {DueDate,-21} ";  //TODO: fix UTC
    }
}