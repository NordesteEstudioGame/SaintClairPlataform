using UnityEngine;
using System.Collections;

public class ControladorSaintClair : MonoBehaviour {

	public bool canUserAtkMgc;
	public bool canMove;                  // variavel sempre começa em falso, caso tenha alguma animaçao antes do jogo começar ativar a variavel
	public bool facingRight = true;
	public bool agachado = false;
	public bool jump;
	public int pontosDeVida = 100;
	private bool doubleJump;

	private Animator anim;					// Referencia ao componente de animaçao do jogador.

	public Transform groundCheck;
	public bool grounded;

	public GUIText vidaText;
	public string vida;

	public Transform sprites;
	public float maxSpeed = 5f;
	public float moveForce = 365f;
	public float jumpForce = 1f;	


	public GameObject AtakMagicoMorcego;
	public Transform frenteSaintClair;

	void Awake(){
		groundCheck = transform.Find("groundCheck");
		anim = GetComponentInChildren<Animator>();
		sprites = GameObject.Find ("Sprites").GetComponent<Transform>();
		vidaText = GameObject.Find("Pontos de Vida").GetComponent<GUIText>();
	
	}
	void Start () {
		canMove = true;
		grounded = true;
		canUserAtkMgc = true;
		vidaText.text = pontosDeVida.ToString ();


	}

	public void Agachar(){
		if (agachado) {
			canMove = false;
			anim.SetBool ("agachado", true);	
		} else {
			canMove = true;
			anim.SetBool ("agachado", false);	
		}
	}
	
	void Update(){

		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("chao")); 


		if (grounded && Input.GetKeyDown (KeyCode.S)) {
			agachado = true;
				} else {
			agachado = false;		
		}

		if (Input.GetButtonDown ("Jump")|| Input.GetKeyDown(KeyCode.W)) 
		{
			if(grounded){
				jump = true;
				StartCoroutine(Jump ());	
			}

		}

		if(Input.GetKeyDown(KeyCode.E)){

		}

		if(pontosDeVida <= 0){
			Morrendo();
		}
			
	}
	void FixedUpdate () {
			if (canMove) {

				float h = Input.GetAxis ("Horizontal");
				anim.SetFloat ("Speed", Mathf.Abs (h));
		
			if(Input.GetKeyDown(KeyCode.Q) && h == 0){
				if(canUserAtkMgc){
					StartCoroutine(InstantiateBat());
					StartCoroutine(CooldownAtkMagic());

				}
				
			}
			if(Input.GetKeyDown(KeyCode.E)){
				StartCoroutine(Ataque());
			}


			if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed){
				GetComponent<Rigidbody2D>().transform.Translate (Vector2.right * h * moveForce);
			}

			if (h > 0 && !facingRight) {
				Flip ();

			} else if (h < 0 && facingRight) {
				Flip ();
			}
			if (jump) {
				GetComponent<Rigidbody2D>().transform.Translate (Vector2.up * jumpForce);
			}
		}
	}

	void Morrendo(){
		canMove = false;
		anim.SetBool ("morto", true);
	}

	IEnumerator Jump(){
		if (jump) {
			anim.SetBool ("pulo", true);
			yield return new WaitForSeconds(1f);
			jump = false;
			grounded = false;
			anim.SetBool ("pulo", false);
			yield return new WaitForSeconds(0.4f);
			canMove = true;
		}

	}

	void Flip ()
	{

		facingRight = !facingRight;
		Vector3 theScale = sprites.localScale;
		theScale.x *= -1;
		sprites.localScale = theScale;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "DeathCharacter")
			Morrendo();
		
	}

	IEnumerator Respaw(){
		yield return new WaitForSeconds(3f);
	}
	IEnumerator CooldownAtkMagic(){
		canMove = false;
		canUserAtkMgc = false;
		anim.SetBool("AtkMagic", true);
		yield return new WaitForSeconds(1f);
		canUserAtkMgc = true;
		anim.SetBool("AtkMagic", false);
		canMove = true;
	}

	IEnumerator InstantiateBat(){
		yield return new WaitForSeconds(0.5f);
			Instantiate (AtakMagicoMorcego, new Vector3 (frenteSaintClair.position.x, frenteSaintClair.position.y, frenteSaintClair.position.z), frenteSaintClair.rotation);
	}

	IEnumerator Ataque(){
		anim.SetBool("AtaqueNormal",true);
		yield return new WaitForSeconds(0.5f);
		anim.SetBool("AtaqueNormal",false);
	}
	public void Resetar(){
		Application.LoadLevel(0);
	}

}
