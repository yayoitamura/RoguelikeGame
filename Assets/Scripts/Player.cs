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

	// bool push;
	string key;

	Vector3 MOVEX = new Vector3 (0.5f, 0, 0);
	Vector3 MOVEY = new Vector3 (0, 0.5f, 0);

	//長押しと判定する時間、メソッドを実行する間隔
	// [SerializeField]
	private float _longPressTime = 0.3f, _invokeInterval = 0.05f;

	//長押しと判定するまで or 次のメソッドを実行するまでの時間
	private float _waitTime = 0;

	//押しているか
	private bool _isPressing = false;

	//一度でもメソッドを実行したか
	private bool _isInvokedEvent = false;
	void Start () {

		movePosition = transform.position;

		cameraControl = GameObject.Find ("Main Camera").GetComponent<CameraControl> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

		animator = GetComponent<Animator> ();
	}

	void Update () {

		// if (push == true) {
		// 	SetTargetPosition ();
		// }
		//ボタンが押されていない時はスルー
		if (!_isPressing) {
			return;
		}
		//待ち時間を減らす
		_waitTime -= Time.deltaTime;
		//待ち時間がまだある時はスルー
		if (_waitTime > 0) {
			return;
		}

		//メソッド実行、待ち時間設定
		// _event.Invoke ();
		SetTargetPosition ();
		_waitTime = _invokeInterval;
		_isInvokedEvent = true;

	}

	public void PushDown (string pushButton) {

		// push = true;
		key = pushButton;
		// Debug.Log ("push   ---  " + key);
		_isPressing = true;
		_isInvokedEvent = false;
		_waitTime = _longPressTime;

	}

	public void PushUp () {

		// push = false;
		_isPressing = false;

	}

	public void Click () {
		Debug.Log ("click");
		//一度もイベントが実行されていなければ実行
		if (!_isInvokedEvent) {
			Debug.Log ("1click");
			// _event.Invoke ();
			SetTargetPosition ();
		}
	}

	public void SetTargetPosition () {

		// if (push == true) {
		switch (key) {

			case "left":
				movePosition = transform.position - MOVEX; //.x -= STEP; // * Time.deltaTime;
				break;

			case "up":
				movePosition = transform.position + MOVEY; //.y += STEP;
				break;

			case "right":
				movePosition = transform.position + MOVEX; //.x += STEP;
				break;

			case "down":
				movePosition = transform.position - MOVEY; //.y -= STEP;
				break;
		}
		// }

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
			float step = 5f;
			// transform.position = movePosition;
			transform.position = Vector3.MoveTowards (transform.position, movePosition, step * Time.deltaTime);
		}
	}

	void CantMove (RaycastHit2D hit) {

		if (hit.collider.gameObject.tag == "enemy") { }
		// Debug.Log ("enemy");
		if (hit.collider.gameObject.tag == "wall") {
			// Debug.Log ("ray");
		}

		if (hit.collider.gameObject.tag == "steps") {
			int nextStage = 0;
			gameManager.LoadScenes (nextStage);
		}

	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "wall") {
			Debug.Log ("colision");

		} else {
			movePosition = transform.position;
		}
	}

}