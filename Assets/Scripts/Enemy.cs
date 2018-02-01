using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
  bool enemyTurn;

  GameObject man;
  Vector2　 targetPosition;
  

  void Start () {
    man = GameObject.Find ("Man");
  }

  public void ChasePlayer (GameObject target) {

    Debug.Log ("move enemy");
    targetPosition = target.transform.position;

    float x = targetPosition.x;
    float y = targetPosition.y;
    Vector2 direction = new Vector2 (x - transform.position.x, y - transform.position.y).normalized;
    GetComponent<Rigidbody2D> ().velocity = (direction * 2);
    // hoge ();
  

  }

  public void dontChase () {
    GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
  }

  void hoge () {
    RaycastHit2D hit = getHitObject (targetPosition);

    if (hit.collider) {
      if (hit.collider.gameObject.tag == "player") {
        Debug.Log ("player hit");
        targetPosition = transform.position;
      }
    }
  }

  RaycastHit2D getHitObject (Vector2 position) {
    Ray ray = new Ray (position, transform.forward);
    return Physics2D.Raycast ((Vector2) ray.origin, (Vector2) ray.direction, 10);
  }
}