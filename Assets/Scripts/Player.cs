using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour {

	bool playerTurn;
	CameraControl cameraControl;
	GameManager gameManager;
	Wall wall;
	Animator animator;
	public GameObject Damage;
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
	const float MOVE_X = 1f;
	const float MOVE_Y = 1f;
	Vector2 itemPosition;

	//Audio
	AudioSource PlayerAudio;
	public AudioClip footsateps;

	void Start () {

		movePosition = transform.position;
		itemPosition = new Vector2 (transform.position.x, transform.position.y + 1f);

		cameraControl = GameObject.Find ("Main Camera").GetComponent<CameraControl> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

		animator = GetComponent<Animator> ();
		PlayerAudio = GetComponent<AudioSource> ();
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
				movePosition.x = transform.position.x - MOVE_X;
				movePosition.x = Mathf.Clamp (movePosition.x, -ACTION_RANGE, ACTION_RANGE);
				break;

			case "up":
				movePosition.y = transform.position.y + MOVE_Y;
				movePosition.y = Mathf.Clamp (movePosition.y, -ACTION_RANGE, ACTION_RANGE);
				break;

			case "right":
				movePosition.x = transform.position.x + MOVE_X;
				movePosition.x = Mathf.Clamp (movePosition.x, -ACTION_RANGE, ACTION_RANGE);
				break;

			case "down":
				movePosition.y = transform.position.y - MOVE_Y;
				movePosition.y = Mathf.Clamp (movePosition.y, -ACTION_RANGE, ACTION_RANGE);
				break;
		}
		SetAnimationParam ();
		Move (movePosition);
	}

	void SetAnimationParam () {

		animator.Play (key);

	}

	void Move (Vector2 movePosition) {

		Ray ray = new Ray (movePosition, transform.forward);
		RaycastHit2D hit = Physics2D.Raycast ((Vector2) ray.origin, (Vector2) ray.direction, 10);

		if (hit.collider) {
			CantMove (hit);
		} else {
			PlayerAudio.PlayOneShot (footsateps, 0.1f);

			itemPosition = transform.position;
			// movePosition = new Vector2 (
			// 	Mathf.Clamp (movePosition.x, -ACTION_RANGE, ACTION_RANGE),
			// 	Mathf.Clamp (movePosition.y, -ACTION_RANGE, ACTION_RANGE));
			transform.position = movePosition;
		}

		cameraControl.Move ();
	}

	void CantMove (RaycastHit2D hit) {

		if (hit.collider.gameObject.tag == "enemy") {
			movePosition = transform.position;
			Instantiate (Damage, transform.position, Quaternion.identity);

		} else if (hit.collider.gameObject.tag == "wall") {
			movePosition = transform.position;

			wall = hit.collider.gameObject.GetComponent<Wall> ();
			wall.wallDamage ();

		} else if (hit.collider.gameObject.tag == "steps") {
			transform.position = movePosition;
			Invoke ("MoveScene", 1.0f);

		} else {
			transform.position = movePosition;
		}
	}

	void MoveScene () {

		gameManager.LoadScene ();

	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "wall") {
			movePosition = transform.position;
		} else {
			movePosition = transform.position;
		}
	}
}