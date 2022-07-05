using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for working with player preferences and other stored information.
/// </summary>
public class PlayerPrefs : MonoBehaviour
{
    /// <summary>
    /// The highest game score.
    /// </summary>
    public int HighestScore { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public PlayerPrefs()
    {
        HighestScore = 0;
    }

    /// <summary>
    /// Saves score in log.
    /// </summary>
    public void SaveScore()
    {
        UnityEngine.PlayerPrefs.SetInt("SavedInteger", HighestScore); // ��������� ���������� � �����.
    }

    /// <summary>
    /// Loads the score from log.
    /// </summary>
    /// <returns>Contained score.</returns>
    public int LoadScore()
    {
        if (UnityEngine.PlayerPrefs.HasKey("SavedInteger")) // �������� ���������� � �����, ���� ������� ����������.
        {
            this.HighestScore = UnityEngine.PlayerPrefs.GetInt("SavedInteger");
        }
        return this.HighestScore; // ������� ���������� ����������.
    }

    /// <summary>
    /// Clears all info in log.
    /// </summary>
    public void Clear()
    {
        UnityEngine.PlayerPrefs.DeleteAll(); // ������� ��� ���������� � ����.
        this.HighestScore = 0; // ������� ��� ���������� � ���������� � ������.
    }

    /// <summary>
    /// Returns scenes from the log as a scene container.
    /// </summary>
    /// <returns>List of scenes.</returns>
    public List<string> GetScenes()
    {
        string scenesString = UnityEngine.PlayerPrefs.GetString("Scenes"); //�������� ������ � ��������� ����.

        if (scenesString.Length <= 0) // ���� ������ ������, �� ������� ������ ���������.
            return new List<string>();
        else // �����...
            return JsonUtility.FromJson<ScenesContainer>(scenesString).scenes; // ������� ��������� ����.
    }

    /// <summary>
    /// Puts the scenes contained in the scene container in the log.
    /// </summary>
    /// <param name="scenes">List of scenes' names.</param>
    public void SetScenes(List<string> scenes)
    {
        ScenesContainer container = new ScenesContainer(); //������������ ��������� ����.
        container.scenes = scenes;
        string str = JsonUtility.ToJson(container); // ������������� ��������� � ������ JSON.
        UnityEngine.PlayerPrefs.SetString("Scenes", str); // ��������� ���������� ������ JSON � ����.
    }

    /// <summary>
    /// Deletes all scenes' names in log.
    /// </summary>
    public void ClearScenes()
    {
        if (UnityEngine.PlayerPrefs.HasKey("Scenes")) // ������� ���������� � ������, ���� ������� ����������.
        {
            UnityEngine.PlayerPrefs.DeleteKey("Scenes");
        }
    }

    /// <summary>
    /// Class representing container of scenes.
    /// </summary>
    class ScenesContainer
    {
        public List<string> scenes;
    }
}
