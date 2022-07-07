using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for working with player preferences and other stored information.
/// </summary>
public class PlayerPrefs : MonoBehaviour
{
    /// <summary>
    /// Saves score in log.
    /// </summary>
    public void SaveScore(int newScore)
    {
        int currentScore = LoadScore();
        if (newScore > currentScore)
            UnityEngine.PlayerPrefs.SetInt("SavedInteger", newScore); // Сохранить информацию о счёте.
    }

    /// <summary>
    /// Loads the score from log.
    /// </summary>
    /// <returns>Contained score.</returns>
    public int LoadScore()
    {
        int score = 0;
        if (UnityEngine.PlayerPrefs.HasKey("SavedInteger")) // Получить информацию о счёте, если таковая существует.
        {
            score = UnityEngine.PlayerPrefs.GetInt("SavedInteger");
        }
        return score; // Вернуть полученную информацию.
    }

    public void SaveCurrentScore(int score)
    {
        UnityEngine.PlayerPrefs.SetInt("CurrentScore", score);
    }

    public int LoadCurrentScore()
    {
        int currentScore = -1;
        if (UnityEngine.PlayerPrefs.HasKey("CurrentScore")) // Получить информацию о счёте, если таковая существует.
        {
            currentScore = UnityEngine.PlayerPrefs.GetInt("CurrentScore");
        }
        return currentScore; // Вернуть полученную информацию.
    }

    public void ClearCurrentScore()
    {
        if (UnityEngine.PlayerPrefs.HasKey("CurrentScore")) // Получить информацию о счёте, если таковая существует.
        {
            UnityEngine.PlayerPrefs.DeleteKey("CurrentScore");
        }
    }

    /// <summary>
    /// Saves level complexity in log.
    /// </summary>
    public void SaveLevelComlexity(int levelComplexity)
    {
        UnityEngine.PlayerPrefs.SetInt("LevelComplexity", levelComplexity);
    }

    /// <summary>
    /// Loads the level complexity from log.
    /// </summary>
    /// <returns>Contained level complexity.</returns>
    public int LoadLevelComplexity()
    {
        int levelComplexity = 0;
        if (UnityEngine.PlayerPrefs.HasKey("LevelComplexity"))
        {
            levelComplexity = UnityEngine.PlayerPrefs.GetInt("LevelComplexity");
        }
        return levelComplexity;
    }

    public void ClearLevelComplexity()
    {
        if (UnityEngine.PlayerPrefs.HasKey("LevelComplexity"))
        {
            UnityEngine.PlayerPrefs.DeleteKey("LevelComplexity");
        }
    }

    /// <summary>
    /// Clears all info in log.
    /// </summary>
    public void Clear()
    {
        UnityEngine.PlayerPrefs.DeleteAll(); // Стереть всю информацию в логе.
    }

    /// <summary>
    /// Returns scenes from the log as a scene container.
    /// </summary>
    /// <returns>List of scenes.</returns>
    public List<string> GetScenes()
    {
        string scenesString = UnityEngine.PlayerPrefs.GetString("Scenes"); //Получить строку с названием сцен.

        if (scenesString.Length <= 0) // Если строка пустая, то вернуть пустую коллекцию.
            return new List<string>();
        else // Иначе...
            return JsonUtility.FromJson<ScenesContainer>(scenesString).scenes; // Вернуть коллекцию сцен.
    }

    /// <summary>
    /// Puts the scenes contained in the scene container in the log.
    /// </summary>
    /// <param name="scenes">List of scenes' names.</param>
    public void SetScenes(List<string> scenes)
    {
        ScenesContainer container = new ScenesContainer(); //Сформировать контейнер сцен.
        container.scenes = scenes;
        string str = JsonUtility.ToJson(container); // Преобразовать контейнер в строку JSON.
        UnityEngine.PlayerPrefs.SetString("Scenes", str); // Сохранить полученную строку JSON в логе.
    }

    /// <summary>
    /// Deletes all scenes' names in log.
    /// </summary>
    public void ClearScenes()
    {
        if (UnityEngine.PlayerPrefs.HasKey("Scenes")) // Удалить информацию о сценах, если таковая существует.
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
