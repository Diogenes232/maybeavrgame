using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using UnityEngine;

public class Alienation : MonoBehaviour {

    List<Transform> aliens = new List<Transform>();
    
    bool aliensComing = false;
    bool aliensWereNearlyHere = false;
    bool aliensHere = false;
    bool aliensWereHere = false;

    bool aliensSoundPlayed = false;
    bool windSoundActivated = false;

    float rotationFactor = 1.0f;
    bool wasCameraRotatedHalf = false;
    int fullCameraRotations = 0;
    
    float aliensDistanceToGround = 500.0f;
    float firstSpeedX;
    float firstSpeedY;

    void Start() {
        foreach (Transform child in transform) {
            aliens.Add(child);
        }
    }

    void FixedUpdate() {
        if (MainMain.myStopWatch.execeedesSeconds(MainMain.secondsBeforeAlienation)) {
            if (!aliensComing && !aliensHere && !aliensWereHere) {
                activateAliens(true);
            }

            if (aliensWereNearlyHere) {
                // MainMain.stopMusic();
            }
        }

        playSomeAlienSounds();

        if (aliensComing && !aliensHere && !aliensWereHere) {
            moveAliensDownwards();
        }

        checkForFinalAlienationCameraMovement();
    }

    private void checkForFinalAlienationCameraMovement() {
        if (MainMain.isAlienationCameraMovementPhase()) {
            GameObject visitorCamera = GameObject.Find("Visitor Camera Holder");
            
            if (fullCameraRotations >= 2) {
                endGame();
            }

            // ROTATION
            float rotationStateY = Math.Abs(visitorCamera.transform.rotation.y);
            if (rotationStateY > 0.95f) {
                wasCameraRotatedHalf = true;
                rotateCamera(visitorCamera, rotationFactor);
                //checkOneTimeRotationToLookDown(visitorCamera);
            }
            else if (wasCameraRotatedHalf && rotationStateY > 0.0f && rotationStateY < 0.2) {
                fullCameraRotations = fullCameraRotations + 1;
                wasCameraRotatedHalf = false;
                UnityEngine.Debug.Log("fullCameraRotations: " + fullCameraRotations);

                //rotationFactor = 1.0f;
                rotateCamera(visitorCamera, rotationFactor);
            }
            else {
                rotateCamera(visitorCamera, rotationFactor);
            }            
            
            // MOVEMENT
            // sidewards
            float differenceToTargetX = 4.0f - visitorCamera.transform.position.x;
            float speedX = (differenceToTargetX + 1.0f ) / 10.0f;
            speedX = checkLowerSpeedBounds(differenceToTargetX, speedX);
            
            // upwards
            float differenceToTargetY = 4.0f - visitorCamera.transform.position.y;
            float speedY = (differenceToTargetY + 0.5f) / 10.0f;
            speedY = checkLowerSpeedBounds(differenceToTargetY, speedY);

            if (fullCameraRotations > 0 || wasCameraRotatedHalf) {
                // faster
                speedY = 10.0f;
                moveAliensHorizontallyWithSpeed(speedY);
            }

            //UnityEngine.Debug.Log("speedX: " + speedX + "  speedY: " + speedY);
            visitorCamera.transform.Translate(new Vector3(speedX, speedY, 0) * Time.deltaTime, Space.World);
        }
    }

    private void playSomeAlienSounds() {
        if (aliensComing && !aliensHere && !aliensWereHere && aliensWereNearlyHere && !aliensSoundPlayed) {
            GameObject.Find("DarkZounds").GetComponent<AudioSource>().Play();
            aliensSoundPlayed = true;
            ALIENS();
        }

        if (wasCameraRotatedHalf && !windSoundActivated) {
            GameObject.Find("Wind").GetComponent<AudioSource>().Play();
            windSoundActivated = true;
        }
    }

    private void checkOneTimeRotationToLookDown(GameObject visitorCamera) {
        if (fullCameraRotations == 0) {
            visitorCamera.transform.Rotate(0.0f, 0.0f, -0.1f, Space.Self);
        } else if (fullCameraRotations == 1) {
            visitorCamera.transform.Rotate(0.0f, 0.0f, 0.1f, Space.Self);
        }
    }

    private void ALIENS() {
        GameObject.Find("Aliens").GetComponent<AudioSource>().Play();
    }

    private void endGame() {
        activateAliens(false);
                
        List<GameObject> gos = MainMain.getGameObjectsContaining("The end canvas");
        foreach (var go in gos) {
            go.SetActive(true);
        }
        aliensWereHere = true;

        ALIENS();
        rotationFactor = 0.66f;
    }

    private void rotateCamera(GameObject visitorCamera) {
        rotateCamera(visitorCamera, 1.0f);
    }

    private void rotateCamera(GameObject visitorCamera, float rotationFactor) {
        visitorCamera.transform.Rotate(0.0f, rotationFactor * 0.35f, 0.0f, Space.Self);
    }

    private float checkLowerSpeedBounds(float differenceToTarget, float speedTowardsTarget) {
        if (Math.Abs(differenceToTarget) < 0.1f) {
            return 0.0f;
        }
        return speedTowardsTarget;
    }

    private void activateAliens(bool activate) {
        UnityEngine.Debug.Log("Alienation: " + activate);
        foreach (Transform alien in aliens) {
            alien.gameObject.SetActive(activate);
        }
        aliensComing = activate;
    }

    private void moveAliensDownwards() {
        float speedY = calcAliensSpeedY();
        moveAliensHorizontallyWithSpeed(speedY);
    }

    private void moveAliensHorizontallyWithSpeed(float speedY) {
        transform.Translate(new Vector3(0, speedY, 0) * Time.deltaTime, Space.World);
    }

    private float calcAliensSpeedY() {
        const float targetPositionY = -25.58f;
        aliensDistanceToGround = targetPositionY - transform.position.y;
        float speedTowardsTarget = aliensDistanceToGround / 2.0f;

        
        if (Math.Abs(aliensDistanceToGround) < 8.0f) {
            aliensWereNearlyHere = true;
        }

        if (Math.Abs(speedTowardsTarget) < 0.01f) {
            aliensHere = true;
            aliensComing = false;
            return 0.0f;
        }
        else if (speedTowardsTarget < 3.0f && speedTowardsTarget >= 0.0f) {
            return 3.0f;
        }
        else if (speedTowardsTarget > -3.0f && speedTowardsTarget <= 0.0f) {
            return -3.0f;
        }
        return speedTowardsTarget;
    }

}
