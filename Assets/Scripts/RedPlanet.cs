using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RedPlanet : MonoBehaviour {

    private MyStopWatch overallStopWatch = new MyStopWatch();

    private Rigidbody compRb;
    private Light compLight;

    bool hasDonePlanetMovement = false;

    void FixedUpdate()
    {
        long currentSecondsCounter = overallStopWatch.getElapsedSeconds();
        if (hasDonePlanetMovement || currentSecondsCounter < MainMain.secondsBeforeRedPlanetMovement) {
            return;
        }
        
        doPlanetMovement();
    }

    private void doPlanetMovement() {
        compRb.AddForce(2.5f, -1, 0, ForceMode.Impulse);
        hasDonePlanetMovement = true;
    }
    
    void Start() {
        compRb = GetComponent<Rigidbody>();
        compLight = GetComponent<Light>();

        //compRb.AddTorque(3.0f, 1.0f, -2.0f, ForceMode.Impulse);
    }

}
