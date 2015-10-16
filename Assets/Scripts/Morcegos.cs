using UnityEngine;
using System.Collections;

public class Morcegos : MonoBehaviour {

	public float velocidadeMorcego;
	private Animator anim;
	public bool canMove;
	public ControladorSaintClair posicaoSaintClair;
	void Start () {
		canMove = true;
		anim = gameObject.GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		if (canMove) {
			transform.Translate (velocidadeMorcego * Time.deltaTime, 0, 0);
		}

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "inimigo"){
			SetCanMove();
			anim.SetBool("explodir",true);
			StartCoroutine(Destroy());
		}

		
	}
	IEnumerator Destroy(){
		yield return new WaitForSeconds(0.4f);
		Destroy(gameObject);
	}
	void SetCanMove(){
		canMove = false;
	}
}
