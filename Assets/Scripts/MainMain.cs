using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMain : MonoBehaviour {

    public const int secondsBeforeRedPlanetMovement = 0;
    public const int secondsBeforeSunMovement1 = secondsBeforeRedPlanetMovement + 59;

    public const int secondsBeforeRocket1Liftoff = 40;
    public const int secondsBeforeRocket2Liftoff = 31;

    private long fun = 50;

    MyStopWatch myStopWatch = new MyStopWatch();
    
    void Start() {
    }

    void FixedUpdate()
    {
        updateHud(myStopWatch.getElapsedSeconds());
    }

    private void updateHud(long currentSecondsCounter) {
    
        if (currentSecondsCounter > secondsBeforeSunMovement1 + 5) {
            fun = 81 - ( 2 * (currentSecondsCounter - (secondsBeforeSunMovement1 + 5)) );
        }
        else {
            if (currentSecondsCounter % 5 == 0) {
                fun = 50 + currentSecondsCounter;
            }
        }
        renderFun(fun);

        if (currentSecondsCounter % 3 == 0) {
            long productivity = 15 + (13 * currentSecondsCounter);
            GameObject.Find("Productivity")
                .GetComponentInChildren<InputField>()
                .text = "Productivity: " + productivity + "%";
        }
    }

    private void renderFun(long fun) {
        if (fun < 101 && fun > 6) {
            GameObject.Find("Fun")
                .GetComponentInChildren<InputField>()
                .text = "Fun: " + fun + "%";
        }
    }

}
