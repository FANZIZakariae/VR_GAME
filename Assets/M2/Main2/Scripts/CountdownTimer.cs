using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    float currentTime = 30f;
    float startingTime = 10f;

    public Text Counter;
    public AnimationStateController characterController; // Reference to the character controller

    void Start()
    {
        currentTime = startingTime;
        Counter.color = Color.white;
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        Counter.text = currentTime.ToString("0");

        if (currentTime <= 20)
        {
            Counter.color = Color.blue;
        }
        if (currentTime <= 10)
        {
            Counter.color = Color.red;
        }
        if (currentTime <= 0)
        {
            currentTime = 0;
            characterController.StartMoving(); // Start character movement when timer reaches zero
        }
    }
}
