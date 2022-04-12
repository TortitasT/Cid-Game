using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog
{
    public List<string> titles = new List<string>();
    public List<string> contents = new List<string>();

    public Dialog(List<string> titles, List<string> contents)
    {
        this.titles = titles;
        this.contents = contents;
    }
}
