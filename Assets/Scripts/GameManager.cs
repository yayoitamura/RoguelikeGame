using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	Dungeon dungeon;

	//UI
	[SerializeField] GameObject title;
	[SerializeField] GameObject gameOver;
	[SerializeField] GameObject fade;
	static int stage = 1;
	public static int playerHp = 5;
	Text stageText;
	Text Hp;

	void Update () {

		switch (GameState.instance.state) {
			case GameState.Game.READY:
				title.SetActive (true);
				fade.SetActive (true);
				GameState.instance.state = GameState.Game.START;
				break;
			case GameState.Game.START:
				//スタート
				//ボタン操作○ プレイヤ× 敵×
				//メニュー画面
				break;
			case GameState.Game.PREPARE:
				//準備
				//ボタン操作× プレイヤ× 敵×
				//シーン以降・フェード・ダンジョン生成
				Prepare ();
				GameObject.Find ("Man").GetComponent<Player> ().PlayerSetUp ();
				GameState.instance.state = GameState.Game.PLAYING;
				break;
			case GameState.Game.PLAYING:

				GameObject.Find ("Main Camera").GetComponent<CameraControl> ().PlayerLookOn ();
				//プレイ中
				//ボタン操作○ プレイヤ○ 敵○
				//プレイ開始→プレイ終了（死or階段）
				break;
			case GameState.Game.END:
				//ゲーム終了
				//ボタン操作○ プレイヤ× 敵×
				//ゲームオーバー/クリア画面
				break;
		}
	}

	public void PushPlayButton () {
		Debug.Log ("startbottun");
		title.SetActive (false);
		GameState.instance.state = GameState.Game.PREPARE;

	}

	void Prepare () {
		fade = GameObject.Find ("Fade");
		dungeon = GameObject.Find ("Dungeon").GetComponent<Dungeon> ();
		stageText = GameObject.Find ("Stage").GetComponent<Text> ();
		Hp = GameObject.Find ("Hp").GetComponent<Text> ();

		stageText.text = "stage : " + stage;
		Hp.text = "HP : " + playerHp;

		SetFadeIn ();
		dungeon.DungeonGenerate ();
	}

	//fade
	void SetFadeIn () {
		fade.SetActive (true);
		fade.GetComponent<FadeControl> ().isFadeIn = true;
	}

	void SetFadeOut () {
		fade.SetActive (true);
		fade.GetComponent<FadeControl> ().isFadeOut = true;
	}

	//ui

	//status
	public void UpdatePlayerHp (int getPlayerHp) {
		playerHp = getPlayerHp;
		Hp.text = "HP : " + playerHp;
	}

	//scene
	public void LoadNextStage () {
		const int nextStage = 0;
		stage++;
		StartCoroutine ("LoadScene", nextStage);
	}

	public void LoadGameOver () {
		gameOver.SetActive (true);
	}

	public void LoadTitle () {
		gameOver.SetActive (false);
		SceneManager.LoadScene (0);
		GameState.instance.state = GameState.Game.READY;
	}

	public void LoadContinue () {
		gameOver.SetActive (false);

		GameState.instance.state = GameState.Game.READY;
		SceneManager.LoadScene (0);
	}

	private IEnumerator LoadScene (int sceneIndex) {

		SetFadeOut ();
		yield return new WaitForSeconds (1.0f);

		SceneManager.LoadScene (sceneIndex);
		yield return new WaitForSeconds (1.0f);

		GameState.instance.state = GameState.Game.PREPARE;

	}
}