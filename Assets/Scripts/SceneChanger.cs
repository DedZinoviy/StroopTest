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

    /// <summary>
    /// Instance of randomizer.
    /// </summary>
    private System.Random random;

    // Start is called before the first frame update
    void Start()
    {
        scenesSequence = playerPrefs.GetScenes();
        random = new System.Random(DateTime.Now.Millisecond);
        levelComplexity = playerPrefs.LoadLevelComplexity();
        Debug.Log(levelComplexity);
    }

    /// <summary>
    /// Loads next random scene.
    /// </summary>
    public void NextScene()
    {
        if (scenesSequence.Count <= 0) // ������������� ������ ����, ���� �� ������.
        {
            GenerateScenesSequence();
            
            if (levelComplexity < 5)
                levelComplexity++;
            
            playerPrefs.SaveLevelComlexity(levelComplexity);
        }

        if (SceneManager.GetActiveScene().name == scenesSequence[0])
        {
            Debug.Log("SwitchScene");
            (scenesSequence[0], scenesSequence[^1]) = (scenesSequence[^1], scenesSequence[0]);
        }

        string nextScene = scenesSequence[0]; // �������� �������� ��������� �����.
        scenesSequence.RemoveAt(0); // ������� ��������� ����� ��������������.
        playerPrefs.SetScenes(scenesSequence); // ��������� ��������� ������������������ � ����.
        ChangeScene(nextScene); // ������� �����.
    }

    /// <summary>
    /// Loads main menu scene.
    /// </summary>
    public void ToMenu()
    {
        playerPrefs.ClearScenes(); // �������� ������ ����.
        playerPrefs.ClearLevelComplexity(); //�������� ������� ���������.
        ChangeScene("MainMenu"); // ������� ����� ����.
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
        playerPrefs.ClearLevelComplexity();
    }

    /// <summary>
    /// Generates random scenes sequence.
    /// </summary>
    private void GenerateScenesSequence()
    {
        scenesSequence = new List<string> { "FallingFruits", "Labyrinth", "StroopTest", "CardPairs" };  // ������������ ��� ��������� �����.

        // ������� ��������� ������������������.
        for (int i = scenesSequence.Count - 1; i >= 1; i--) // ��� ������ �����.
        {
            int newIndex = random.Next(0, i + 1); // ������� ��������� ��������� �����.
            (scenesSequence[i], scenesSequence[newIndex]) = (scenesSequence[newIndex], scenesSequence[i]); // �������� �����.
        }
    }
}
