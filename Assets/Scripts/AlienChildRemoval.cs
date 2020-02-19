using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

public class AlienChildRemoval : MonoBehaviour
{
    MyStopWatch alienationStopWatch;
    GameObject head, body, childEndingSound;

    // end phase
    bool isAlienated = false;

    void FixedUpdate() {
        checkAlienation();
    }

    private void checkAlienation() {
        if (MainMain.isAlienationPhase()) {

            // we are alienated right now
            if (alienationStopWatch != null && alienationStopWatch.isRunning()) {

                long myAlienationDuration = (int) ((Convert.ToSingle(MainMain.secondsOfChildAlienation)*1000.0f) + (1500.0f*randomFloat(0.0f, 1.0f)));
                if (alienationStopWatch.execeedesMilliseconds(myAlienationDuration)) {
                    // we gonna end this
                    playSoundOfADyingChild();
                    destroyGameObjectsParent(body);
                    alienationStopWatch.stop();

                    isAlienated = true;
                    MainMain.noOfChildrenInAlienation--;
                }
                else {
                    // we stay in that phase
                    setGameObjectVisible( (randomChanceOccurs(0.5f) ? true : false) );
                }
            }
            // start my alienation
            else if (MainMain.noOfChildrenInAlienation < 2 && isAlienated == false) {
                MainMain.noOfChildrenInAlienation++;
                alienationStopWatch = new MyStopWatch();
                setGameObjectVisible(false);
            }
        }
    }

    private float randomFloat(float min, float max) {
        return UnityEngine.Random.Range(min, max);
    }

    private bool randomChanceOccurs(float chance) {
        return (randomFloat(0.0f, 1.0f) <= chance);
    }

    private void setGameObjectVisible(bool b) {
        head.SetActive(b);
        body.SetActive(b);
    }

    private void destroyGameObjectsParent(GameObject a) {
        Destroy(a.transform.parent.gameObject, 0);
    }

    private void playSoundOfADyingChild() {
        childEndingSound.GetComponent<AudioSource>().Play();
    }

    void Start()
    {
        // find children nodes
        foreach (Transform child in transform) {
            if (child.name == "Head" || child.name.Contains("head")) {
                head = child.gameObject;
            } else if (child.name == "Body" || child.name.Contains("body")) {
                body = child.gameObject;
            }
        }
        childEndingSound = GameObject.Find("ChildEndingSound");
    }

}