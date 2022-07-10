using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for changing scenes in application.
/// </summary>
public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// Inscance of saving module.
    /// </summary>
    [SerializeField]
    private PlayerPrefs playerPrefs;

    /// <summary>
    /// List of scenes' names.
    /// </summary>
    private List<string> scenesSequence;

    /// <summary>
    /// Current complexity level.
    /// </summary>
    private int levelComplexity;

    private int currentScore;

    /// <summary>
    /// Instance of randomizer.
    /// </summary>
    private System.Random random;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
            playerPrefs.Clear();

        scenesSequence = playerPrefs.GetScenes();
        random = new System.Random(DateTime.Now.Millisecond);
        levelComplexity = playerPrefs.LoadLevelComplexity();
        currentScore = playerPrefs.LoadCurrentScore();
    }

    /// <summary>
    /// Loads next random scene.
    /// </summary>
    public void NextScene()
    {
        if (scenesSequence.Count <= 0) // Сгенерировать массив сцен, если он пустой.
        {
            GenerateScenesSequence();
            
            if (levelComplexity < 5)
                levelComplexity++;
            
            playerPrefs.SaveLevelComlexity(levelComplexity);
        }

        if (SceneManager.GetActiveScene().name == scenesSequence[0])
        {
            (scenesSequence[0], scenesSequence[^1]) = (scenesSequence[^1], scenesSequence[0]);
        }

        string nextScene = scenesSequence[0]; // Получить название следующей сцены.
        scenesSequence.RemoveAt(0); // Считать выбранную сцену использованной.
        playerPrefs.SetScenes(scenesSequence); // Сохранить изменённую последовательность в логе.
        playerPrefs.SaveCurrentScore(currentScore + 1);
        ChangeScene(nextScene); // Сменить сцену.
    }

    public void Retry()
    {
        scenesSequence = new List<string>();
        levelComplexity = 0;
        currentScore = -1;
        NextScene();
    }

    /// <summary>
    /// Loads main menu scene.
    /// </summary>
    public void ToMenu()
    {
        playerPrefs.SaveScore(playerPrefs.LoadCurrentScore());
        ChangeScene("MainMenu"); // Открыть сцену меню.
    }

    /// <summary>
    /// Changes scenes into apllication.
    /// </summary>
    /// <param name="sceneName"></param>
    private void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Closes application.
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }

    // OnApplicationQuit is called when the application quits.
    private void OnApplicationQuit()
    {
        playerPrefs.SaveScore(currentScore);
    }

    /// <summary>
    /// Generates random scenes sequence.
    /// </summary>
    private void GenerateScenesSequence()
    {
        scenesSequence = new List<string> { "FallingFruits", "Labyrinth", "StroopTest", "CardPairs" };  // Перечислиить все возможные сцены.

        // Создать случайную последовательность.
        for (int i = scenesSequence.Count - 1; i >= 1; i--) // Для каждой сцены.
        {
            int newIndex = random.Next(0, i + 1); // Выбрать случайное положение сцены.
            (scenesSequence[i], scenesSequence[newIndex]) = (scenesSequence[newIndex], scenesSequence[i]); // Вставить сцену.
        }
    }
}
