using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
  Player player;
  SearchCharacter search;
  Vector2　 playerPosition;
  bool isMovable;

  void Start () {
    player = GameObject.Find ("Man").GetComponent<Player> ();
    search = transform.GetChild (0).gameObject.GetComponent<SearchCharacter> ();
  }

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
      player.playerTurn = true;
    } else {
      transform.position = end;
      player.playerTurn = true;
    }
  }

  RaycastHit2D getHitObject (Vector2 position) {
    Ray ray = new Ray (position, transform.forward);
    return Physics2D.Raycast ((Vector2) ray.origin, (Vector2) ray.direction, 10);
  }

  public void abortChase () {
    GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
  }

}