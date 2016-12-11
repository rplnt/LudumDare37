using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenIn : MonoBehaviour {

    Light sirenLight;
    AudioSource sirenAudio;

    int beepCounter = 0;

    void Start() {
        sirenLight = this.GetComponent<Light>();
        sirenAudio = this.GetComponent<AudioSource>();
    }


    void Update () {
        if (beepCounter > 0 && sirenAudio.isPlaying == false) {
            sirenAudio.Play();
            beepCounter--;
            sirenLight.intensity = 3.0f;
        } else if (sirenAudio.isPlaying || sirenLight.intensity >= 0.01f) {
            sirenLight.intensity -= 2f * Time.deltaTime;
        } else {
            sirenLight.intensity = 0.0f;
        }
		
	}


    public void Beep() {
        beepCounter++;
    }

    public void StopBeep() {
        beepCounter = 0;
    }
}
