using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageManager : MonoBehaviour {

    Rigidbody2D rb;
    SpriteRenderer highlight;
    SpriteRenderer shadow;
    SpriteRenderer sprite;

    public bool isHeld;
    public bool onBelt;

	// Use this for initialization
	void Start () {
        highlight = this.transform.FindChild("Highlight").GetComponent<SpriteRenderer>();
        shadow = this.transform.FindChild("Shadow").GetComponent<SpriteRenderer>();
        sprite = this.transform.FindChild("Sprite").GetComponent<SpriteRenderer>();
        //rb = this.GetComponent<Rigidbody2D>();
        isHeld = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void PickUp() {
        isHeld = true;
        HighlightPackage(false);
        shadow.enabled = true;
        sprite.transform.localScale *= 1.1f;
        //rb.isKinematic = true;
    }

    public void Drop() {
        isHeld = false;
        shadow.enabled = false;
        //rb.velocity = Vector2.zero;
        sprite.transform.localScale /= 1.1f;
        if (!onBelt) {
            //rb.isKinematic = false;
        }
        
    }


    public void HighlightPackage(bool on) {
        StartCoroutine(HighlightInOut(0.25f, !on));
    }


    IEnumerator HighlightInOut(float duration, bool fadeOut) {
        float elapsed = 0.0f;
        float a = fadeOut ? 0.8f : 0.0f;
        float b = fadeOut ? 0.0f : 0.8f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            highlight.color = new Color(highlight.color.r, highlight.color.g, highlight.color.b, Mathf.Lerp(a, b, elapsed / duration));
            yield return null;
        }

    }


    public void PickUpPackage() {

    }
}
