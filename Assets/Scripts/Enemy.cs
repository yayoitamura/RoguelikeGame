using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
  bool enemyTurn;

  GameObject man;

  void Start () {
    man = GameObject.Find ("Man");
  }

  public void ChasePlayer (GameObject target) {
    Debug.Log ("move enemy");
    Vector2　 targetPsition = target.transform.position;

    float x = targetPsition.x;
    float y = targetPsition.y;
    Vector2 direction = new Vector2 (x - transform.position.x, y - transform.position.y).normalized;
    GetComponent<Rigidbody2D> ().velocity = (direction * 2);

  }

  public void dontChase () {
    GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
  }
}