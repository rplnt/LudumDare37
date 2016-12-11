using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateBelt : MonoBehaviour {

    float offset = 0;
    Renderer rend;
    Convey belt;

	// Use this for initialization
	void Start () {
        rend = this.GetComponent<Renderer>();
        belt = this.GetComponentInParent<Convey>();
	}
	
	// Update is called once per frame
	void Update () {
        if (belt.mode == 0) return;
        offset += belt.speed * Time.deltaTime * (int)belt.mode * -1.0f;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0.0f, offset));
	}
}
