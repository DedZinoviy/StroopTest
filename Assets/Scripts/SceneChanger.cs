using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private PlayerPrefs playerPrefs;

    private List<string> scenesSequence;

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

    public void NextScene()
    {
        if (scenesSequence.Count <= 0)
            GenerateScenesSequence();

        string nextScene = scenesSequence[0];
        scenesSequence.RemoveAt(0);
        playerPrefs.SetScenes(scenesSequence);
        ChangeScene(nextScene);
    }

    public void ToMenu()
    {
        playerPrefs.ClearScenes();
        ChangeScene("MainMenu");
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

    private void OnApplicationQuit()
    {
        playerPrefs.ClearScenes();
    }

    private void GenerateScenesSequence()
    {
        scenesSequence = new List<string> { "FallingFruits", "Labyrinth", "StroopTest" };

        for (int i = scenesSequence.Count - 1; i >= 1; i--)
        {
            int newIndex = random.Next(0, i + 1);
            (scenesSequence[i], scenesSequence[newIndex]) = (scenesSequence[newIndex], scenesSequence[i]);
        }

        scenesSequence.ForEach(x => Debug.Log(x));
    }
}
