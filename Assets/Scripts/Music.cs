using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    AudioSource music;

    public void playMusic() {
        music = GetComponent<AudioSource>();
        music.Play();
    }
    
    void Start() {
        playMusic();

        DontDestroyOnLoad(this.gameObject);
    }

    void Update() {   
        if (MainMain.isMusicToStop()) {
            music.Stop();
        }     
    }
}
