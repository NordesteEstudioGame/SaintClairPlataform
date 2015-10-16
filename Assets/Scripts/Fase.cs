using UnityEngine;
using System.Collections;

public class Fase : MonoBehaviour {

	public ControladorSaintClair controlador;
	// Use this for initialization
	void Awake(){
		controlador = GameObject.FindWithTag("Player").GetComponent<ControladorSaintClair>();
	}

	void Start () {

	}

	void OnTriggerEnter2D(Collider2D obg2d) {
		if (obg2d.gameObject.tag == "Player") {
		
			controlador.SetLocalSagrado(true);
		}
		
	}
	void OnTriggerExit2D(Collider2D obg2d) {
		if (obg2d.gameObject.tag == "Player") {
			controlador.SetLocalSagrado(false);
		}
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
