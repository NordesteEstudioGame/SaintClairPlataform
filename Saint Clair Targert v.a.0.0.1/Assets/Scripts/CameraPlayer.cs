using UnityEngine;
using System.Collections;


public class CameraPlayer : MonoBehaviour {

	public Transform objectToFollow;
	public bool blockCamera = true;
	public Vector3 offset = new Vector3(0f, 0f, -10f);

	// Use this for initialization
	void Start () {

	}
	

	void Update () {
		if (blockCamera == false) {
			transform.position = new Vector3(objectToFollow.position.x,0, objectToFollow.position.z)	+ offset;
		}
	}
}
