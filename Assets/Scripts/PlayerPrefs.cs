using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefs : MonoBehaviour
{
    public int HighestScore { get; set; }

    public PlayerPrefs()
    {
        HighestScore = 0;
    }

    public void SaveScore()
    {
        UnityEngine.PlayerPrefs.SetInt("SavedInteger", HighestScore);
    }

    public int LoadScore()
    {
        if(UnityEngine.PlayerPrefs.HasKey("SavedInteger"))
        {
            this.HighestScore = UnityEngine.PlayerPrefs.GetInt("SavedInteger");
        }
        return this.HighestScore;
    }

    public void Clear()
    {
        UnityEngine.PlayerPrefs.DeleteAll();
        this.HighestScore = 0;
    }
}
