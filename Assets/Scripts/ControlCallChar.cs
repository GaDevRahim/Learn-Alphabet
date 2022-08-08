using UnityEngine;

public class ControlCallChar : MonoBehaviour
{
    GameManager gameManager;
    int CountBalloon;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        CountBalloon = 0;
    }

    private void OnTriggerExit(Collider collision)
    {
        CountBalloon++;
        if (CountBalloon == 3)
        {
            StartCoroutine(gameManager.CallChar());
            CountBalloon = 0;
        }
    }
}
