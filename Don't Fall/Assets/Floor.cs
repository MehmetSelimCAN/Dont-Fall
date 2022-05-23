using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Floor : MonoBehaviour {

    [SerializeField] private int health;
    private TextMeshPro text;
    private MeshRenderer floorMeshRenderer;
    private MeshRenderer outlineMeshRenderer;
    private float darkAmount;
    private bool playerCollidedThisFloor;

    private Transform particlesPrefab;

    public FloorType floorType;

    public enum FloorType {
        Normal,
        Last
    }

    private void Awake() {
        playerCollidedThisFloor = false;

        floorMeshRenderer = transform.Find("floor").GetComponent<MeshRenderer>();
        outlineMeshRenderer = transform.Find("outline").GetComponent<MeshRenderer>();
        darkAmount = 0.25f;

        particlesPrefab = Resources.Load<Transform>("Prefabs/pfFloorParticles");

        ChangeColor();

        text = GetComponentInChildren<TextMeshPro>();
        text.SetText(health.ToString());
    }

    private void TakeDamage() {
        health--;
        text.SetText(health.ToString());
        ChangeColor();

        if (health == 0) {
            //destroy animation
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            Transform particles = Instantiate(particlesPrefab, transform.position, Quaternion.Euler(new Vector3(90,0,0)));
            Destroy(particles.gameObject, 1f);
            Destroy(gameObject);
        }
    }

    private void ChangeColor() {
        switch (health) {
            case 0:
            case 1:
                floorMeshRenderer.material.color = new Color32(200, 25, 0, 255);
                break;
            case 2:
                floorMeshRenderer.material.color = new Color32(255, 70, 0, 255);
                break;
            case 3:
                floorMeshRenderer.material.color = new Color32(250, 220, 50, 255);
                break;
            case 4:
                floorMeshRenderer.material.color = new Color32(50, 220, 255, 255);
                break;
        }

        Color floorColor = floorMeshRenderer.material.color;
        outlineMeshRenderer.material.color = new Color(floorColor.r - darkAmount, floorColor.g - darkAmount, floorColor.b - darkAmount);
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.collider.tag == "Player" && !playerCollidedThisFloor && !PlayerMovement.hasCollided) {
            PlayerMovement.hasCollided = true;
            playerCollidedThisFloor = true;
            floorMeshRenderer.material.color = Color.white;

            if (floorType == FloorType.Last) {
                transform.parent.GetComponentInChildren<Obstacle>().CheckFloors();
            }
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.collider.tag == "Player" && playerCollidedThisFloor) {
            TakeDamage();
            playerCollidedThisFloor = false;
            PlayerMovement.hasCollided = false;
        }
    }

    public int GetHealth() {
        return health;
    }
}
