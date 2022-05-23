using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    private Transform floorsParent;
    private Transform lastFloor;

    private Transform particlesPrefab;

    private float pushForce = 1000f;

    private void Awake() {
        floorsParent = transform.parent;
        particlesPrefab = Resources.Load<Transform>("Prefabs/pfObstacleParticles");

        for (int i = 0; i < floorsParent.childCount; i++) {
            if (floorsParent.GetChild(i).gameObject == gameObject) {
                continue;
            }
            if (floorsParent.GetChild(i).GetComponent<Floor>().floorType == Floor.FloorType.Last) {
                lastFloor = floorsParent.GetChild(i).transform;
                break;
            }
        }
    }

    public void CheckFloors() {
        StartCoroutine(DelayForChecking());
    }

    private IEnumerator DelayForChecking() {
        yield return new WaitForSeconds(0.05f);                 
        if (lastFloor != null) {                                    //Obstacle and last floor
            if (lastFloor.GetComponent<Floor>().GetHealth() == 1 && floorsParent.childCount - 1 == 1) {
                Transform particles = Instantiate(particlesPrefab, transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
                Destroy(particles.gameObject, 1f);
                Destroy(gameObject);
            }
        }
    }
         
    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Player") {
            Vector3 pushDirection = transform.position - collision.contacts[0].point;
            pushDirection = -pushDirection.normalized;
            collision.collider.GetComponent<Rigidbody>().AddForce(pushDirection * pushForce);
        }
    }
}
