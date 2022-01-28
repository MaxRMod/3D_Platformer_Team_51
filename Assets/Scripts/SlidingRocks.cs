using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingRocks : MonoBehaviour
{

    public GameObject[] stones;
    public GameObject[] spawnpoints;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < stones.Length; i++) 
        {
            stones[i].transform.position = spawnpoints[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
