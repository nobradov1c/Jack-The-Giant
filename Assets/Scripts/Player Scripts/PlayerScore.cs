using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField]
    private AudioClip coinClip, lifeClip;

    private CameraScript cameraScript;

    private Vector3 previousPosition;
    private bool countScore;

    public static int scoreCount;
    public static int lifeScore;
    public static int coinScore;

    private void Awake()
    {
        cameraScript = Camera.main.GetComponent<CameraScript>();
        scoreCount = 0;
        lifeScore = 2;
        coinScore = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
        countScore = true;
    }

    // Update is called once per frame
    void Update()
    {
        CountScore();
    }

    void CountScore()
    {
        if (countScore)
        {
            if(transform.position.y < previousPosition.y)
            {
                scoreCount++;
                GameplayController.instance.SetScore(scoreCount);
            }
            previousPosition = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Coin")
        {
            coinScore++;
            scoreCount += 200;
            GameplayController.instance.SetScore(scoreCount);
            GameplayController.instance.SetCoinScore(coinScore);
            AudioSource.PlayClipAtPoint(coinClip, transform.position);
            target.gameObject.SetActive(false);
        }

        if(target.tag == "Life")
        {
            lifeScore++;
            scoreCount += 300;
            GameplayController.instance.SetScore(scoreCount);
            GameplayController.instance.SetCoinScore(lifeScore);
            AudioSource.PlayClipAtPoint(lifeClip, transform.position);
            target.gameObject.SetActive(false);
        }

        if(target.tag == "Bounds" || target.tag == "Deadly")
        {
            cameraScript.moveCamera = false;
            countScore = false;

            transform.position = new Vector3(500, 500, 0);
            lifeScore--;
            GameManager.instance.CheckGameStatus(scoreCount, coinScore, lifeScore);

        }
    }
}
