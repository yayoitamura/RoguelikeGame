using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
	SpriteRenderer spriteRenderer;
	public GameObject groundSprite;
	public Sprite damegeSprite;
	int wallHp = 2;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();

	}

	void Update () {

	}

	public void wallDamage () {
		wallHp--;
		if (wallHp <= 0) {
			Debug.Log ("wall");
			// gameObject.SetActive (false);
			Destroy (this.gameObject);
			Instantiate (groundSprite, transform.position, Quaternion.identity);

		} else { spriteRenderer.sprite = damegeSprite; }
	}

}