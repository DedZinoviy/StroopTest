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
        if (scenesSequence.Count <= 0) // ������������� ������ ����, ���� �� ������.
            GenerateScenesSequence();

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
