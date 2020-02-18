using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSporadicSound : MonoBehaviour {

    MyStopWatch stopWatch = new MyStopWatch(true);
    float durationToSoundLiftoff = -1.0f;

    public float minDurationToSound = 250.0f;
    public float maxDurationToSound = 15000.0f;

    void Start() {
        
    }

    void Update() {

        if (durationToSoundLiftoff < 0.0f){
            durationToSoundLiftoff = randomFloat(minDurationToSound, maxDurationToSound);
        }

        if ( stopWatch.execeedesMilliseconds((int)durationToSoundLiftoff) ){
            AudioSource liftoffSound = GetComponent<AudioSource>();
            liftoffSound.Play();

            durationToSoundLiftoff = -1.0f;
            stopWatch.reset();
        }
        
    }

    private float randomFloat(float min, float max) {
        return UnityEngine.Random.Range(min, max);
    }
}
