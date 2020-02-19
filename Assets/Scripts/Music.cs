using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    public void playMusic() {
        AudioSource music = GetComponent<AudioSource>();
        music.Play();
    }
    
    void Start() {
        playMusic();

        DontDestroyOnLoad(this.gameObject);
    }

    void Update() {        
    }
}
