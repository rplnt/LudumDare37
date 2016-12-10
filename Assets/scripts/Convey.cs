using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convey : MonoBehaviour {

    public float speed;
    public bool reverse;

    List<GameObject> packagesOnBelt;

    void Start() {
        packagesOnBelt = new List<GameObject>();
    }

    void Update() {
        if (reverse && packagesOnBelt.Count == 0) {
            Reverse();
        }
    }


    void Reverse() {
        reverse = !reverse;
        foreach (GameObject package in packagesOnBelt) {
            speed = speed * -1.0f;
            package.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -1.0f) * speed * 1.93f;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.attachedRigidbody.CompareTag("package")) {
            packagesOnBelt.Add(col.attachedRigidbody.gameObject);
            col.attachedRigidbody.velocity = new Vector2(0.0f, -1.0f) * speed * 1.93f;
            col.attachedRigidbody.isKinematic = true;
        }
    }


    void OnTriggerExit2D(Collider2D col) {
        if (col.attachedRigidbody.CompareTag("package")) {
            packagesOnBelt.Remove(col.attachedRigidbody.gameObject);
            col.attachedRigidbody.velocity = Vector2.zero;
            col.attachedRigidbody.isKinematic = false;
        }
    }
}
