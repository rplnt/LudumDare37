using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPackage : MonoBehaviour {

    public Continets.Continents destination;
    Transform reject;
    Convey conveyor;
    Light sirenLight;
    AudioSource sirenAudio;

    public int sirenCounter = 0;


	// Use this for initialization
	void Start () {
        conveyor = this.GetComponentInParent<Convey>();
        reject = this.transform.parent.FindChild("Reject").transform;
        Transform siren = this.transform.parent.FindChild("Siren");
        if (siren == null) {
            Debug.LogError("No Siren attached to this belt: " + this.gameObject.name);
        } else {
            sirenLight = siren.GetComponent<Light>();
            sirenAudio = siren.GetComponent<AudioSource>();
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (sirenCounter > 0 && sirenAudio.isPlaying == false) {
            sirenAudio.Play();
            sirenCounter--;
            sirenLight.intensity = 3.0f;
        } else if (sirenAudio.isPlaying || sirenLight.intensity >= 0.01f) {
            sirenLight.intensity -= 3.0f * Time.deltaTime;
        } else {
            sirenLight.intensity = 0.0f;
        }
		
	}

    void OnTriggerEnter2D(Collider2D col) {
        PackageManager pm = col.gameObject.GetComponent<PackageManager>();

        if (pm == null) {
            Debug.LogError("Not a package!");
            // Destroy?
            return;
        }

        conveyor.RemovePackageFromBelt(col.gameObject);
        if (pm.destination.continent == this.destination && GameManager.GM.accepting == pm.destination) {
            col.transform.position = new Vector2(-30f, -30f);
            col.transform.parent = null;
            col.gameObject.SetActive(false);

            GameManager.GM.CollectedPackage(pm);
            //TODO send to pool / add score
            Debug.Log("You have a point!");
        } else {

            col.transform.parent = null;
            col.transform.position = new Vector3(reject.transform.position.x + Random.Range(-0.1f, 0.1f), reject.transform.position.y + Random.Range(-0.1f, 0.1f), col.transform.position.z);
            col.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
            sirenCounter += 3;

        }
    }
}
