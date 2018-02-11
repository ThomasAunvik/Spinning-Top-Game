using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour {

    public Canvas pauseCanvas;
    
    public void PauseGame()
    {
        pauseCanvas.enabled = true;
        GameManager.PauseGame();
    }

    public void ResumeGame()
    {
        pauseCanvas.enabled = false;
        GameManager.ResumeGame();
    }

    public void MainMenu()
    {
        GameManager.LoadLevel(1);
    }
}
