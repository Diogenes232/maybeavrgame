using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alienation : MonoBehaviour {

    bool alienated = false;
    List<Transform> aliens = new List<Transform>();

    void Start()
    {
        foreach (Transform child in transform) {
            aliens.Add(child);
        }
        UnityEngine.Debug.Log("Found aliens: " + aliens.Count);
    }

    void FixedUpdate()
    {
        checkForAlienation();
    }


    private void checkForAlienation() {

        if (alienated || MainMain.myStopWatch.execeedesSeconds(MainMain.secondsBeforeAlienation)) {            

            if (!alienated) {
                UnityEngine.Debug.Log("Starting the alienation..");
                foreach (Transform alien in aliens) {
                    alien.gameObject.SetActive(true);
                }
                alienated = true;
            }

            float positionAliensY = transform.position.y;
            float loweringSpeed = (positionAliensY + 25.58f ) / 2.0f;
            UnityEngine.Debug.Log("positionAliensY: " + positionAliensY + " / loweringSpeed: " + loweringSpeed);

            if (loweringSpeed < 0.01f) {
                return ;
            }
            else if (loweringSpeed < 3.0f) {
                loweringSpeed = 3.0f;
            }
            transform.Translate(new Vector3(0, -loweringSpeed, 0) * Time.deltaTime, Space.World);

            // y target: -25.58

        }
    }
}
