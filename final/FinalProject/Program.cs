using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

class Program
{
    public static async Task Main(string[] args)
    {
        List<Course> courses = [];



        Console.WriteLine($"Welcome to HomeworkTriage!");
        if (Environment.GetEnvironmentVariable("CANVAS_API_TOKEN") == null)
        {
            Console.WriteLine($"No API key detected! Please save API key to the environment variable to 'CANVAS_API_TOKEN");
            return;
        }
        Console.WriteLine($"Attempting to detect classes...");


        try
        {
            // Await the asynchronous fetch function to get the list of courses.
            courses = await Parser.FetchCourses(debug: true);

            // Now you can work with the list, for instance:
            Console.WriteLine($"Classes detected, classes with null scores and scores of 0 have been ignored");

            foreach (Course course in courses)
            {
                Console.WriteLine(course);
                foreach(Assignment assn in course.AssignmentList)
                {
                    System.Console.WriteLine(assn);
                }
                
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message + ex.StackTrace);
        }

        

    }

}
