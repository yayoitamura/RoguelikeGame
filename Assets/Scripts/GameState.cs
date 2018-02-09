using UnityEngine;

public class GameState : MonoBehaviour {

  public enum Game {
    READY,
    START,
    PREPARE,
    PLAYING,
    END
  }

  public Game state = Game.READY;

  public static GameState instance { get; protected set; }

  void Awake () {

    if (instance == null) {
      instance = this;
    } else if (instance != this) {
      Destroy (gameObject);
    }

    DontDestroyOnLoad (gameObject);

  }
}