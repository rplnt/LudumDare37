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
    public int difficulty;
    float sinceSpawn;
    int spawned = 0;

    void Start() {
        speaker = this.GetComponent<AudioSource>();
        siren = this.GetComponentInChildren<SirenIn>();
        sinceSpawn = spawnDelay / 2.0f;
    }
    

    void Update() {
        sinceSpawn += Time.deltaTime;

        if (sinceSpawn >= spawnDelay) {
            if (jammed > 0.1f) {
                sinceSpawn = spawnDelay / 2.0f;
                return;
            }
            sinceSpawn = 0.0f;
            GameManager.GM.SpawnedPackage(Spawn());
            spawned++;

            if (spawned % 5 == 0) {
                spawnDelay = Mathf.Min(5.0f, spawnDelay * 0.9f);
            }

            if (spawned % 10 == 0) {
                difficulty = Mathf.Max(difficulty + 1, countries.Length - 1);
            }
        }
    }

    public PackageManager Spawn() {
        spawned++;
        speaker.Play();

        GameObject go = PackagePrefab[Random.Range(0, PackagePrefab.Length - 1)];

        Country toSpawnCountry = null;
        while (toSpawnCountry == null) {
            Country c = countries[Random.Range(0, difficulty) * ((countries.Length - 1) / difficulty)];
            if (c == GameManager.GM.accepting) continue;
            toSpawnCountry = c;
        }

        Debug.Log("Spawning... " + toSpawnCountry.name);

        go = Instantiate(go, new Vector3(this.transform.position.x, this.transform.position.y, -1.1f), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        PackageManager pm = go.GetComponent<PackageManager>();
        pm.destination = toSpawnCountry;

        return pm;

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
