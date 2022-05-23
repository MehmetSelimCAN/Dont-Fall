using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody rb;
    private Camera mainCamera;
    private Vector3 cameraOffset;

    private float speed;
    private float xInput;
    private float zInput;

    public static bool hasCollided;
    public static bool canMove;

    private void Awake() {
        speed = 165f;
        rb = GetComponent<Rigidbody>();

        mainCamera = Camera.main;
        cameraOffset = new Vector3(0f, mainCamera.transform.position.y, mainCamera.transform.position.z);
        mainCamera.transform.position = transform.position + cameraOffset;
    }

    private void Update() {
        TakeInputs();
        CameraMovement();

        if (transform.position.y < 0.25f) {
            rb.drag = 1;
        }

        if (transform.position.y < -2) {
            GameManager.Restart();
        }
    }

    private void FixedUpdate() {
        if (!GameManager.isGamePaused) {
            Move();
        }
    }

    private void TakeInputs() {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
    }

    private void Move() {
        if (rb.drag == 5 && canMove) {
            Vector3 direction = new Vector3(xInput, 0f, zInput);
            rb.AddForce(direction.normalized * speed);
        }
    }

    private void CameraMovement() {
        mainCamera.transform.position = transform.position + cameraOffset;
    }

    private void OnCollisionStay(Collision collision) {
        canMove = true;
        rb.drag = 5;
    }

    private void OnCollisionExit(Collision collision) {
        canMove = false;
        rb.drag = 1;
    }
}
