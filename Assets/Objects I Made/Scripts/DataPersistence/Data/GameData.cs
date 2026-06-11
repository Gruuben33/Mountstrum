using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int LaunchCount;

    public GameData()
    {
        Debug.Log("workign");
        LaunchCount = 0;
    }
}
