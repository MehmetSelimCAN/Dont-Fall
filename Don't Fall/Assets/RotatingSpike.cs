using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSpike : MonoBehaviour {

    [SerializeField] private Transform rotationPoint;
    [SerializeField] private float rotationSpeed;

    private float pushForce = 100f;

    private void Update() {
        transform.RotateAround(rotationPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Player") {
            Vector3 pushDirection = transform.position - collision.contacts[0].point;
            pushDirection = -pushDirection.normalized;
            collision.collider.GetComponent<Rigidbody>().AddForce(new Vector3(pushDirection.x, 0.5f, pushDirection.z) * pushForce, ForceMode.Impulse);
        }
    }

}
