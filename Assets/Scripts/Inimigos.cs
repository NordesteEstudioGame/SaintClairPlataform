using UnityEngine;
using System.Collections;

public class Inimigos : MonoBehaviour {

	public Classe thisClasse;

	public float vida = 1f;
	public float ataque = 0.35f;
	public float armadura = 10f;

	public void SelecionarClasse(string current_classe){
		switch  (current_classe)
		{
		case "Cavaleiro":
			thisClasse = Classe.cavaleiro;
			break;
		case "Arqueiro":
			thisClasse = Classe.arqueiro;
			break;
		default:

			break;
		}
		SelecionarAtributos();
	}

	public void SelecionarAtributos(){
		switch  (thisClasse)
		{
			case Classe.cavaleiro:
				ataque = 0.2f;
				armadura = 0.1f;
			break;
		
			case Classe.arqueiro:
				ataque = 0.15f;
				armadura = 0f;
			break;

			default:
				return;
			break;
		}
	}

}	

	public enum Classe{
		cavaleiro,
		arqueiro
	}


