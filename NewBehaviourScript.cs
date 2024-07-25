using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    public Vector2 currentPosition; // Current position of the touch
    public Vector2 initiatePosition; // Initial touch position
    private Vector2 touchDirection; // Direction of the touch
    private bool isTouchActive = false; // Flag to check if touch is active
    [SerializeField]
    private float radius = 50f; // Radius boundary for the joystick
    [SerializeField]
    private float moveSpeed = 5f; // Speed at which the player moves

    public Rigidbody rb; // Ensure Rigidbody is assigned in the Inspector or via code

    public TMP_Text currentPositionText; // Ensure these are assigned in the Inspector
    public TMP_Text initiatePositionText;
    public TMP_Text boundaryConditionText;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                initiatePosition = touch.position; // Record initial touch position
                isTouchActive = true; // Set touch active flag
                Debug.Log("Start point: " + initiatePosition);
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                currentPosition = touch.position; // Update current touch position
                touchDirection = currentPosition - initiatePosition; // Calculate direction

                if (IsWithinBoundary(currentPosition)) // Check current position
                {
                    Debug.Log("Within boundary");
                    MovePlayer(touchDirection); // Move the player
                }
                else
                {
                    Debug.Log("Current position is outside the boundary.");
                    isTouchActive = false; // Set touch active flag to false
                }
                Debug.Log("Direction: " + touchDirection);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouchActive = false; // Set touch active flag to false
                initiatePosition = Vector2.zero;
                currentPosition = Vector2.zero;
            }
        }

        currentPositionText.text = "Current Position: " + currentPosition;
        initiatePositionText.text = "Initiate Position: " + initiatePosition;
        boundaryConditionText.text = "Within Boundary: " + IsWithinBoundary(currentPosition);
    }

    public bool IsWithinBoundary(Vector2 point)
    {
        // Calculate the distance between the center point and the point to check
        float distance = Vector2.Distance(initiatePosition, point);

        // Check if the distance is less than or equal to the radius
        return distance <= radius;
    }

    void MovePlayer(Vector2 direction)
    {
        if (isTouchActive)
        {
            Vector2 normalizedDirection = direction.normalized;
            Vector3 movement = new Vector3(normalizedDirection.x, 0, normalizedDirection.y) * moveSpeed * Time.deltaTime; // Convert Vector2 to Vector3 and set y to 0
            Vector3 newPosition = rb.position + movement; // Calculate new position
            rb.MovePosition(newPosition); // Use MovePosition for smooth movement
        }
    }
}
