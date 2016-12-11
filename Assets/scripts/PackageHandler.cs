using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageHandler : MonoBehaviour {

    HashSet<GameObject> withinReach;
    GameObject targetPackage = null;
    public GameObject holding = null;
    

	// Use this for initialization
	void Start () {
        withinReach = new HashSet<GameObject>();
	}


    void Update() {
        if (holding == null && targetPackage == null) {
            float minDistance = Mathf.Infinity;
            GameObject closest = null;
            foreach (GameObject package in withinReach) {
                // find the closest package
                if (Vector2.Distance(this.transform.position, package.transform.position) < minDistance) {
                    minDistance = Vector2.Distance(this.transform.position, package.transform.position);
                    closest = package;
                }
            }

            if (closest != null) {
                closest.GetComponent<PackageManager>().HighlightPackage(true);
                targetPackage = closest;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftControl)) {
            if (targetPackage != null && holding == null) {
                PickupPackage();
            } else if (holding != null) {
                DropPackage();
            }
        }
    }


    void PickupPackage() {
        holding = targetPackage;
        Rigidbody2D rb = holding.GetComponent<Rigidbody2D>();
        if (rb != null) {
            Destroy(rb);
        }

        PackageManager pm = holding.GetComponent<PackageManager>();
        pm.PickUp();

        // TODO move package closer
        holding.transform.SetParent(this.transform);
    }

    void DropPackage() {
        holding.transform.SetParent(null);
        holding.GetComponent<PackageManager>().Drop();
        holding = null;
        targetPackage = null;
    }


    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("package")) {
            withinReach.Add(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.CompareTag("package")) {
            withinReach.Remove(col.gameObject);
            if (targetPackage == col.gameObject) {
                targetPackage.GetComponent<PackageManager>().HighlightPackage(false);
                targetPackage = null;
            }
        }
    }
}
