using System;
using System.IO;

class Journal
{
    public string _journalName;
    public string _journalFile;

    public Journal(string name, string fileDirectory)
    {
        _journalName = name;
        _journalFile = $"{fileDirectory}/{name}.journal";
        if (!File.Exists(_journalFile))
        {
            File.Create(_journalFile).Dispose();
        }
    }

    public static string[] getJournalsInDir()
    {
        //store current directory
        string[] journalFiles =  Directory.GetFiles(Directory.GetCurrentDirectory(),"*.journal");
        return journalFiles;
    }

    public static int countEntries()
    {
        //count number of entries in journal
        return 0;
    }

    public void saveEntry(Entry entry)
    {
        //save entry to journal file
        StreamWriter sw = new StreamWriter( );
    }

}