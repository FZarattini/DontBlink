using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public float timeLeft;
    public GameObject timeCounter;
    public GameObject winnerMessageCanvas;
    public GameObject winnerMessage;
    public GameObject player;
    public GameObject hitCounter;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        timeCounter.GetComponent<Text>().text = timeLeft.ToString("F0");
        hitCounter.GetComponent<Text>().text = "Hits: " + player.GetComponent<Player>().hitCount.ToString();

        if(timeLeft < 10)
        {
            timeCounter.GetComponent<Text>().color = Color.red;
        }

        if(timeLeft < 0)
        {
            Time.timeScale = 0f;
            GameOver("Nazi");
        }
        else if(player.GetComponent<Player>().hitCount == 5)
        {
            Time.timeScale = 0f;
            GameOver("Hunter");
        }
	}

    private void GameOver(string winner)
    {
        if (winner == "Nazi")
        {
            
            winnerMessage.GetComponent<Text>().text = "Villain wins!";
            winnerMessageCanvas.SetActive(true);
        }else if(winner == "Hunter")
        {
            winnerMessage.GetComponent<Text>().text = "Hero wins!";
            winnerMessageCanvas.SetActive(true);
        }

    }
}
