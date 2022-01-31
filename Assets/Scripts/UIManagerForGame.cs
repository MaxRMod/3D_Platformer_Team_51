using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManagerForGame : MonoBehaviour
{


    [SerializeField] GameObject pausePanel;           
    private bool isPaused=false;       
    public void LoadLevelByIndex(int levelIndex){
        SceneManager.LoadScene(levelIndex);

    }

    public void LoadLevelByName(string levelName){

        SceneManager.LoadScene(levelName);
    }

    public void ResetStats(){


    }

    public void ResumeFromPause(){
        pausePanel.SetActive(false);
        Time.timeScale=1;
        isPaused=false;
        Cursor.visible=false;

    }
    // Start is called before the first frame update
    void Start()
    {
        isPaused=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)&&!isPaused)
        {
            Time.timeScale=0;
            pausePanel.SetActive(true);
            isPaused=true;
            Cursor.visible=true;
       
        }



    }

}
