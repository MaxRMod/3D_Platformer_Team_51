using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for moving objects between scenes
public class AudioBetweenScenes : MonoBehaviour
{
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
}
