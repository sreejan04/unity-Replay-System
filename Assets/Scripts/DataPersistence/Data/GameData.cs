using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<ActionReplayRecord>[] Records;
    //public List<string> id =new List<string> { "abc", "ac" };
    public GameData()
    {
        this.Records = new List<ActionReplayRecord>[7];
    }
}
