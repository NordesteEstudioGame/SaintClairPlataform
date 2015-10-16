using UnityEngine;
using System.Collections;

public class Cavaleiro_new : Inimigos {

	public Animator animator;

	public float velocidadeAndar = 1f;
	public bool direita;

	//variaveis de ataque
	private bool podeAtacar = false;
	public float tempoAtacarNovamente;
	private float tempoAtualParaAtaque;

	//variaveis de trocar lado

	public bool grounded;
	public bool grounded2;
	public Transform groundCheck;
	public Transform groundCheck2;


	public Cavaleiro_Ataque cavaleiro_Ataque;

	public enum MachineState{
		ANDANDO,
		ATACANDO,
		MORTO
	}

	public MachineState mState;

	void Awake(){
	
		SelecionarClasse("Cavaleiro");
	}
	
	void Start () {
		mState = MachineState.ANDANDO;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("chao")); 
		grounded2 = Physics2D.Linecast(transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("chao"));

		if ((!grounded ) || (!grounded2)){
			Flip();
		}
		switch( mState ) {

		case MachineState.ANDANDO :
			animator.SetBool("Atacar",false);
			animator.SetBool("Atacando",false);
			transform.Translate(velocidadeAndar * Time.deltaTime,0,0);

			break;
		case MachineState.ATACANDO :
			animator.SetBool("Atacar",true);

			tempoAtualParaAtaque += Time.deltaTime;

			if(tempoAtualParaAtaque >= tempoAtacarNovamente){

				try {
					ControladorSaintClair c = cavaleiro_Ataque.c;
					animator.SetBool("Atacando",true);
					c.ReceberDano(this.ataque);
				}catch{

				}
				tempoAtualParaAtaque = 0;
			}

			break;
		case MachineState.MORTO :

			break;

		}

		//ManchinaState - Morrendo
		if(this.vida <= 0){
			Morrer();
		}

	} 

/*	public void Atacar(){
			
			tempoAtualParaAtaque += Time.deltaTime * 1f;
			if(tempoAtualParaAtaque > tempoAtacarNovamente){
				animator.SetBool("Atacar",true);
				tempoAtualParaAtaque = 0;
				
				//animator.SetBool("Atacar",true);
		}

	}*/

	public void setPodeAtacar(MachineState m, bool _podeAtacar){
		mState = m;
		podeAtacar = _podeAtacar;
	}

	public void Morrer(){
		mState = MachineState.MORTO;
		animator.SetBool("Morto", true);
	}
	public void ChangeState(MachineState m){
		this.mState = m;
	}

	public void Flip(){
		if(direita){
		//	transform.localScale = new Vector3 ( 1f, 1f, 1f);
			transform.eulerAngles = new Vector3 ( 0f,180f,0f);
			direita = false;
	}else{
		//	transform.localScale = new Vector3 ( -1f, 1f, 1f);
			transform.eulerAngles = new Vector3 ( 0f,0f,0f);
			direita = true;
		}
	}
}
