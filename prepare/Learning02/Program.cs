using System;

class Program
{
    static void Main(string[] args)
    {
        //Create a new resume
        Resume resume = new Resume()
        //Set member variables
        {_name = "Trey Torrez",
        _jobs = new List<Job>()
        //Add jobs inside the resume declaration using an object initializer
        {
            new Job()
            {
                _company = "Microsoft",
                _jobTitle = "Software Engineer",
                _startYear = 2019,
                _endYear = 2021
            },
            new Job()
            {
                _company = "Google",
                _jobTitle = "Software Engineer",
                _startYear = 2021,
                _endYear = 2023
            }
        }
        };

        //Display the resume
        resume.Display();

    }
}