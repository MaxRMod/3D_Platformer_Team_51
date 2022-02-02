using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissappearingPlatform : MonoBehaviour
{
 private float m_waitInSeconds = 1f;
 private float StartTime = 0.0f;
 public GameObject platform;
 private bool isActive;
 private float activeTimer;
 
 void Start()
 {
    isActive = true;
    activeTimer = 0;
 }
 
    void Update()
    {
        activeTimer += Time.deltaTime;
        if(activeTimer >= 3 && !isActive)
        {
           platform.SetActive(true);
           activeTimer = 0;
           isActive = true;
        }
        if (activeTimer >= 2 && isActive) 
        {
            platform.SetActive(false);
            activeTimer = 0;
            isActive = false;
        }
    }     
} 
