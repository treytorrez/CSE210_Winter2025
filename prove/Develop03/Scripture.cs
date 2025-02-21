using System;
using System.Collections.Generic;

public class Scripture
{
    public Dictionary<int, string[]> _content = new Dictionary<int, string[]>();
    public ScriptureReference _reference;

    public Scripture(ScriptureReference reference)
    {
        _reference = reference;
    }

    public int GetTotalWords()
    {
        int totalWords = 0;
        foreach ( var verse in _content)
        {
            totalWords += verse.Value.Length;
        }

        return totalWords;
    }


    public Dictionary<int, string[]> GetContent()
    {
        return _content;
    }

};
