using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class AutonomousChild : MonoBehaviour
{
    MyStopWatch myStopWatch = new MyStopWatch();
    private Rigidbody compRb;
    GameObject head, body;

    // speed
    const float minSpeed_x_z = 2.0f;
    const float maxSpeed_x_z = 15.0f;

    // find direction
    bool moveXplus, moveXminus, moveZplus, moveZminus;

    void FixedUpdate() {
        float position_x = head.transform.position.x;
        float position_z = head.transform.position.z;

        // when a border is passed
        if (!detectBorderTrespassing(position_x, position_z)) {
            if (randomFloat(0.0f, 1.0f) < 0.025f) {
                giveRandomDirectionToChild();
            }
        }
        
        float one = 1.0f;
        float moveX = randomFloat(-one, one);
        float moveZ = randomFloat(-one, one);

        if (moveXplus) {
            moveX = randomFloat(0.0f, one) * randomFloat(0, one);
        } else if (moveXminus) {
            moveX = randomFloat(-one, 0.0f) * randomFloat(0, one);
        }
        if (moveZplus) {
            moveZ = randomFloat(0.0f, one) * randomFloat(0, one);
        } else if (moveZminus) {
            moveZ = randomFloat(-one, 0.0f) * randomFloat(0, one);
        }

        // move
        moveBody( new Vector3(0, moveZ, moveX) );
    }

    private bool detectBorderTrespassing(float position_x, float position_z) {
        if (position_x > 16.0f) {
            moveXplus = false;
            moveXminus = true;
            return true;
        } else if (position_x < 5.5f) {            
            moveXplus = true;
            moveXminus = false;
            return true;
        }
        if (position_z > 3.5f) {
            moveZminus = true;
            moveZplus = false;
            return true;
        } else if (position_z < -3.5f) {
            moveZminus = false;
            moveZplus = true;
            return true;
        }
        return false;
    }

    private void moveBody(Vector3 v) {
        float speed_x_z = getRandomizedSpeed();
        head.transform.Translate(speed_x_z * v  * Time.deltaTime);
        body.transform.Translate(speed_x_z * v  * Time.deltaTime);
    }

    private float getRandomizedSpeed() {
        return minSpeed_x_z + (randomFloat(0.0f, 1.0f) * (maxSpeed_x_z - minSpeed_x_z));
    }

    private float randomFloat(float min, float max) {
        return UnityEngine.Random.Range(min, max);
    }

    private void giveRandomDirectionToChild() {
        if (randomFloat(0.0f, 1.0f) < 0.5f) {
            moveXplus = true;
            moveXminus = false;
        } else {
            moveXplus = false;
            moveXminus = true;
        }
        if (randomFloat(0.0f, 1.0f) < 0.5f) {
            moveZplus = true;
            moveZminus = false;
        } else {
            moveZplus = false;
            moveZminus = true;
        }
    }

    void Start()
    {
        // find children nodes
        foreach (Transform eachChild in transform) {
            if (eachChild.name == "Head") {
                head = eachChild.gameObject;
            } else if (eachChild.name == "Body") {
                body = eachChild.gameObject;
            }
        }

        giveRandomDirectionToChild();
    }

}