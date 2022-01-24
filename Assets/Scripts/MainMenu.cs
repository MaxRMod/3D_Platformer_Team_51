using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour
{
    public void LoadLevelByIndex(int levelIndex){
        SceneManager.LoadScene(levelIndex);
    }

    public void loadLevelByName(string levelName){

        SceneManager.LoadScene(levelName);
    }

    public void resetStats(){


    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale=1;
    }

    //MainMenu -> Game
    public void PlayGame()
    {
        resetStats();
        SceneManager.LoadScene("Spiellevel");
    }

    //Exit Game
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
