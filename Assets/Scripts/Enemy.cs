using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

  GameObject man;

  void Start () {
    man = GameObject.Find ("Man");
  }

}