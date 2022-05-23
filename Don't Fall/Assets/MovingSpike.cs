using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpike : MonoBehaviour {

    [SerializeField] private Transform position1;
    [SerializeField] private Transform position2;
    [SerializeField] private float movementSpeed;
    private Transform target;

    private float pushForce = 100f;

    private void Awake() {
        if (Vector3.Distance(transform.position, position1.position) <= Vector3.Distance(transform.position, position2.position)) {
            target = position1;
        }
        else {
            target = position2;
        }
    }

    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f) {
            ChangeDirection();
        }
    }

    private void ChangeDirection() {
        if (target == position1) {
            target = position2;
            return;
        }
        target = position1;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Player") {
            Vector3 pushDirection = transform.position - collision.contacts[0].point;
            pushDirection = -pushDirection.normalized;
            collision.collider.GetComponent<Rigidbody>().AddForce(new Vector3(pushDirection.x, 0.5f, pushDirection.z) * pushForce, ForceMode.Impulse);
        }
    }

}
