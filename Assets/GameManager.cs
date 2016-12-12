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
    public Text bestScoreUI;

    public Country accepting;

    AudioSource player;
    public AudioClip acceptChange;

    string scoreKey = "LD34_best";
    int score;
    int best;

    Dictionary<Country, List<PackageManager>> packages;
    HashSet<Country> existing;

    void Start() {
        best = PlayerPrefs.GetInt(scoreKey);
        if (best > 0) {
            bestScoreUI.text = "Best: " + best;
        } else {
            bestScoreUI.text = "";
        }
        best = 0;
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
        AddPoint();
        Destroy(package.gameObject);
    }


    public void AddPoint() {
        score++;
        UpdateScore();
    }

    public void RemovePoint() {
        score--;
        UpdateScore();
    }

    public int ExistingTypes() {
        return existing.Count;
    }

    public GameObject gameover;
    public Text gameovertext;
    public bool isover = false;

    public void GameOver() {
        if (score > best) {
            PlayerPrefs.SetInt(scoreKey, score);
        }
        isover = true;
        gameover.SetActive(true);
        gameovertext.text = "Final Score: " + score + ((score > best) ? " [NEW RECORD]" : "");
    }

    public void ResetGame() {
        Application.LoadLevel("room");
    }

}
