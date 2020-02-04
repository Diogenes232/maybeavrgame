using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    public const int secondsBeforeRedPlanetMovement = 0;
    public const int secondsBeforeSunMovement1 = secondsBeforeRedPlanetMovement + 63;

    public const int secondsBeforeRocket1Liftoff = 40;
    public const int secondsBeforeRocket2Liftoff = 31;

    MyStopWatch myStopWatch = new MyStopWatch();
    
    void Start() {
    }

    void FixedUpdate()
    {
        updateHud(myStopWatch.getElapsedSeconds());
    }

    private void updateHud(long currentSecondsCounter) {
        
        if (currentSecondsCounter % 5 == 0) {
            long fun = 50 + (currentSecondsCounter);
            if (fun < 101) {
                GameObject.Find("Fun")
                    .GetComponentInChildren<InputField>()
                    .text = "Fun: " + fun + "%";
            }
        }

        if (currentSecondsCounter % 3 == 0) {
            long productivity = 996 + (13 * currentSecondsCounter);
            GameObject.Find("Productivity")
                .GetComponentInChildren<InputField>()
                .text = "Productivity: " + productivity + "%";
        }
    }

}
