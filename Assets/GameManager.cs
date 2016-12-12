using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
    public SpriteRenderer tv;
    public Text scoreUI;

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

        foreach (Country c in spawner.countries) {
            packages[c] = new List<PackageManager>();
        }
    }


    void Update() {
        if ((accepting == null || packages[accepting].Count == 0) && existing.Count > 0) {
            UpdateAccepting();
        }

    }


    IEnumerator PlayAccept(AudioClip c) {
        player.clip = acceptChange;
        player.Play();
        float wait = player.clip.length;
        float elapsed = 0.0f;
        yield return null;

        while (elapsed < wait) {
            elapsed += Time.deltaTime;
            yield return null;
        }

        player.clip = c;
        player.Play();
    }


    void UpdateScore() {
        scoreUI.text = "Score: " + score;
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

        StartCoroutine(PlayAccept(_target.clip));
        tv.sprite = _target.flag;
    }


    public void SpawnedPackage(PackageManager package) {
        packages[package.destination].Add(package);
        existing.Add(package.destination);
    }

    public void CollectedPackage(PackageManager package) {
        packages[package.destination].Remove(package);
        score++;
        UpdateScore();
        Destroy(package.gameObject);
    }

}
