using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FailPanel : MonoBehaviour
{
    [SerializeField]
    private PlayerPrefs playerPrefs;

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private TMP_Text recordText;

    [SerializeField]
    private Button retryBtn;

    [SerializeField]
    private Button toMenuBtn;

    private int currentScore;

    private int currentRecord;

    private bool isFailed;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        isFailed = false;
    }

    public void Open()
    {
        if (isFailed == true)
            return;

        isFailed = true;
        gameObject.SetActive(true);

        currentScore = playerPrefs.LoadCurrentScore();
        currentRecord = playerPrefs.LoadScore();

        retryBtn.enabled = false;
        toMenuBtn.enabled = false;

        scoreText.text = "Текущий счет: " + currentScore.ToString();
        if (currentScore > currentRecord)
            recordText.text = "Новый рекорд: " + currentScore.ToString();
        else
            recordText.text = "Текущий рекорд: " + currentRecord.ToString();

        playerPrefs.SaveScore(currentScore);
        StartCoroutine(SetButtonEnable());
    }

    private IEnumerator SetButtonEnable()
    {
        yield return new WaitForSeconds(1);

        if (retryBtn.enabled == false)
        {
            retryBtn.enabled = true;
            toMenuBtn.enabled = true;
        }
    }
}
