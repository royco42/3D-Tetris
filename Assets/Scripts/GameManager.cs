using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // start playing
    public void startPlay()
	{
        SceneManager.LoadScene("2_GameScene");
    }

	// play game again
	public void playAgain()
	{
		SceneManager.LoadScene("2_GameScene");
    }

    // back to start
    public void back()
	{
        SceneManager.LoadScene("1_PlayScene");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("3_GameOver");
    }
    
    // quit game
    public void quitGame()
	{
		Application.Quit();
	}
}
