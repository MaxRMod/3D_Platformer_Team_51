using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Text healthText;
    [SerializeField]
    public int currentLives;
    [SerializeField]
    private int maxLives;
    // Start is called before the first frame update
    void Start()
    {
        currentLives = maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Lives: " + currentLives;
    }

    public void TakeDamage(int amount) {
        currentLives -= amount;
    }

    public void ResetLives() {
        currentLives = maxLives;
    }

    public void Heal(int amount) {
        currentLives += amount;

        if (currentLives > maxLives) {
            currentLives = maxLives;
        }
    }
}
