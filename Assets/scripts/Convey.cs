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
    public bool input;

    public float minPenalty;
    float penaltyTimer = 0.0f;

    HashSet<GameObject> packagesOnBelt;


    void Start() {
        packagesOnBelt = new HashSet<GameObject>();
        mode = BeltMode.NORMAL;
    }


    public void RemovePackageFromBelt(GameObject package) {
        packagesOnBelt.Remove(package);
        PackageManager pm = package.gameObject.GetComponent<PackageManager>();
        pm.onBelt = false;
        pm.belt = null;
    }


    void Update() {

        if (!input && mode == BeltMode.REVERSE) {
            penaltyTimer += Time.deltaTime;

            if (penaltyTimer >= minPenalty && packagesOnBelt.Count == 0) {
                mode = BeltMode.NORMAL;
                penaltyTimer = 0.0f;
            }
            
        }


        foreach (GameObject package in packagesOnBelt) {
            if (!package.activeInHierarchy) {
                Debug.LogError("Inactive package on belt found: Should not happen!");
                continue;
            }
            if (!package.GetComponent<PackageManager>().isHeld) {
                package.transform.Translate(new Vector2(0.0f, 1.0f) * speed * 1.77f * Time.deltaTime * (int)mode, Space.World);
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
            RemovePackageFromBelt(col.gameObject);
        }
    }
}
