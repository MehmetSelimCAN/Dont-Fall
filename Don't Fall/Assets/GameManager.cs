using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static bool isGamePaused;
    public static Vector3 spawnPoint;
    public static Transform levelsPrefab;
    public static Transform currentLevels;

    public static Transform winScreen;

    private void Awake() {
        levelsPrefab = Resources.Load<Transform>("Prefabs/pfLevels");
        winScreen = GameObject.Find("WinScreen").transform;
        winScreen.gameObject.SetActive(false);
    }

    private void Start() {
        GameObject.FindGameObjectWithTag("Player").transform.position = spawnPoint;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            Restart();
        }
    }

    public static void Restart() {
        PlayerMovement.hasCollided = false;

        currentLevels = GameObject.FindGameObjectWithTag("CurrentLevels").transform;
        Destroy(currentLevels.gameObject);

        Transform levels = Instantiate(levelsPrefab);
        for (int i = 0; i < levels.childCount; i++) {
            if (levels.GetChild(i).GetComponentInChildren<CheckPoint>().transform.position == CheckPoint.currentCheckpoint.position) {
                break;
            }
            else {
                    //Destroy the completed parts.
                Destroy(levels.GetChild(i).gameObject);
            }
        }

        GameObject.FindGameObjectWithTag("Player").transform.position = spawnPoint;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().drag = 5;
    }

    public static void Win() {
        winScreen.gameObject.SetActive(true);
    }
}
