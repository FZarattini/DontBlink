using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;
    bool paused;

	// Use this for initialization
	void Start () {
        paused = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
	}

    public void PauseGame()
    {

        if (paused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            paused = !paused;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            paused = !paused;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
