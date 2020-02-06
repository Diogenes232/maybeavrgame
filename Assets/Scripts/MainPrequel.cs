using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPrequel : MonoBehaviour {

    void Start()
    {
        updateHud();
    }

    void FixedUpdate() {
    }

    private void updateHud() {

        var text = "Episode IV, A NEW HOPE It is a period of civil war. Rebel spaceships, striking from a hidden base, have won their first victory against the evil Galactic Empire. " 
            + "During the battle, Rebel spies managed to steal secret plans to the Empire’s ultimate weapon, the DEATH STAR, an armored space station with enough power to destroy an entire planet. "
            + "Pursued by the Empire’s sinister agents, Princess Leia races home aboard her starship, custodian of the stolen plans that can save her people and restore freedom to the galaxy...";
        
        GameObject story = GameObject.Find("Story");
        story.GetComponentInChildren<Text>().text = text;

        Rigidbody compRb = GetComponent<Rigidbody>();
        compRb.AddForce(0, 0.3f, 0.3f, ForceMode.Impulse);
    }

}
