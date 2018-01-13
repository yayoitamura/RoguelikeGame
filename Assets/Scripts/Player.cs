using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour {
	CameraControl cameraControl;
	GameManager gameManager;
	Wall wall;
	Animator animator;

	//Click
	float longPressTime = 0.2f;
	float interval = 0.1f;
	float waitTime = 0;
	bool isPressing = false;
	bool isEvent = false;

	//Move
	string key;
	Vector2 movePosition;
	const float HEIGH = 8.5f;
	const float WIDTH = 8.5f;
	Vector3 MOVEX = new Vector3 (0.5f, 0, 0);
	Vector3 MOVEY = new Vector3 (0, 0.5f, 0);

	void Start () {

		movePosition = transform.position;

		cameraControl = GameObject.Find ("Main Camera").GetComponent<CameraControl> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

		animator = GetComponent<Animator> ();
	}

	void Update () {

		if (!isPressing) {
			return;
		}
		waitTime -= Time.deltaTime;
		if (waitTime > 0) {
			return;
		}

		SetTargetPosition ();
		waitTime = interval;
		isEvent = true;

	}

	//buttonからの入力
	public void PushDown (string pushButton) {
		key = pushButton;
		isPressing = true;
		isEvent = false;
		waitTime = longPressTime;
	}
	public void PushUp () {
		isPressing = false;
	}
	public void Click () {
		if (!isEvent) {
			SetTargetPosition ();
		}
	}

	public void SetTargetPosition () {

		switch (key) {

			case "left":
				movePosition = transform.position - MOVEX;
				movePosition.x = Mathf.Clamp (movePosition.x, -HEIGH, HEIGH);
				break;

			case "up":
				movePosition = transform.position + MOVEY;
				movePosition.y = Mathf.Clamp (movePosition.y, -HEIGH, HEIGH);
				break;

			case "right":
				movePosition = transform.position + MOVEX;
				movePosition.x = Mathf.Clamp (movePosition.x, -HEIGH, HEIGH);
				break;

			case "down":
				movePosition = transform.position - MOVEY;
				movePosition.y = Mathf.Clamp (movePosition.y, -HEIGH, HEIGH);
				break;
		}

		SetAnimationParam ();
		Move ();
	}

	void SetAnimationParam () {

		animator.Play (key);

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

		if (hit.collider.gameObject.tag == "enemy") {
			Debug.Log ("Raycast " + hit.collider.gameObject.name);
		}

		if (hit.collider.gameObject.tag == "wall") {
			Debug.Log ("Raycast " + hit.collider.gameObject.name);

			wall = hit.collider.gameObject.GetComponent<Wall> ();
			wall.wallDamage ();
		}

		if (hit.collider.gameObject.tag == "steps") {
			Debug.Log ("Raycast " + hit.collider.gameObject.name);
			int nextStage = 0;
			gameManager.LoadScenes (nextStage);

		} //8,-9  8,-9

	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "wall") {
			Debug.Log ("colision");
			movePosition = transform.position;
		} else {
			movePosition = transform.position;
		}
	}

}