using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour {
	CameraControl cameraControl;
	GameManager gameManager;
	Wall wall;
	Animator animator;
	public GameObject item;

	//Click
	float longPressTime = 0.2f;
	float interval = 0.1f;
	float waitTime = 0;
	bool isPressing = false;
	bool isEvent = false;

	//Move
	string key;
	Vector2 movePosition;
	const float ACTION_RANGE = 8.5f;
	Vector2 MOVE_RANGE = new Vector2 (8.5F, 8.5F);
	Vector3 MOVEX = new Vector3 (1f, 0, 0);
	Vector3 MOVEY = new Vector3 (0, 1f, 0);

	Vector2 itemPosition;
	void Start () {

		movePosition = transform.position;
		itemPosition = new Vector2 (transform.position.x, transform.position.y + 1f);

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
		} //方向転換後マスを勧められない時に出せない
	}
	public void PutItem () {
		Ray itemRay = new Ray (itemPosition, transform.forward);
		RaycastHit2D itemHit = Physics2D.Raycast ((Vector2) itemRay.origin, (Vector2) itemRay.direction, 10);

		if (itemHit.collider) {
			return;
		}
		Instantiate (item, itemPosition, Quaternion.identity);
	}

	public void SetTargetPosition () {
		switch (key) {

			case "left":
				movePosition = transform.position - MOVEX;
				movePosition.x = Mathf.Clamp (movePosition.x, -ACTION_RANGE, ACTION_RANGE);
				break;

			case "up":
				movePosition = transform.position + MOVEY;
				movePosition.y = Mathf.Clamp (movePosition.y, -ACTION_RANGE, ACTION_RANGE);
				break;

			case "right":
				movePosition = transform.position + MOVEX;
				movePosition.x = Mathf.Clamp (movePosition.x, -ACTION_RANGE, ACTION_RANGE);
				break;

			case "down":
				movePosition = transform.position - MOVEY;
				movePosition.y = Mathf.Clamp (movePosition.y, -ACTION_RANGE, ACTION_RANGE);
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
			CantMove (hit);
		} else {
			itemPosition = transform.position;
			// movePosition = new Vector2 (
			// 	Mathf.Clamp (movePosition.x, -ACTION_RANGE, ACTION_RANGE),
			// 	Mathf.Clamp (movePosition.y, -ACTION_RANGE, ACTION_RANGE));
			transform.position = movePosition;
		}
	}

	void CantMove (RaycastHit2D hit) {

		if (hit.collider.gameObject.tag == "enemy") {
			movePosition = transform.position;
			Debug.Log ("Raycast " + hit.collider.gameObject.name);
		}

		if (hit.collider.gameObject.tag == "wall") {
			movePosition = transform.position;
			Debug.Log ("Raycast " + hit.collider.gameObject.name);

			wall = hit.collider.gameObject.GetComponent<Wall> ();
			wall.wallDamage ();
		}

		if (hit.collider.gameObject.tag == "steps") {
			Debug.Log ("Raycast " + hit.collider.gameObject.name);
			transform.position = movePosition;
			Invoke ("MoveScene", 1.0f);

		}
	}

	void MoveScene () {
		int nextStage = 0;
		gameManager.LoadScenes (nextStage);
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