using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for dealing damage to player
public class HurtPlayer : MonoBehaviour
{

    [SerializeField]private int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player")) {
            FindObjectOfType<HealthManager>().TakeDamage(damage);
        }
    }
}
