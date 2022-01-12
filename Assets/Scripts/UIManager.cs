using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
        resetStats();
        SceneManager.LoadScene("Spiellevel");

        }

}
}
