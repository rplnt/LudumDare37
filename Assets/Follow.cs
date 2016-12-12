using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    GameObject follow;
    public Transform limitUL;
    public Transform LimitDR;

	// Use this for initialization
	void Start () {
        follow = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        // TODO set limit to not go out of bounds
        float size = Camera.main.orthographicSize;
        float ratio = Camera.main.aspect;

        Vector3 newPosition = new Vector3(
            Mathf.Clamp(follow.transform.position.x, limitUL.position.x + size * ratio, LimitDR.position.x - size * ratio),
            Mathf.Clamp(follow.transform.position.y, LimitDR.position.y + size, limitUL.position.y - size),
            -10.0f);
        Debug.Log("Player: " + follow.transform.position + " bounds: " + limitUL.position + ", " + LimitDR.position);
        this.transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 10.0f);
	}
}
