using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Rocket1 : MonoBehaviour
{
    MyStopWatch myStopWatch = new MyStopWatch();
    
    float speed_y = 2 / 3;
    float speed_x = 2f;
    const float speedAccelerator = 1.05f;

    void FixedUpdate()
    {
        long currentSecondsCounter = myStopWatch.getElapsedSeconds();

        adaptCountdownField(currentSecondsCounter);

        if (currentSecondsCounter < Main.secondsBeforeRocket1Liftoff) {
            return;
        }

        moveRocket();
        accelerateRocket();
    }

    private void adaptCountdownField(long currentSecondsCounter) {
        long count = (Main.secondsBeforeRocket1Liftoff - currentSecondsCounter);
        if (count < 1) {
            count = 0;
        }
        GameObject.Find("Liftoff counter")
            .GetComponentInChildren<InputField>()
            .text = "T minus " + count + " seconds..";            
    }

    private void moveRocket() {
        // up
        transform.Translate( (new Vector3(0, speed_y, 0)) * Time.deltaTime, Space.World);
        // to the right
        transform.Translate( (new Vector3(speed_x, 0, 0)) * Time.deltaTime);
    }

    private void accelerateRocket() {
        speed_y *= speedAccelerator;
        speed_x *= speedAccelerator;
    }
    
    void Start() {}

}
