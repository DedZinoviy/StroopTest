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

    public List<string> GetScenes()
    {
        string scenesString = UnityEngine.PlayerPrefs.GetString("Scenes");

        if (scenesString.Length <= 0)
            return new List<string>();
        else
            return JsonUtility.FromJson<ScenesContainer>(scenesString).scenes;
    }

    public void SetScenes(List<string> scenes)
    {
        ScenesContainer container = new ScenesContainer();
        container.scenes = scenes;
        string str = JsonUtility.ToJson(container);
        UnityEngine.PlayerPrefs.SetString("Scenes", str);
    }

    public void ClearScenes()
    {
        UnityEngine.PlayerPrefs.DeleteKey("Scenes");
    }

    class ScenesContainer
    {
        public List<string> scenes;
    }
}
