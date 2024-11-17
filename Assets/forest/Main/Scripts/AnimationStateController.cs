using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class AnimationStateController : MonoBehaviour
{
    private float runTime = 0f; // Timer to track how long the character has been moving
    private float maxRunTime = 8f; // Maximum time the character should move
    private int once = 1; 

    private Animator animator;
    private bool isRunning = false;
    public float speed = 10f; // Speed of the character

    public CountdownTimer counter; // Reference to CountdownTimer
    public ItemTriggerZone itemWatcher; // Reference to ItemTriggerZone

    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();

        // Check if counter and itemWatcher references are set
        if (counter == null)
        {
            Debug.LogError("CountdownTimer reference is missing in AnimationStateController. Please assign it in the Inspector.");
        }
        if (itemWatcher == null)
        {
            Debug.LogError("ItemTriggerZone reference is missing in AnimationStateController. Please assign it in the Inspector.");
        }
    }

    void Update()
    {
        // Start moving if either condition is met and isRunning is still false
        if ((counter != null && counter.isReady || itemWatcher != null && !itemWatcher.isSafe) && !isRunning && (once == 1))
        {
            StartMoving();
        }

        // If the character is running, move it forward
        if (isRunning)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            // Increment the run timer
            runTime += Time.deltaTime;

            // Stop the character after maxRunTime seconds
            if (runTime >= maxRunTime)
            {
                StopMoving(); // Stop both animation and movement
            }
        }
    }

    public void StartMoving()
    {
        isRunning = true; // Set isRunning to true
        once = 0;
        runTime = 0f; // Reset the timer
        animator.SetBool("isRunning", isRunning); // Trigger the running animation
    }

    public void StopMoving()
    {
        // Stop movement and animation
        isRunning = false; // Set isRunning to false to stop movement
        transform.Translate(Vector3.zero); // Ensure no further movement by setting velocity to zero (or simply not moving)
        animator.SetBool("isRunning", isRunning); // Stop the running animation
        SceneManager.LoadScene("GameOver");

    }
}
