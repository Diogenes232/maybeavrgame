﻿using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class SeekingChild2 : MonoBehaviour
{
    MyStopWatch myStopWatch = new MyStopWatch();
    GameObject head;

    // current direction
    public bool xMoveMinus = false;
    public bool zMoveMinus = false;
    public bool xMovePlus = false;
    public bool zMovePlus = true;
    public int noOfAccelerations = 0;

    // speed
    const int secondsBeforeStarting = 1;
    static float initialSpeed_x_z = 2.0f;
    static float maxSpeed_x_z = 50.0f;
    static float speed_x_z;
    static float speedAcceleratorFactor = 1.2f;
    static bool speed_x_z_accelerate = true;

    // stop points
    float xLowerStopPosition = 6.5f;
    float xUpperStopPosition = 14.5f;
    float zLowerStopPosition = -2.7f;
    float zUpperStopPosition = 2.0f;

    void FixedUpdate() {   

        if (head == null || myStopWatch.getElapsedSeconds() < secondsBeforeStarting) {
            // gameObject was destroyed or too soon
            return;
        }

        float position_x = transform.position.x;
        float position_z = transform.position.z;

        if (xMoveMinus && position_x < xLowerStopPosition) {
            // phase1
            xMoveMinus = false;
            zMoveMinus = true;
            changeSpeed();
        } else if (zMoveMinus && position_z < zLowerStopPosition) {
            // phase2
            zMoveMinus = false;
            xMovePlus = true;
            changeSpeed();
        } else if (xMovePlus && position_x > xUpperStopPosition) {
            // phase3
            xMovePlus = false;
            zMovePlus = true;
            changeSpeed();
        } else if (zMovePlus && position_z > zUpperStopPosition) {
            // phase4
            zMovePlus = false;
            xMoveMinus = true;
            changeSpeed();
        } 

        if (xMoveMinus) {
            transform.Translate(speed_x_z * new Vector3(0, 0, -1) * Time.deltaTime);
            head.transform.Translate(speed_x_z * new Vector3(0, 0, -1) * Time.deltaTime);
        } else if (zMoveMinus) {
            transform.Translate(speed_x_z * new Vector3(0, -1, 0) * Time.deltaTime);
            head.transform.Translate(speed_x_z * new Vector3(0, -1, 0) * Time.deltaTime);
        } else if (xMovePlus) {
            transform.Translate(speed_x_z * new Vector3(0, 0, 1) * Time.deltaTime);
            head.transform.Translate(speed_x_z * new Vector3(0, 0, 1) * Time.deltaTime);
        } else if (zMovePlus) {
            transform.Translate(speed_x_z * new Vector3(0, 1, 0) * Time.deltaTime);
            head.transform.Translate(speed_x_z * new Vector3(0, 1, 0) * Time.deltaTime);
        }

    }

    private void changeSpeed() {
        if (MainMain.isDownTempoPhase()) {
            speed_x_z = MainMain.calcDownTempo();
        }
        else {
            if (noOfAccelerations < 1) {
                noOfAccelerations += 1;
                return;
            }

            if (speed_x_z_accelerate) {
                speed_x_z = speedAcceleratorFactor * speed_x_z;
                if (speed_x_z > maxSpeed_x_z) {
                    speed_x_z_accelerate = false;
                }
            } else {
                speed_x_z = speed_x_z / speedAcceleratorFactor;
                if (speed_x_z <= initialSpeed_x_z) {
                    speed_x_z_accelerate = true;
                }
            }
        }
    }

    void Start()
    {
        speed_x_z = SeekingChild2.initialSpeed_x_z;
        head = GameObject.Find("Childish head 2");
    }
}