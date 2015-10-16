using UnityEngine;
using System.Collections;

public class Pilar : MonoBehaviour {

	public Animator animator;

	public bool startAnimation;

	public float cdAnimacao = 1f;
	private float tempoAnimacaoAtual;

	void Start () {
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(startAnimation){
			tempoAnimacaoAtual += Time.deltaTime;
			print (tempoAnimacaoAtual);
			if(tempoAnimacaoAtual > cdAnimacao){
				animator.SetBool("quebrado",true);
				startAnimation = false;
			}

		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "Player")
			startAnimation = true;
			
		
	}
}
