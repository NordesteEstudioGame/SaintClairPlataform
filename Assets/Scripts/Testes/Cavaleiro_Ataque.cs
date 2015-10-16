using UnityEngine;
using System.Collections;

public class Cavaleiro_Ataque : MonoBehaviour {

	public Cavaleiro_new cavaleiro;
	public ControladorSaintClair c = null;

	public void OnTriggerStay2D(Collider2D obg2d) {
		if (obg2d.gameObject.tag == "Player") {
			c = obg2d.GetComponent<ControladorSaintClair>();
			if(
			cavaleiro.ChangeState(Cavaleiro_new.MachineState.ATACANDO);

		}
		
	}

/*	void OnTriggerEnter2D(Collider2D obg2d) {
		if (obg2d.gameObject.tag == "Player") {
			cavaleiro.ChangeState(Cavaleiro_new.MachineState.ATACANDO);
			//cavaleiro.setPodeAtacar(Cavaleiro_new.MachineState.ATACANDO, true);
		}
		
	}
*/
	void OnTriggerExit2D(Collider2D obg2d) {
		if (obg2d.gameObject.tag == "Player") {
			cavaleiro.ChangeState(Cavaleiro_new.MachineState.ANDANDO);
			c = null;
			//cavaleiro.setPodeAtacar(Cavaleiro_new.MachineState.ANDANDO, false);
		}
		
	}
}
