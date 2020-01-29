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
        if (currentSecondsCounter < Main.secondsBeforeRedPlanetMovement) {
            return;
        }
        
        if (hasDonePlanetMovement == false) {
            doPlanetMovement();
            hasDonePlanetMovement = true;
        }
    }

    private void doPlanetMovement() {
        compRb.AddForce(2.5f, -1, 0, ForceMode.Impulse);
    }
    
    void Start() {
        compRb = GetComponent<Rigidbody>();
        compLight = GetComponent<Light>();
    }

}
