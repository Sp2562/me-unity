using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(10f, 1000f)]
     // Corrected variable name
    public GameObject player;
    private Rigidbody playerRb;
    public float moveDistance = 10f;
    public float jumpMultiplier = 1f; // Corrected variable name

    void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        MoveAndJumpPlayer();
    }

    void MoveAndJumpPlayer()
    {


       // public float moveDistance = 100f;
      //  public float jumpMultiplier = 50f;

    Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x = moveDistance * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x = -moveDistance * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.z = moveDistance * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDirection.z = -moveDistance * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveDirection.y = moveDistance * jumpMultiplier * Time.deltaTime;
        }

        playerRb.AddForce(moveDirection);
    }
}
