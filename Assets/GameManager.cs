using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    static GameManager _instance;
    public static GameManager GM {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<GameManager>(); ;
            }

            return _instance;
        }
    }

    public PackageSpawner spawner;
    public GameObject flagTV;
    SpriteRenderer tv;
    public Country accepting;

    AudioSource player;
    public AudioClip acceptChange;
    
    int score;

    Dictionary<Country, List<PackageManager>> packages;
    HashSet<Country> existing;

    void Start() {
        score = 0;
        accepting = null;
        packages = new Dictionary<Country, List<PackageManager>>();
        existing = new HashSet<Country>();
        player = this.GetComponent<AudioSource>();
        tv = flagTV.GetComponent<SpriteRenderer>();

        foreach (Country c in spawner.countries) {
            packages[c] = new List<PackageManager>();
        }
    }


    void Update() {

        if (accepting == null && existing.Count > 0) {
            UpdateAccepting();
        }

    }


    void UpdateAccepting() {
        List<Country> _existing = new List<Country>(existing);
        Country _target = _existing[Random.Range(0, _existing.Count - 1)];

        if (packages[_target].Count > 0) {
            accepting = _target;
            existing.Remove(_target);
        } else {
            return;
        }

        player.clip = acceptChange;
        player.Play();
        tv.sprite = _target.flag;

    }


    public void SpawnedPackage(PackageManager package) {
        packages[package.destination].Add(package);
        existing.Add(package.destination);
    }


    public int IncrementScore() {
        return score++;
    }

}
