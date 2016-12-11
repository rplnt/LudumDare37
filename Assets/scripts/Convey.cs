using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convey : MonoBehaviour {

    public enum BeltMode {
        REVERSE = -1,
        OFF = 0,
        NORMAL = 1
    };

    public float speed;
    public BeltMode mode;

    HashSet<GameObject> packagesOnBelt;


    void Start() {
        packagesOnBelt = new HashSet<GameObject>();
        mode = BeltMode.NORMAL;
    }


    void Update() {
        if (mode == BeltMode.REVERSE && packagesOnBelt.Count == 0) {
            mode = BeltMode.NORMAL;
        }

        foreach (GameObject package in packagesOnBelt) {
            if (!package.GetComponent<PackageManager>().isHeld) {
                package.transform.Translate(new Vector2(0.0f, 1.0f) * speed * 1.93f * Time.deltaTime * (int)mode, Space.World);
            }
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
            PackageManager pm = col.gameObject.GetComponent<PackageManager>();
            pm.onBelt = true;
            pm.belt = this.transform;
            packagesOnBelt.Add(col.gameObject);
            
        }

    }


    void OnTriggerExit2D(Collider2D col) {
        //if (col.attachedRigidbody.CompareTag("package")) {
        //    packagesOnBelt.Remove(col.attachedRigidbody.gameObject);
        //    col.attachedRigidbody.velocity = Vector2.zero;
        //    col.attachedRigidbody.isKinematic = false;
        //    col.attachedRigidbody.gameObject.GetComponent<PackageManager>().onBelt = false;
        //}

        //if (col.gameObject.CompareTag("package") && col.transform.parent != this.transform) {
        if (col.gameObject.CompareTag("package")) {
            packagesOnBelt.Remove(col.gameObject);
            PackageManager pm = col.gameObject.GetComponent<PackageManager>();
            pm.onBelt = false;
            pm.belt = null;
        }
    }
}
