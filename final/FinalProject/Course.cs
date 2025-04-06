using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


public class Course
{
    public string Name { get; }
    public double Grade { get; }
    public string Id { get; }
    public List<Assignment> AssignmentList;

    public Course(string name, double grade, string id)
    {
        this.Name = name;
        this.Grade = grade;
        this.Id = id;
        AssignmentList = new List<Assignment>(); // Initialize the list
        _asyncAllAssignments().ConfigureAwait(false).GetAwaiter().GetResult(); // Block until assignments are fetched (less ideal for UI apps)
    }

    public override string ToString() => $"Course: {Name,-35} Grade: {Grade,-8} ID: {Id,-6}";

    private async Task _asyncAllAssignments()
    {
        this.AssignmentList = await Parser.FetchAssignments(Id);
        Debug.WriteLine(this.AssignmentList.Count + " assignments found for ID :" + this.Id);
        foreach(Assignment assn in this.AssignmentList)
        {
            Debug.WriteLine(assn);
        }

    }

    


    public System.Func<float, System.TimeSpan> latePolicy
    {
        get => default;
        set
        {
        }
    }
}
