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
    /// Instance of randomizer.
    /// </summary>
    private System.Random random;

    // Start is called before the first frame update
    void Start()
    {
        scenesSequence = playerPrefs.GetScenes();
        random = new System.Random(DateTime.Now.Millisecond);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Loads next random scene.
    /// </summary>
    public void NextScene()
    {
        if (scenesSequence.Count <= 0) // Сгенерировать массив сцен, если он пустой.
            GenerateScenesSequence();

        string nextScene = scenesSequence[0]; // Получить название следующей сцены.
        scenesSequence.RemoveAt(0); // Считать выбранную сцену использованной.
        playerPrefs.SetScenes(scenesSequence); // Сохранить изменённую последовательность в логе.
        ChangeScene(nextScene); // Сменить сцену.
    }

    /// <summary>
    /// Loads main menu scene.
    /// </summary>
    public void ToMenu()
    {
        playerPrefs.ClearScenes(); // Очистить список сцен.
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
        playerPrefs.ClearScenes();
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
