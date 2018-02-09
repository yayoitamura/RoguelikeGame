using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour {

	public bool playerTurn = true;
	CameraControl cameraControl;
	GameManager gameManager;
	Animator animator;
	[SerializeField] private GameObject Damage;
	[SerializeField] private GameObject item;
	[SerializeField] private LayerMask layerMask;
	// int playerHp = 5;
	public static int playerHp = 5;

	//Click
	float longPressTime = 0.2f;
	float interval = 0.1f;
	float waitTime = 0;
	bool isPressing = false;
	bool needMove = false;

	//Move
	string key;
	Vector2 movePosition;
	const float ACTION_RANGE = 8.5f;
	// Vector2 MOVE_RANGE = new Vector2 (8.5F, 8.5F);
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

	/*
	needMove isPressing（押している） 
	長押し true true
	押してない　true false
	短い押し false true
	押してない false false
	*/
	void Update () {

		if (!isPressing) {
			return;
		}
		waitTime -= Time.deltaTime;
		if (waitTime < 0) {
			SetTargetPosition ();
			waitTime = interval;
			needMove = true;
		}
	}

	//buttonからの入力
	public void PushDown (string pushButton) {
		key = pushButton;
		isPressing = true;
		needMove = false;
		waitTime = longPressTime;
	}
	public void PushUp () {
		isPressing = false;
	}
	public void Click () {
		if (!needMove) {
			SetTargetPosition ();
		} //方向転換後マスを勧められない時に出せない
	}
	public void PutItem () {

		Ray itemRay = new Ray (itemPosition, transform.forward);
		RaycastHit2D itemHit = Physics2D.Raycast ((Vector2) itemRay.origin, (Vector2) itemRay.direction);

		if (itemHit.collider) {
			return;
		}
		Instantiate (item, itemPosition, Quaternion.identity);
	}

	public void SetTargetPosition () {
		const float PLAYER_STRIDE = 1f;
		switch (key) {

			case "left":
				movePosition.x = transform.position.x - PLAYER_STRIDE;
				movePosition.x = Mathf.Clamp (movePosition.x, -ACTION_RANGE, ACTION_RANGE);
				break;

			case "up":
				movePosition.y = transform.position.y + PLAYER_STRIDE;
				movePosition.y = Mathf.Clamp (movePosition.y, -ACTION_RANGE, ACTION_RANGE);
				break;

			case "right":
				movePosition.x = transform.position.x + PLAYER_STRIDE;
				movePosition.x = Mathf.Clamp (movePosition.x, -ACTION_RANGE, ACTION_RANGE);
				break;

			case "down":
				movePosition.y = transform.position.y - PLAYER_STRIDE;
				movePosition.y = Mathf.Clamp (movePosition.y, -ACTION_RANGE, ACTION_RANGE);
				break;
		}
		SetAnimationParam ();
		// 当たったものの取得
		RaycastHit2D hit = getHitObject (movePosition);

		Move (hit);
		// カメラの移動
		cameraControl.Move ();
	}

	void SetAnimationParam () {
		animator.Play (key);
	}

	RaycastHit2D getHitObject (Vector2 position) {
		Ray ray = new Ray (position, transform.forward);
		return Physics2D.Raycast ((Vector2) ray.origin, (Vector2) ray.direction, 10);
	}

	void Move (RaycastHit2D hit) {
		// プレイヤー移動
		if (hit.collider) {
			if (hit.collider.gameObject.tag == "enemy") {
				AttackEnemy (hit.collider.gameObject);

			} else if (hit.collider.gameObject.tag == "wall") {
				BreakWall (hit.collider.gameObject);

			} else if (hit.collider.gameObject.tag == "steps") {
				transform.position = movePosition;
				MoveScene (1);
			}
		} else {
			if (playerTurn) {
				PlayerAudio.PlayOneShot (footsateps, 0.1f);
				itemPosition = transform.position;

				transform.position = movePosition;
				playerTurn = false;
				//ターン終了	
			}
		}
	}

	void AttackEnemy (GameObject hitEnemy) {
		Enemy enemy = hitEnemy.GetComponent<Enemy> ();
		enemy.ReceiveAttack ();
		movePosition = transform.position;
		playerTurn = false;
	}
	void BreakWall (GameObject hitWall) {
		movePosition = transform.position;
		Wall wall = hitWall.GetComponent<Wall> ();
		wall.wallDamage ();
	}

	public void ReceiveAttack () {
		Destroy (Instantiate (Damage, transform.position, Quaternion.identity), 0.5f);
		playerHp -= 1;

		if (playerHp == 0) {
			PlayerDie ();
		}
	}

	void PlayerDie () {
		Destroy (gameObject, 1f);
		
	}

	public static int getPlayerHp () {
		return playerHp;
	}

	void MoveScene (int sceneIndex) {
		gameManager.LoadNextStage ();
	}

	void OnCollisionEnter2D (Collision2D other) {
		movePosition = transform.position;
	}
}