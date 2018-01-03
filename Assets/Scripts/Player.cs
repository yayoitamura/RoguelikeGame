using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	Vector2 currentPosition;
	private const int STEP = 1;
	private const float HEIGH = 6.5f;
	private const float WIDTH = 5.5f;

	public enum InputKey {
		Left, // 左
		Up, // 上
		Right, // 右
		Down, // 下

		Empty,
		};
		public InputKey key = InputKey.Empty;

		void Start () {
		currentPosition = transform.position;
	}

	void Update () {

		KeyInput ();

		switch (key) {
			case InputKey.Left:

				break;
			case InputKey.Up:

				break;
			case InputKey.Right:

				break;
			case InputKey.Down:

				break;
		}
	}

	void KeyInput () {
		if (Input.GetKeyDown ("left")) {
			key = InputKey.Left;
			currentPosition.x = Mathf.Clamp (transform.position.x - STEP, -HEIGH, HEIGH);

		} else if (Input.GetKeyDown ("up")) {
			key = InputKey.Up;
			currentPosition.y = Mathf.Clamp (transform.position.y + STEP, -WIDTH, WIDTH);

		} else if (Input.GetKeyDown ("right")) {
			key = InputKey.Right;
			currentPosition.x = Mathf.Clamp (transform.position.x + STEP, -HEIGH, HEIGH);

		} else if (Input.GetKeyDown ("down")) {
			key = InputKey.Down;
			currentPosition.y = Mathf.Clamp (transform.position.y - STEP, -WIDTH, WIDTH);
		}

		Ray ray = new Ray (currentPosition, transform.forward);
		RaycastHit2D hit = Physics2D.Raycast ((Vector2) ray.origin, (Vector2) ray.direction, 10);
		if (hit.collider) {
			currentPosition = transform.position;
			CantMove (hit);
		}

		transform.position = currentPosition;
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