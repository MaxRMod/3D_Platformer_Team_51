using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBetweenScenes : MonoBehaviour
{
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
}
