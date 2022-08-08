using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Balloons")]
    [SerializeField] List<GameObject> balloonsList;
    [SerializeField] List<PrefabsMove> prefabsMoveScripts;
    internal bool allCanMoves;

    [Header("Chars")]
    [SerializeField] char targetChar;
    [SerializeField] List<Char> charsList = new List<char>();

    [Header("List Of TextMesh Chars")]
    [SerializeField] List<TextMeshPro> charsText;

    [Header("Aduio"), SerializeField, Space(10)]
    AudioClip ChooseChar;
    [SerializeField, Space(5)] List<AudioClip> congratulationsList;
    int randomCongratulationAudio;
    AudioSource audioSource;
    float V_SpeakerSound = 5.0f;

    [Header("ParticleSystem"), SerializeField, Space(10)]
    List<ParticleSystem> Bad_Good_FX;

    [Header("Buttons"), Space(10)]
    [SerializeField] UIController ui_Controller;

    internal bool GameOver;

    void Start()
    {
        GameOver = false;
        SetAllComponent();
        SetCharsText();
    }

    void SetAllComponent()
    {
        for (int i = 0; i < prefabsMoveScripts.Count; i++)
            prefabsMoveScripts[i] = prefabsMoveScripts[i].gameObject.GetComponent<PrefabsMove>();
        ui_Controller = ui_Controller.gameObject.GetComponent<UIController>();
        audioSource = GetComponent<AudioSource>();
    }

    internal IEnumerator CallChar()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(ChooseChar, V_SpeakerSound);
    }

    void FixedUpdate()
    {
        if (IsOnBasicLine()) SwapChars();
    }

    internal bool IsOnBasicLine()
    {
        return (balloonsList[0].transform.position.y == balloonsList[1].transform.position.y &&
                balloonsList[1].transform.position.y == balloonsList[2].transform.position.y &&
                balloonsList[2].transform.position.y == -9.0f);
    }

    internal bool IsGameOver()
    {
        return GameOver;
    }

    void SwapChars()
    {
        if (GameOver) return;
        char temp = charsList[1];
        charsList[1] = charsList[0];
        charsList[0] = charsList[2];
        charsList[2] = temp;
        SetCharsText();
    }

    void SetCharsText()
    {
        for (int i = 0; i < charsText.Count; i++)
        {
            charsText[i].SetText(charsList[i] + "");
        }
    }

    internal void TestChar(int index)
    {
        if(Convert.ToChar(charsText[index].text) == targetChar)
        {
            
            Instantiate(Bad_Good_FX[1], balloonsList[index].transform.position, Bad_Good_FX[1].transform.rotation);
            randomCongratulationAudio = UnityEngine.Random.Range(0, congratulationsList.Count);
            audioSource.PlayOneShot(congratulationsList[randomCongratulationAudio], V_SpeakerSound);
            GoBackToBasicLine();
            ui_Controller.SetNewScore(true);
            if (ui_Controller.GetScore() == 100.0f)
            {
                ui_Controller.UiGameFinished();
                GameOver = true;
            }
        }
        else
        {
            Instantiate(Bad_Good_FX[0], balloonsList[index].transform.position, Bad_Good_FX[0].transform.rotation);
            GoBackToBasicLine();
            ui_Controller.SetNewScore(false);
            if (ui_Controller.GetScore() == 0.0f)
            {
                ui_Controller.UiGameFinished();
                GameOver = true;
            }
        }
    }

    internal void GoBackToBasicLine()
    {
        for (int i = 0; i < prefabsMoveScripts.Count; i++)
            prefabsMoveScripts[i].GoNow();
    }
}
