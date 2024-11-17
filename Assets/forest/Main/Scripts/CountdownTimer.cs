using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float startingTime = 100f;
    private float currentTime;
    public bool isReady = false;
    public Text Counter;
    
    void Start()
    {
        currentTime = startingTime;
        Counter.color = Color.white;
    }

    void Update()
    {
        if (isReady) return; // Exit if already ready

        // Update the timer
        currentTime -= Time.deltaTime;
        Counter.text = currentTime.ToString("0");

        // Change the counter color based on time left
        if (currentTime <= 10)
        {
            Counter.color = Color.red;
        }
        else if (currentTime <= 20)
        {
            Counter.color = Color.blue;
        }

        // Check if time has run out
        if (currentTime <= 0)
        {
            currentTime = 0;
            isReady = true;
            Debug.Log("Countdown Timer finished. isReady set to true.");
        }
    }
}
