using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamDetector : MonoBehaviour {

    public float timeout;
    float blocked = 0.0f;
    int jam;


    void FixedUpdate() {
        if (jam > 0) {
            blocked += Time.deltaTime;
        } else {
            blocked = 0.0f;
        }

        if (blocked > timeout) {
            // TODO Game Over
            Debug.Log("Input coneyor jammed, game over!");
        }

        jam = 0;
    }

	void OnTriggerStay2D(Collider2D col) {
        jam++;
    }
}
