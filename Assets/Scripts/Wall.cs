using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
	SpriteRenderer spriteRenderer;
	public GameObject groundSprite;
	public Sprite damegeSprite;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();

	}

	void Update () {

	}

	public void wallDamage () {

		spriteRenderer.sprite = damegeSprite;
		Destroy (gameObject, 0.1f);
		Instantiate (groundSprite, transform.position, Quaternion.identity);

	}

}