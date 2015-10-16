using UnityEngine;
using System.Collections;


public class BlockCamera : MonoBehaviour {

	Camera mainCamera;
	CameraPlayer cPlayer;
	void Start () {
		mainCamera = Camera.main;
		cPlayer = mainCamera.GetComponent<CameraPlayer> ();
	}
	

	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D obg2d) {
		if (obg2d.gameObject.tag == "Player") {
			cPlayer.blockCamera = true;

		}
						
	}
	void OnTriggerExit2D(Collider2D obg2d) {
		if (obg2d.gameObject.tag == "Player") {
				cPlayer.blockCamera = false;
		}
	}
}