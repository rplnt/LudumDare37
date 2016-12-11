using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float rotateSpeed;

    public bool useMouseToRotate;
    bool paused = false;

    Rigidbody2D rb;
    PackageHandler ph;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody2D>();
        ph = this.GetComponentInChildren<PackageHandler>();
	}
	
	// Update is called once per frame
	void Update () {
        if (paused) return;

        float holdingPenalty = ph.holding == null ? 1.2f : 0.85f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            transform.Translate(Vector2.up * speed * Time.deltaTime * holdingPenalty);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(Vector2.down * speed * Time.deltaTime * 0.85f * holdingPenalty);
        }

        if (useMouseToRotate) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (mousePos - gameObject.transform.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * rotateSpeed);


            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
        } else {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime * holdingPenalty);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                transform.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime * holdingPenalty);
            }
        }
	}

    //void OnCollisionEnter2D(Collision2D col) {
    //    Debug.Log(col.gameObject.name);
    //}


    //void OnTriggerEnter2D(Collider2D col) {
    //    Debug.Log(col.gameObject.name);
    //}
}
