namespace Homeworktriage;

public class Student
{
    private string name;
    private float gpa;

    private List<Course> courses = new List<Course>();
    private List<Assignment> assignments = new List<Assignment>();

    public IReadOnlyList<Course> Courses => courses.AsReadOnly();
    public IReadOnlyList<Assignment> Assignments => assignments.AsReadOnly();

    public Student(string name, float gpa)
    {
        this.name = name;
        this.gpa = gpa;
    }

    public void AddCourse(Course course) => courses.Add(course);
    public void RemoveCourse(Course course) => courses.Remove(course);

    public void AddAssignment(Assignment assignment) => assignments.Add(assignment);
    public void RemoveAssignment(Assignment assignment) => assignments.Remove(assignment);
}