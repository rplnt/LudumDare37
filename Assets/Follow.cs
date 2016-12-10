using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    GameObject follow;

	// Use this for initialization
	void Start () {
        follow = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        // TODO set limit to not go out of bounds
        this.transform.position = Vector3.Lerp(transform.position, new Vector3(follow.transform.position.x, follow.transform.position.y, -10.0f), Time.deltaTime * 10.0f);
	}
}
