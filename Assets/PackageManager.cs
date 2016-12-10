using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageManager : MonoBehaviour {

    SpriteRenderer highlight;
    SpriteRenderer shadow;

    public bool isHeld;

	// Use this for initialization
	void Start () {
        highlight = this.transform.FindChild("Highlight").GetComponent<SpriteRenderer>();
        shadow = this.transform.FindChild("Shadow").GetComponent<SpriteRenderer>();
        isHeld = false;
	}
	
	// Update is called once per frame
	void Update () {
	}


    public void HighlightPackage(bool on) {
        StartCoroutine(HighlightInOut(0.25f, !on));
    }


    IEnumerator HighlightInOut(float duration, bool fadeOut) {
        float elapsed = 0.0f;
        float a = fadeOut ? 1.0f : 0.0f;
        float b = fadeOut ? 0.0f : 1.0f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            highlight.color = new Color(highlight.color.r, highlight.color.g, highlight.color.b, Mathf.Lerp(a, b, elapsed / duration));
            yield return null;
        }

    }


    public void PickUpPackage() {

    }
}
