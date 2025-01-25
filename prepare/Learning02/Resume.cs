public class Resume
{
    public string _name;

    public List<Job> _jobs = new List<Job>();

    //unused func; added for the sake of the exercise
    public List<Job> addJob(Job job)
    {
        _jobs.Add(job);
        return _jobs;
    }
    public void Display()
    {
        System.Console.WriteLine($"Name: {_name}");
        System.Console.WriteLine("Jobs:");
        foreach (var job in _jobs)
        {
            //Padding for the job string; makes it look nice
            string jobStringPadding = "     ";
            System.Console.Write(jobStringPadding);
            job.Display();
        }}
}
