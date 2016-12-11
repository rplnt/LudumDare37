using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSpawner : MonoBehaviour {

    public GameObject[] PackagePrefab;
    public Country[] countries;



    public float jamTimeout;
    public float jamWarning;
    float jammed = 0.0f;
    SirenIn siren;
    int jam;

    AudioSource speaker;

    public float spawnDelay;
    float sinceSpawn = 0.0f;
    int spawned = 0;

    

    void Update() {
        sinceSpawn += Time.deltaTime;

        if (sinceSpawn >= spawnDelay) {
            sinceSpawn = 0.0f;
            GameManager.GM.SpawnedPackage(Spawn());
        }
    }

    public PackageManager Spawn() {
        Debug.Log("Spawning...");
        if (jammed > 0.1f) return null;
        speaker.Play();

        GameObject toSpawnGO = PackagePrefab[Random.Range(0, PackagePrefab.Length - 1)];

        Country toSpawnCountry = null;
        while (toSpawnCountry == null) {
            Country c = countries[Random.Range(0, countries.Length - 1)];
            if (c == GameManager.GM.accepting) continue;
            toSpawnCountry = c;
        }

        return null;

    }


    void Start() {
        speaker = this.GetComponent<AudioSource>();
        siren = this.GetComponentInChildren<SirenIn>();
    }

    void FixedUpdate() {
        if (jam > 0) {
            jammed += Time.deltaTime;
        } else {
            siren.StopBeep();
            jammed = 0.0f;
        }

        if (jammed > jamWarning) {
            siren.Beep();
        }

        if (jammed > jamTimeout) {
            // TODO Game Over
            Debug.Log("Input coneyor jammed, game over!");
        }

        jam = 0;
    }

    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.CompareTag("package")) {
            jam++;
        }
    }
}
