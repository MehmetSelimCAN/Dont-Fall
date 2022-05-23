using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour {

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Player") {
            GameManager.Win();
        }
    }

}
