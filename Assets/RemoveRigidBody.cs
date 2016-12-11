using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveRigidBody : MonoBehaviour {

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.CompareTag("package")) {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null) {
                Destroy(rb);
            }

        }
    }
}
