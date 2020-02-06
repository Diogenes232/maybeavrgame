using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SunA : MonoBehaviour
{
    private MyStopWatch overallStopWatch = new MyStopWatch();
    private MyStopWatch longerSunShiningStopWatch = new MyStopWatch(false);

    private Rigidbody compRb;
    private Light compLight;

    const int secondsBeforeSunMovement1 = Main.secondsBeforeSunMovement1;
    const int secondsBeforeSunMovement2 = 9;
    bool hasDoneSunMovement1 = false;
    bool hasDoneSunMovement2 = false;

    void FixedUpdate()
    {
        long currentSecondsCounter = overallStopWatch.getElapsedSeconds();
        if (currentSecondsCounter < secondsBeforeSunMovement1) {
            return;
        }

        if (!longerSunShiningStopWatch.isRunning() || longerSunShiningStopWatch.execeedesMilliseconds(700)) {
            longerSunShiningStopWatch.stop();

            float randomIntensity = getRandomIntensity(currentSecondsCounter);

            setColor(randomIntensity);
        }
        
        moveTheSun(currentSecondsCounter);
    }

    private float getRandomIntensity(long currentSecondsCounter) {
        float randomIntensity;

        if (currentSecondsCounter < (secondsBeforeSunMovement1 + secondsBeforeSunMovement2 + 3)) {
            // start .. until starting movement2
            randomIntensity = randomFloat(0.0f, 1.0f);
        } else if (currentSecondsCounter > (secondsBeforeSunMovement1 + secondsBeforeSunMovement2 + 20)) {
            // end phase of movement2
            randomIntensity = randomFloat(0.0f, 0.3f);
        } else {
            // after starting movement2
            randomIntensity = randomFloat(0.0f, 0.5f);
        }

        if (randomIntensity >= 0.4f) {
            randomIntensity = 1.0f;

            // decide now if the sun gets a longer shining phase
            if (!longerSunShiningStopWatch.isRunning()) {
                if (randomFloat(0.0f, 1.0f) > 0.7f) {
                    longerSunShiningStopWatch = new MyStopWatch();
                }
            }

        } else {
            randomIntensity *= 0.6f;
        }

        return randomIntensity;   
    }

    private void moveTheSun(long currentSecondsCounter) {
        if (hasDoneSunMovement1 == false) {
            doSunMovement1();
            hasDoneSunMovement1 = true;
        } else if (hasDoneSunMovement1 == true
                && hasDoneSunMovement2 == false
                && currentSecondsCounter >= (secondsBeforeSunMovement1 + secondsBeforeSunMovement2)) {
            doSunMovement2();
            hasDoneSunMovement2 = true;
        }
    }

    private void setColor(float randomIntensity) {
        Material mat = GetComponent<Renderer>().material;

        // set color
        Color color = new Color(randomIntensity, randomIntensity, 0.0f);
        mat.color = color;
        mat.SetColor("_EmissionColor", color);
        // set intensity
        compLight.intensity = randomIntensity * 3.0f;
    }

    private float randomFloat(float min, float max) {
        return UnityEngine.Random.Range(min, max);
    }

    private void doSunMovement1() {
        compRb.AddForce(5, 1, 5, ForceMode.Impulse);
    }

    private void doSunMovement2() {
        // compRb.AddForce(4, 1.26f, 3.5f, ForceMode.Impulse);
        compRb.AddForce(-8, -2.52f, -7, ForceMode.Impulse);
    }
    
    void Start() {
        compRb = GetComponent<Rigidbody>();
        compLight = GetComponent<Light>();
    }

}
