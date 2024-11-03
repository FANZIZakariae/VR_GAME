using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class AnimationStateController : MonoBehaviour
{
    private Animator animator;
    private bool isRunning = false;
    public float speed = 10f; // Speed of the character

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the "1" key is pressed to start running
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartMoving(); // Start moving when "1" is pressed
        }

        // If the character is running, move it forward
        if (isRunning)
        {
            // Move the character forward
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    public void StartMoving() // New method to start moving
    {
        isRunning = true; // Set isRunning to true
        animator.SetBool("isRunning", isRunning); // Trigger the running animation
    }

    // Method called when the collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object we collided with is the "EndingZone"
        if (other.gameObject.CompareTag("EndingZone"))
        {
            // Load the GAMEOVER scene
            SceneManager.LoadScene("GAMEOVER");
        }
    }
}
