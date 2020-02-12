using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMain : MonoBehaviour {

    public static MyStopWatch myStopWatch = new MyStopWatch();
    private static List<GameObject> activeChildren = new List<GameObject>();
    private long fun = 70;

    // camera movement
    private static GameObject visitorCamera;
    private bool isInAlienationCameraMovement = false;

    // early phase
    public const int secondsBeforeRedPlanetMovement = 0;
    public const int secondsBeforeSunMovement1 = secondsBeforeRedPlanetMovement + 59;

    // in between
    public const int secondsBeforeRocket1Liftoff = 40;
    public const int secondsBeforeRocket2Liftoff = 31;
    
    // first ending phase
    private const int secondsBeforeKidsNegativeAcceleration = 130;
    private const float secondsBeforeKidsAreCooledDown = 10.0f;
    
    // ending ending phase
    //public const int secondsBeforeAlienation = secondsBeforeKidsNegativeAcceleration + (((int)(secondsBeforeKidsAreCooledDown * 1.00f)) );
    public const int secondsBeforeAlienation = 10;
    public const int secondsOfChildAlienation = 2;
    public static int noOfChildrenInAlienation = 0;
    
    void Start() {
        enableVisitorCameraIfProd();
        updateActiveChildren();
    }

    void FixedUpdate()
    {
        updateHud(myStopWatch.getElapsedSeconds());
        updateActiveChildren();
        checkForAlienationCameraMovement();
    }

    /* Check if ending downtempo time has arrived. */
    public static bool isDownTempoPhase() {
        return myStopWatch.execeedesSeconds(secondsBeforeKidsNegativeAcceleration);
    }

    public static bool isAlienationPhase() {
        return myStopWatch.execeedesSeconds(secondsBeforeAlienation);
    }

    public static float calcDownTempo() {
        long secondsOver = (myStopWatch.getElapsedSeconds() - secondsBeforeKidsNegativeAcceleration);

        float speed = 4 * (1 - (secondsOver / secondsBeforeKidsAreCooledDown));
        if (speed < 0) {
            speed = 0;
        }
        return speed;
    }

    private void checkForAlienationCameraMovement() {
        if (activeChildren.Count < 3 && !isInAlienationCameraMovement) {
            UnityEngine.Debug.Log("camera movement starting now..");
            visitorCamera.transform.Translate(new Vector3(0, 3, 0) * Time.deltaTime, Space.World);
            isInAlienationCameraMovement = true;
        }
    }

    private void updateHud(long currentSecondsCounter) {
    
        if (currentSecondsCounter > secondsBeforeSunMovement1 + 5) {
            fun = 81 - ( 2 * (currentSecondsCounter - (secondsBeforeSunMovement1 + 5)) );
        }
        else {
            if (currentSecondsCounter % 5 == 0) {
                fun = 70 + currentSecondsCounter;
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
        if (fun < 101 && fun > 0) {
            GameObject.Find("Fun")
                .GetComponentInChildren<InputField>()
                .text = "Fun: " + fun + "%";
        }
    }

    public static void enableVisitorCameraIfProd() {
        bool isEditor = Application.isEditor;

        var cameras = Resources.FindObjectsOfTypeAll<Camera>();
        UnityEngine.Debug.Log("cameras overall: " + cameras.Length + " / isEditor: " + isEditor);

        if (!isEditor) {
            foreach (var cam in cameras) {
                if (cam.name == "Visitor Camera") {
                    cam.gameObject.SetActive(true);
                    visitorCamera = cam.gameObject;
                }
                else {
                    cam.gameObject.SetActive(false);
                }
                
            }
        }
    }

    private void updateActiveChildren() {
        var gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        activeChildren.Clear();
        foreach (var go in gameObjects) {
            if (go.name.Contains(" child ")) {
                if (go.activeSelf) {
                    activeChildren.Add(go);
                }
            }
        }
    }

}
