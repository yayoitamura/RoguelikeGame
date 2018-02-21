using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
  Player player;
  Vector2　 playerPosition;
  bool isMovable;
  int enemyHp = 10;
  [SerializeField] GameObject Damage;

  void Start () {
    player = GameObject.Find ("Man").GetComponent<Player> ();
  }

  //PLAYINGstateの時だけにキャラクターを動かせるようにしたい
  //pkayingで呼び出す→死んだ」タイミングでステート切り替え
  // 

  // void Update () {

	// 	switch (GameState.instance.state) {
	// 		case GameState.Game.READY:
	// 			GameState.instance.state = GameState.Game.START;
	// 			break;
	// 		case GameState.Game.START:
	// 			//スタート
	// 			//ボタン操作○ プレイヤ× 敵×
	// 			//メニュー画面
	// 			break;
	// 		case GameState.Game.PREPARE:
	// 			//準備
	// 			//ボタン操作× プレイヤ× 敵×
	// 			//シーン以降・フェード・ダンジョン生成
	// 			Prepare ();
	// 			GameObject.Find ("Man").GetComponent<Player> ().PlayerSetUp ();
	// 			GameState.instance.state = GameState.Game.PLAYING;
	// 			break;
	// 		case GameState.Game.PLAYING:

	// 			GameObject.Find ("Main Camera").GetComponent<CameraControl> ().PlayerLookOn ();
	// 			//プレイ中
	// 			//ボタン操作○ プレイヤ○ 敵○
	// 			//プレイ開始→プレイ終了（死or階段）
	// 			break;
	// 		case GameState.Game.END:
	// 			//ゲーム終了
	// 			//ボタン操作○ プレイヤ× 敵×
	// 			//ゲームオーバー/クリア画面
	// 			break;
	// 	}
	// }


  void Update () {
    if (!player.playerTurn) {
      attemptMove ();
    }
  }

  void attemptMove () {

    if (!isMovable) {
      player.playerTurn = true;

    } else {
      ChasePlayer ();
    }

  }

  public void GetPlayerPosition (Vector2 target, bool isTrigger) {

    playerPosition = target;
    isMovable = isTrigger;

  }

  public void ChasePlayer () {

    float xDirection = 0;
    float yDirection = 0;

    const float ENEMY_STRIDE = 1;

    if (Mathf.Abs (playerPosition.x - transform.position.x) < float.Epsilon) {
      yDirection = playerPosition.y > transform.position.y ? ENEMY_STRIDE : -ENEMY_STRIDE;
    } else {
      xDirection = playerPosition.x > transform.position.x ? ENEMY_STRIDE : -ENEMY_STRIDE;
    }

    Move (xDirection, yDirection);
  }

  void Move (float xDirection, float yDirection) {
    Vector2 start = transform.position;
    Vector2 end = start + new Vector2 (xDirection, yDirection);

    BoxCollider2D boxCollider = GetComponent<BoxCollider2D> ();
    boxCollider.enabled = false;

    RaycastHit2D hit = Physics2D.Linecast (start, end);

    boxCollider.enabled = true;
    if (hit) {
      if (hit.transform.tag == "wall") {
        player.playerTurn = true;

      } else if (hit.transform.tag == "player") {
        AttackPlayer ();
        player.playerTurn = true;
      }
    } else {
      if (!player.playerTurn) {
        transform.position = end;
        player.playerTurn = true;
      }
    }
  }

  RaycastHit2D getHitObject (Vector2 position) {
    Ray ray = new Ray (position, transform.forward);
    return Physics2D.Raycast ((Vector2) ray.origin, (Vector2) ray.direction, 10);
  }

  void AttackPlayer () {
    player.ReceiveAttack ();
  }

  public void ReceiveAttack () {
    Destroy (Instantiate (Damage, transform.position, Quaternion.identity), 0.5f);
    enemyHp -= 1;
  }

  public void abortChase () {
    GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
  }

}