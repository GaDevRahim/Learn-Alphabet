using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    GameManager gameManager;
    [Header("UI Component"), Space(10)]
    [SerializeField] Slider scoreBar;
    [SerializeField] internal float maxScore;
    [SerializeField] internal float MinScore;
    [SerializeField] internal float currenScore;

    [Header("Buttons"), Space(10)]
    [SerializeField] Button startButton;
    [SerializeField] Button restartButton;
    [SerializeField] TMP_Text greatText;
    [SerializeField] TMP_Text overText;

    void Start()
    {
        SetAllComponent();
        StartUi();
        startButton.onClick.AddListener(StartButtonFun);
        restartButton.onClick.AddListener(RestartButtonFun);
    }

    void StartButtonFun()
    {
        Time.timeScale = 1;
        gameManager.GameOver = false;
        startButton.gameObject.SetActive(false);
        scoreBar.gameObject.SetActive(true);
        scoreBar.value = currenScore;
        gameManager.GoBackToBasicLine();
    }

    void RestartButtonFun()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    void SetAllComponent()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        scoreBar = scoreBar.gameObject.GetComponent<Slider>();
        startButton = startButton.gameObject.GetComponent<Button>();
        restartButton = restartButton.gameObject.GetComponent<Button>();
        greatText = greatText.gameObject.GetComponent<TMP_Text>();
        overText = overText.gameObject.GetComponent<TMP_Text>();
        StartUi();
    }

    internal void StartUi()
    {
        scoreBar.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        SetScore(MinScore, currenScore, maxScore);
        Time.timeScale = 0;
    }


    internal void SetScore(float min, float current, float max)
    {
        scoreBar.minValue = min;
        scoreBar.value = current;
        scoreBar.maxValue = max;
    }

    internal float GetScore()
    {
        return scoreBar.value;
    }

    internal void SetNewScore(bool correct)
    {
        float bouns = 12.5f;
        if (correct) scoreBar.value += bouns; else if (!correct) scoreBar.value -= bouns;
    }

    internal void UiGameFinished()
    {
        gameManager.GameOver = true;
        scoreBar.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
        if (scoreBar.value == MinScore)
        {
            overText.gameObject.SetActive(true);
            greatText.gameObject.SetActive(false);
        }
        else if (scoreBar.value == maxScore)
        {
            greatText.gameObject.SetActive(true);
            overText.gameObject.SetActive(false);
        }
    }

}
