using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public static Transform currentCheckpoint;

    private void Awake() {
        if (currentCheckpoint == null) {
            currentCheckpoint = GameObject.Find("FirstPart").transform.Find("pfCheckPoint").transform;
            GameManager.spawnPoint = new Vector3(currentCheckpoint.position.x, 0.45f, currentCheckpoint.position.z);
        }
    }

    private void SetCheckPoint() {
        currentCheckpoint = transform;
        GameManager.spawnPoint = new Vector3(currentCheckpoint.position.x, 0.45f, currentCheckpoint.position.z);
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.tag == "Player") {
            SetCheckPoint();
        }
    }
}
