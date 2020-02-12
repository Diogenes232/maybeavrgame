using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Alienation : MonoBehaviour {

    bool aliensComing = false;
    bool aliensHere = false;
    List<Transform> aliens = new List<Transform>();

    void Start() {
        foreach (Transform child in transform) {
            aliens.Add(child);
        }
    }

    void FixedUpdate() {
        if (MainMain.myStopWatch.execeedesSeconds(MainMain.secondsBeforeAlienation)) {
            if (!aliensComing && !aliensHere) {
                activateAliens();
            }
        }

        if (aliensComing && !aliensHere) {
            moveAliens();
        }
    }

    private void activateAliens() {
        UnityEngine.Debug.Log("Starting the alienation..");
        foreach (Transform alien in aliens) {
            alien.gameObject.SetActive(true);
        }
        aliensComing = true;
    }

    private void moveAliens() {
        float positionAliensY = transform.position.y;
        float loweringSpeed = (positionAliensY + 25.58f ) / 2.0f;

        if (loweringSpeed < 0.01f) {
            aliensHere = true;
            aliensComing = false;
            return ;
        }
        else if (loweringSpeed < 3.0f) {
            loweringSpeed = 3.0f;
        }
        transform.Translate(new Vector3(0, -loweringSpeed, 0) * Time.deltaTime, Space.World);
    }
}
