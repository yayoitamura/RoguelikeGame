using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
	public GameObject particlePrefab;
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
		ParticleSystem particle = Instantiate (particlePrefab, transform.position, Quaternion.identity).GetComponent<ParticleSystem> ();

		particle.Play ();
		ParticleSystem.MainModule mainModule = particle.main;
		Destroy (particle.gameObject, mainModule.duration);

		Destroy (gameObject, 0.1f);
		Instantiate (groundSprite, transform.position, Quaternion.identity);

	}

}