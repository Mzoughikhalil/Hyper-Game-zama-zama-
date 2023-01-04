using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField] private float forceMagnanto;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float rotSpeed;

    private Camera mainCamera;
    private Rigidbody rb;

    private Vector3 movementDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ProccessInput();
        KeepPlayerOnScreen();
        RotateToFace();
    }
    void FixedUpdate()
    {
        if (movementDirection == Vector3.zero) { return; }
        rb.AddForce(movementDirection * forceMagnanto * Time.deltaTime, ForceMode.Force);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }

    private void ProccessInput()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();


            Vector3 worldPostion = mainCamera.ScreenToWorldPoint(touchPosition);

            movementDirection = transform.position - worldPostion;
            movementDirection.z = 0;
            movementDirection.Normalize();

        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }
    private void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        if(viewportPosition.x > 1)
        {
            newPosition.x = -newPosition.x + .1f;
        }
        else if (viewportPosition.x < 0)
        {
            newPosition.x = -newPosition.x - .1f;
        }
        if (viewportPosition.y > 1)
        {
            newPosition.y = -newPosition.y + .1f;
        }
        else if (viewportPosition.y < 0)
        {
            newPosition.y = -newPosition.y - .1f;
        }

        transform.position = newPosition;
    }
    void RotateToFace()
    {
        if(rb.velocity == Vector3.zero) { return; }

        Quaternion targeRotatin = Quaternion.LookRotation(rb.velocity, Vector3.back);

        transform.rotation = Quaternion.Lerp(transform.rotation, 
            targeRotatin, rotSpeed * Time.deltaTime);
    }



}
