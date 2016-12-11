using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convey : MonoBehaviour {

    public float speed;
    public bool reverse;

    HashSet<GameObject> packagesOnBelt;


    void Start() {
        packagesOnBelt = new HashSet<GameObject>();
    }


    void Update() {
        if (reverse && packagesOnBelt.Count == 0) {
            Reverse();
        }

        foreach (GameObject package in packagesOnBelt) {
            if (!package.GetComponent<PackageManager>().isHeld) {
                package.transform.Translate(new Vector2(0.0f, -1.0f) * speed * 1.93f * Time.deltaTime, Space.World);
            }
        }
    }


    void Reverse() {
        reverse = !reverse;
        foreach (GameObject package in packagesOnBelt) {
            speed = speed * -1.0f;
            //package.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -1.0f) * speed * 1.5f;
        }
    }


    void OnTriggerEnter2D(Collider2D col) {
        //if (col.attachedRigidbody.CompareTag("package")) {
        //    packagesOnBelt.Add(col.attachedRigidbody.gameObject);
        //    //col.attachedRigidbody.velocity = new Vector2(0.0f, -1.0f) * speed * 1.93f;
        //    col.attachedRigidbody.isKinematic = true;
        //    col.attachedRigidbody.gameObject.GetComponent<PackageManager>().onBelt = true;
        //}

        if (col.gameObject.CompareTag("package")) {
            packagesOnBelt.Add(col.gameObject);
            //col.attachedRigidbody.velocity = new Vector2(0.0f, -1.0f) * speed * 1.93f;
            col.gameObject.GetComponent<PackageManager>().onBelt = true;
        }

    }


    void OnTriggerExit2D(Collider2D col) {
        //if (col.attachedRigidbody.CompareTag("package")) {
        //    packagesOnBelt.Remove(col.attachedRigidbody.gameObject);
        //    col.attachedRigidbody.velocity = Vector2.zero;
        //    col.attachedRigidbody.isKinematic = false;
        //    col.attachedRigidbody.gameObject.GetComponent<PackageManager>().onBelt = false;
        //}

        if (col.gameObject.CompareTag("package")) {
            packagesOnBelt.Remove(col.gameObject);
            col.gameObject.GetComponent<PackageManager>().onBelt = false;
        }
    }
}
