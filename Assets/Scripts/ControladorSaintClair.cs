using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControladorSaintClair : MonoBehaviour {

	public bool canUserAtkMgc;			  	// Verifica se o jogador pode sar Ataques magicos
	public bool canMove;					// variavel sempre começa em falso, caso tenha alguma animaçao antes do jogo começar ativar a variavel
	public bool facingRight = true;			// Jogador esta olhando para a direita?
	public bool agachado = false;			// Jogador esta agachado?
	public bool jump;						// Jogador pode pular?
	private bool doubleJump;				// Jogador pode dar pulo duplo
	private Animator anim;					// Referencia ao componente de animaçao do jogador.
	public Transform groundCheck;			// Verificador de contato com o chao
	public bool grounded;					// Verifica se o jogador esta no chao 
	public GUIText vidaText;				// GUI do jogador
	public string textVida;					// String que informa qual os pontos de vida do jogador
	public Transform sprites;				// Sprites de animaçao do jogador
	public float maxSpeed = 5f;				// Velocidade Maxima que o jogador pode chegar
	public float moveForce = 365f;			// Força do movimento do jogador
	public float jumpForce = 1f;			// Força do pulo do jogador
	public GameObject AtakMagicoMorcego;	// Prefab do Ataque magico morcego
	public Transform frenteSaintClair;
	public Transform atrasSaintClair;
	public bool shotRight = true;


	public float vida = 1f;
	public Image imgSliderVida;
	public float ataque = 0.35f;
	public float armadura = 0f;
	public float lifeSteal = 0.35f;
	public float custoAtkMagico = 0.2f;


	// variaveis do local sagrado
	public bool localSagrado = false;
	public float danoSagrado = 0.05f;
	public float tempoParaDano = 5f;
	public float tempoAtualDanoSagrado = 0f;

	//variaveis para combo
	public bool cooldownAtaque;
	public float tempoCombo = 0.5f;
	public float tempoAtualCombo;
	public int combo=0;

	void Awake(){
		groundCheck = transform.Find("groundCheck");	//procura o verificador de chao
		anim = GetComponentInChildren<Animator>();		// pega componente Animator	
		sprites = GameObject.Find ("Sprites").GetComponent<Transform>();		// pega componente Transform	
	
	}
	void Start () {
	
		canMove = true; 					// Variavel inicializada como true.
		grounded = true; 					// Variavel inicializada como true.
		canUserAtkMgc = true;				// Variavel inicializada como true.
	}

	
	void Update(){ // chamada todo frame
		imgSliderVida.fillAmount = vida;
		// todo frame verifica se esta no chao
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

		if(localSagrado){

			tempoAtualDanoSagrado += Time.deltaTime * 1f;

			if(tempoAtualDanoSagrado > tempoParaDano){
				vida -= danoSagrado;
				tempoAtualDanoSagrado =0;
			}


		}else{
			tempoAtualDanoSagrado = 0;
		}

		if(vida <= 0){
			Morrendo();
		}
		if(cooldownAtaque){
			tempoAtualCombo += Time.deltaTime * 1f;
		}
	}
	void FixedUpdate () {
		if (canMove) {
			float h = Input.GetAxis ("Horizontal");
			anim.SetFloat ("Speed", Mathf.Abs (h));
			
			if(Input.GetKeyDown(KeyCode.Q) && h == 0){
			
					if(vida> custoAtkMagico){
					StartCoroutine(InstantiateBat());
					StartCoroutine(CooldownAtkMagic());

				}
				
			}
			if(Input.GetKeyDown(KeyCode.E)){
				cooldownAtaque = true;

				if(combo == 0){
					StartCoroutine(Ataque());
					combo++;
				}else if(combo == 1){
					combo++;
				}else if(combo == 2){

				}
			}
			if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed){
				GetComponent<Rigidbody2D>().transform.Translate (Vector2.right * h * moveForce);
			}
			if (h > 0 && !facingRight) {
				shotRight = true;
				Flip ();

			} else if (h < 0 && facingRight) {
				shotRight = false;
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
		vida -= custoAtkMagico;
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
		if(shotRight)
			Instantiate (AtakMagicoMorcego, new Vector3 (frenteSaintClair.position.x, frenteSaintClair.position.y, frenteSaintClair.position.z), frenteSaintClair.rotation);
		else
			Instantiate (AtakMagicoMorcego, new Vector3 (atrasSaintClair.position.x, atrasSaintClair.position.y, atrasSaintClair.position.z), atrasSaintClair.rotation);
	}
	IEnumerator Ataque(){
		anim.SetBool("AtaqueNormal",true);
		yield return new WaitForSeconds(0.5f);
		anim.SetBool("AtaqueNormal",false);

	}
	public void Resetar(){
		Application.LoadLevel(0);
	}

	public void SetLocalSagrado(bool isSagrado){
		localSagrado = isSagrado;
	}

	public void ReceberDano(float dano){
		vida -= dano;
	}
}
