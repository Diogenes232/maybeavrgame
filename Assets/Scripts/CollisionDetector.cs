using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionDetector : MonoBehaviour {

    AudioSource sound;
    float groundPositionY;

    bool touchingTheGround = false;

    void Start() {
        groundPositionY = GameObject.Find("Floor").transform.position.y;
        sound = GameObject.Find("BoxCollisionSound").GetComponent<AudioSource>();
    }

    void Update() {
        float distance = Math.Abs(groundPositionY - transform.position.y);

        if (distance > 0.2f) {
            touchingTheGround = false;
        }
        else {
            if (touchingTheGround == false) {
                sound.Play();
            }
            touchingTheGround = true;
        }
    }
}
