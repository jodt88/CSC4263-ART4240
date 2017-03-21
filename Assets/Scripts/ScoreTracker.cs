using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public TextMesh scoreText;

    void Start()
    {
        scoreText = GameObject.Find("ScoreDisplay").GetComponent<TextMesh>();
        scoreText.GetComponent<TextMesh>().text = "Score: " + TotalScore.score;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "patron0")
        {
            TotalScore.score += 50;
        }
    }

    void Update()
    {
        scoreText = GameObject.Find("ScoreDisplay").GetComponent<TextMesh>();
        scoreText.GetComponent<TextMesh>().text = "Score: " + TotalScore.score;
    }
}
