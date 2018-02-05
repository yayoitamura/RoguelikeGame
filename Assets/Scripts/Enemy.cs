using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
  bool enemyTurn;
  Vector2　 playerPosition;

  Player player;
  void Start () {
    player = GameObject.Find ("Man").GetComponent<Player> ();
  }

  public void standBy () {

    if (playerPosition == new Vector2 (0, 0)) {
      Debug.Log ("enemy null");
      if (!player.playerTurn) {
        player.playerTurn = true;
      }
    }
  }
  public void getPlayerPosition (GameObject target) {

    playerPosition = target.transform.position;
    ChasePlayer ();

  }

  public void ChasePlayer () {

    int xDirection = 0;
    int yDirection = 0;

    if (Mathf.Abs (playerPosition.x - transform.position.x) < float.Epsilon) {
      yDirection = playerPosition.y > transform.position.y ? 1 : -1;
    } else {
      xDirection = playerPosition.x > transform.position.x ? 1 : -1;
    }

    Vector2 start = transform.position;
    Vector2 end = start + new Vector2 (xDirection, yDirection);
    if (!player.playerTurn) {
      transform.position = end;
      player.playerTurn = true;
    }

  }

  void attemptMove () {

  }

  RaycastHit2D getHitObject (Vector2 position) {
    Ray ray = new Ray (position, transform.forward);
    return Physics2D.Raycast ((Vector2) ray.origin, (Vector2) ray.direction, 10);
  }

  public void abortChase () {
    GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
  }

}