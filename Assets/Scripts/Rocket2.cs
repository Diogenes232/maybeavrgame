using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Rocket2 : MonoBehaviour
{
    MyStopWatch myStopWatch = new MyStopWatch();
    
    float speed_y = 2 / 3;
    float speed_x = -2.0f;
    float speed_z = 0.06f;
    const float speedAccelerator = 1.05f;

    float rotate_y_beforeLiftoff = -12.0f;
    const int rotate_timeoutBeforeLiftoff = 1;
    bool wasRocketRotatedBeforeLiftoff = false;

    void FixedUpdate()
    {
        long currentSecondsCounter = myStopWatch.getElapsedSeconds();

        adaptCountdownField(currentSecondsCounter);
        
        rotateRocket(currentSecondsCounter);

        if (currentSecondsCounter < MainMain.secondsBeforeRocket2Liftoff) {
            return;
        }

        moveRocket(currentSecondsCounter);
        accelerateRocket();
    }

    private void rotateRocket(long currentSecondsCounter) {
        if (wasRocketRotatedBeforeLiftoff || currentSecondsCounter < MainMain.secondsBeforeRocket2Liftoff - rotate_timeoutBeforeLiftoff) {
            return;
        }
        // once
        Vector3 rot = new Vector3(0.0f, rotate_y_beforeLiftoff, 0.0f);
        transform.Rotate(rot * Time.deltaTime * 20, Space.World);
        wasRocketRotatedBeforeLiftoff = true;
    }

    private void adaptCountdownField(long currentSecondsCounter) {
        long count = (MainMain.secondsBeforeRocket2Liftoff - currentSecondsCounter);
        if (count < 1) {
            count = 0;
        }
        //UnityEngine.Debug.Log("");
        GameObject.Find("Liftoff counter 2")
            .GetComponentInChildren<InputField>()
            .text = "T minus " + count + " seconds..";            
    }

    private void moveRocket(long currentSecondsCounter) {
        if (currentSecondsCounter - MainMain.secondsBeforeRocket2Liftoff < 1) {
            speed_z = 0.0f;
        }

        // up
        transform.Translate( (new Vector3(0, speed_y, 0)) * Time.deltaTime, Space.World );
        // to the right
        transform.Translate( (new Vector3(speed_x, 0, speed_z)) * Time.deltaTime );
    }

    private void accelerateRocket() {
        speed_y *= speedAccelerator;
        speed_x *= speedAccelerator;
    }
    
    void Start() {}

}
