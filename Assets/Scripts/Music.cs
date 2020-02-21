using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    AudioSource music;

    private bool alienMusicOn = false;

    public void playMusic() {
        music = GetComponent<AudioSource>();
        music.Play();
    }
    
    void Start() {
        playMusic();

        DontDestroyOnLoad(this.gameObject);
    }

    void Update() {   
        if (MainMain.isToswitchToAlienMusic() && !alienMusicOn) {
            music.Stop();

            UnityEngine.Debug.Log("Switching to Alien's music channel");
            GameObject go = GameObject.Find("Alien Music");
            AudioSource alienMusic = go.GetComponent<AudioSource>();
            alienMusic.Play();

            alienMusicOn = true;
        }

        
    }
}
