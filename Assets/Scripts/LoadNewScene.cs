using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            int index = SceneManager.GetActiveScene().buildIndex;
            if (index == 4) {
                Cursor.visible = true;
                SceneManager.LoadScene(0);
            } else {
                SceneManager.LoadScene(++index);
            }
            
        }
    }
}
