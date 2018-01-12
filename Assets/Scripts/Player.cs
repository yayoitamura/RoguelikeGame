using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour {
	CameraControl cameraControl;
	GameManager gameManager;
	Wall wall;
	Animator animator;

	// const float STEP = 0.5f;
	Vector2 movePosition;
	string key;

	Vector3 MOVEX = new Vector3 (0.5f, 0, 0);
	Vector3 MOVEY = new Vector3 (0, 0.5f, 0);

	float longPressTime = 0.2f;
	float interval = 0.1f;
	float waitTime = 0;
	bool isPressing = false;
	bool isEvent = false;

	void Start () {

		movePosition = transform.position;

		cameraControl = GameObject.Find ("Main Camera").GetComponent<CameraControl> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

		animator = GetComponent<Animator> ();
	}

	void Update () {

		//ボタンが押されていない時はスルー
		if (!isPressing) {
			return;
		}
		//待ち時間を減らす
		waitTime -= Time.deltaTime;
		//待ち時間がまだある時はスルー
		if (waitTime > 0) {
			return;
		}

		//メソッド実行、待ち時間設定
		SetTargetPosition ();
		waitTime = interval;
		isEvent = true;

	}

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
		//一度もイベントが実行されていなければ実行
		if (!isEvent) {
			SetTargetPosition ();
		}
	}

	public void SetTargetPosition () {

		switch (key) {

			case "left":
				movePosition = transform.position - MOVEX;
				break;

			case "up":
				movePosition = transform.position + MOVEY;
				break;

			case "right":
				movePosition = transform.position + MOVEX;
				break;

			case "down":
				movePosition = transform.position - MOVEY;
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
			// float step = 5f;
			transform.position = movePosition;
			// transform.position = Vector3.MoveTowards (transform.position, movePosition, step * Time.deltaTime);
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

		}

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