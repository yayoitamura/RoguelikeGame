using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	Vector2 currentPosition;
	private const int STEP = 1;
	private const float HEIGH = 6.5f;
	private const float WIDTH = 5.5f;
	private float speed = 5.0f;
	Vector2 mousePosition;

	void Start () { }

	void Update () {

		if (Input.GetMouseButton (0)) {
			mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			float step = speed * Time.deltaTime;

			Ray ray = new Ray (mousePosition, transform.forward);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2) ray.origin, (Vector2) ray.direction, 10);
			if (hit.collider) {
				mousePosition = transform.position;
				CantMove (hit);
			}

			transform.position = Vector2.MoveTowards (transform.position, new Vector2 (mousePosition.x, mousePosition.y), step);
		}

	}

	void CantMove (RaycastHit2D hit) {
		if (hit.collider.gameObject.name == "Enemy") {
			Debug.Log ("Enemy hit!!!!!!!!!!!!!!!!!!!!!");
		}
		if (hit.collider.gameObject.name == "crate") {
			Debug.Log ("crate hit!!!!!!!!!!!!!!!!!!!!!");
		}

	}

	void OnCollisionEnter2D (Collision2D other) {
		Debug.Log ("On Collision == " + other.gameObject.tag);
	}

}