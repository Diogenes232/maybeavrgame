using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Rocket1 : MonoBehaviour
{
    MyStopWatch myStopWatch = new MyStopWatch();
    
    float speed_y = 2 / 3;
    float speed_x = -2.0f;
    float speed_z = 0.12f;
    const float speedAccelerator = 1.05f;

    float rotate_y_beforeLiftoff = -20.0f;
    const int rotate_timeoutBeforeLiftoff = 1;
    bool wasRocketRotatedBeforeLiftoff = false;

    void FixedUpdate() {

        long currentSecondsCounter = myStopWatch.getElapsedSeconds();

        adaptCountdownField(currentSecondsCounter);

        rotateRocket(currentSecondsCounter);

        if (currentSecondsCounter < MainMain.secondsBeforeRocket1Liftoff) {
            return;
        }

        moveRocket(currentSecondsCounter);
        accelerateRocket();

        checkIfRocketIsToDestroy();
    }

    private void rotateRocket(long currentSecondsCounter) {
        if (wasRocketRotatedBeforeLiftoff || currentSecondsCounter < MainMain.secondsBeforeRocket1Liftoff - rotate_timeoutBeforeLiftoff) {
            return;
        }
        // once
        Vector3 rot = new Vector3(0.0f, rotate_y_beforeLiftoff, 0.0f);
        transform.Rotate(rot * Time.deltaTime * 20, Space.World);
        wasRocketRotatedBeforeLiftoff = true;
    }

    private void adaptCountdownField(long currentSecondsCounter) {

        // show countdown when other rocket was launched
        if (currentSecondsCounter <= (MainMain.secondsBeforeRocket2Liftoff + 2)) {
            return;
        }

        long count = (MainMain.secondsBeforeRocket1Liftoff - currentSecondsCounter);
        if (count < 1) {
            count = 0;
        }
        //UnityEngine.Debug.Log("");
        GameObject.Find("Liftoff counter 1")
            .GetComponentInChildren<InputField>()
            .text = "T minus " + count + " seconds..";            
    }

    private void moveRocket(long currentSecondsCounter) {
        if (currentSecondsCounter - MainMain.secondsBeforeRocket1Liftoff < 1) {
            speed_z = 0.0f;
        }

        // up
        transform.Translate( (new Vector3(0, speed_y, 0)) * Time.deltaTime, Space.World );
        // to the right / speed_z: how much to the "left"
        transform.Translate( (new Vector3(speed_x, 0, speed_z)) * Time.deltaTime );
    }

    private void accelerateRocket() {
        speed_y *= speedAccelerator;
        speed_x *= speedAccelerator;
    }

    private void checkIfRocketIsToDestroy() {
        if (transform.position.x + transform.position.y > 20000.0f) {
            Destroy(transform.gameObject, 1);
        }
    }
    
    void Start(){}

}
