using UnityEngine;
using System.Collections;

public class Paralax : MonoBehaviour {


	public Material material;
	public float velocidade;
	private float offset;
	
	// Use this for initialization
	void Start  () {
		offset = 0.001f;
	//	material = gameObject.GetComponent<Material>();
	}
	
	// Update is called once per frame
	void Update () {

		float h = Input.GetAxis ("Horizontal");
		offset += h;

		material.SetTextureOffset("_MainTex", new Vector2 ( offset*velocidade,0));
              
	}
}
