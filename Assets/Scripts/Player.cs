using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour {

	CameraControl cameraControl;
	GameManager gameManager;

	Animator animator;
	const float STEP = 0.5f;
	Vector2 movePosition;

	void Start () {

		movePosition = transform.position;

		cameraControl = GameObject.Find ("Main Camera").GetComponent<CameraControl> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

		animator = GetComponent<Animator> ();
	}

	public void SetTargetPosition (string key) {

		switch (key) {
			case "left":
				movePosition.x -= STEP;
				Debug.Log ("state  left");
				break;
			case "up":
				movePosition.y += STEP;
				Debug.Log ("state  up");
				break;
			case "right":
				movePosition.x += STEP;
				Debug.Log ("state  right");
				break;
			case "down":
				movePosition.y -= STEP;
				Debug.Log ("state  down");
				break;
		}

		SetAnimationParam (key);
		Move ();
	}

	void SetAnimationParam (string key) {

		if (key == "left") {
			animator.Play ("chaLeft");
			return;
		}

		if (key == "right") {
			animator.Play ("chaRight");
			return;
		}

		if (key == "down") {
			animator.Play ("chaDown");
			return;
		}

		if (key == "up") {
			animator.Play ("chaUp");
			return;
		}
	}

	void Move () {

		cameraControl.Move ();

		Ray ray = new Ray (movePosition, transform.forward);
		RaycastHit2D hit = Physics2D.Raycast ((Vector2) ray.origin, (Vector2) ray.direction, 10);

		if (hit.collider) {
			movePosition = transform.position;
			CantMove (hit);
		} else {
			transform.position = movePosition;
		}
	}

	void CantMove (RaycastHit2D hit) {

		if (hit.collider.gameObject.tag == "enemy") { }

		if (hit.collider.gameObject.tag == "wall") { }

		if (hit.collider.gameObject.tag == "steps") {
			int nextStage = 0;
			gameManager.LoadScenes (nextStage);
		}

	}

	void OnCollisionEnter2D (Collision2D other) {

		Debug.Log ("On Collision == " + other.gameObject.tag);
		movePosition = transform.position;
	}

}