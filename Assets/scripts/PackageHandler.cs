using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageHandler : MonoBehaviour {

    HashSet<GameObject> withinReach;
    GameObject targetPackage = null;
    public GameObject holding = null;

    public AudioClip pickup;
    public AudioClip drop;
    AudioSource player;
    

	// Use this for initialization
	void Start () {
        withinReach = new HashSet<GameObject>();
        player = this.GetComponent<AudioSource>();
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

            if (closest != null && minDistance < 1.0f) {
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

        PlaySound(pickup);
        PackageManager pm = holding.GetComponent<PackageManager>();
        pm.PickUp();
        // TODO move package closer
        holding.transform.SetParent(this.transform);
    }


    void PlaySound(AudioClip c) {
        player.clip = c;
        player.pitch = Random.Range(0.8f, 1.1f);
        player.Play();
    }


    void DropPackage() {
        holding.transform.SetParent(null);
        holding.GetComponent<PackageManager>().Drop();
        holding = null;
        targetPackage = null;
        PlaySound(drop);
    }


    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("package")) {
            withinReach.Add(col.gameObject);
        }
    }

    void NotInReach(GameObject package) {
        withinReach.Remove(package);
        package.GetComponent<PackageManager>().HighlightPackage(false);
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.CompareTag("package")) {
            NotInReach(col.gameObject);
            if (targetPackage == col.gameObject) {
                targetPackage = null;
            }
        }
    }
}
