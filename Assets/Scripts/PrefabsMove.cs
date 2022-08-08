using System.Collections;
using UnityEngine;

public class PrefabsMove : MonoBehaviour
{
    GameManager gameManager;

    Vector2 basicPosi;
    bool canMove;
    float speed;
    float randomTime;
    float minTimeRange;
    float maxTimeRange;

    int count = 0;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        basicPosi = new Vector2(transform.position.x, -9.0f);
        speed = 0.05f;
        minTimeRange = 1.0f;
        maxTimeRange = 2.5f;
        GoNow();
    }

    void FixedUpdate()
    {
        if (canMove && gameManager.allCanMoves && !gameManager.GameOver)
        {
            transform.Translate(Vector3.up * speed);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.name == "Senssor")
        {
            canMove = false;
            GoNow();
        }
    }

    internal void GoNow()
    {
        canMove = false;
        transform.position = basicPosi;
        if(!gameManager.GameOver) StartCoroutine(SomeWait());
    }

    IEnumerator SomeWait()
    {
        yield return new WaitUntil(gameManager.IsOnBasicLine);
        gameManager.allCanMoves = false;
        randomTime = Random.Range(minTimeRange, maxTimeRange);
        yield return new WaitForSeconds(randomTime);
        gameManager.allCanMoves = true;
        canMove = true;
    }

    private void OnMouseDown()
    {
        int whichOgj = Mathf.RoundToInt((transform.position.x / 5) + 1);
        gameManager.TestChar(whichOgj);
    }
}