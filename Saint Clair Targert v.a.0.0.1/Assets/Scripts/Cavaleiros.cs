using UnityEngine;
using System.Collections;

public class Cavaleiros : Inimigos {


	public Animator anim;

	public ControladorSaintClair pontos;

	public float speedMove;
	public float posicaoMedia;
	public float velocidade;
	public float diferencaDePosicao;
	public float tempoCorrente;
	public float tempoDecorrido;

	private bool facingRight;
	private bool trocaLado;

	private Transform sprites;
	public Transform posicao;	
	public Transform posicaoSaintClair;
	
	public Collider2D colisaoAnimAtack;

	void Awake(){
		tempoCorrente = 3;
		sprites = GameObject.Find ("cavbranco_andando0003").GetComponent<Transform> ();
		}

	void Start () {
		speedMove = 0.03f;
		facingRight = false;
		trocaLado = true;
		velocidade = 20f;
	}
	

	void Update () {

		diferencaDePosicao = posicao.transform.position.x - posicaoSaintClair.transform.position.x;
		tempoDecorrido += Time.deltaTime;

		RaycastHit2D hit = Physics2D.Raycast (posicao.transform.position, Vector2.right, 7f);
		print (hit.collider.tag);

		if(hit.collider.tag == "Player"){
		
			if (diferencaDePosicao > 0 && !facingRight) {
				//Movement();
				trocaLado = false;
				Flip ();
			
		
			} else if (diferencaDePosicao < 0 && facingRight) {
				//Movement();
				trocaLado = true;	
				Flip ();
		
			}

		}else if(tempoDecorrido > tempoCorrente){
			tempoDecorrido = 0;
			Caminhar();
		}

		if(diferencaDePosicao < 7.5f || diferencaDePosicao > -7.5){
			Movement();
		}

		}
	

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.tag == "Player") {
			_Atacar (false);
		}
	}
	

	void OnTriggerStay2D(Collider2D coll){

		if (coll.tag == "Player") {
			_Atacar (true);
		}
	}

	IEnumerator Atacar(bool atacar){
		anim.SetBool("Atacar",atacar);

		yield return new WaitForSeconds (0.3f);
		pontos.pontosDeVida --;

	}
	void _Atacar(bool atacar){
		StartCoroutine (Atacar (atacar));
	}

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.tag == "Player"){
			pontos.pontosDeVida = pontos.pontosDeVida - 10;
		}
	
	}

	void Flip ()
	{
		
		facingRight = !facingRight;
		Vector3 theScale = sprites.localScale;
		theScale.x *= -1;
		sprites.localScale = theScale;
	}

	void Movement(){
		if (colisaoAnimAtack.tag != "Player" && trocaLado == false) {
			posicao.transform.Translate (-speedMove*velocidade*Time.deltaTime, 0, 0);

		} else if(colisaoAnimAtack.tag != "Player" && trocaLado == true){
			posicao.transform.Translate (speedMove*velocidade*Time.deltaTime, 0, 0);
		}

	}

	void Caminhar(){

		if(tempoDecorrido == 0 && trocaLado == false){
			trocaLado = true;
		}else if(tempoDecorrido == 0 && trocaLado == true){
			trocaLado = false;
		}

		if (tempoDecorrido == 0) {

			Flip ();
			} else if (trocaLado == false) {
				posicao.transform.Translate (-speedMove * velocidade * Time.deltaTime, 0, 0);
			} else if (trocaLado == true) {
				posicao.transform.Translate (speedMove * velocidade * Time.deltaTime, 0, 0);
			}
	}
	
}
