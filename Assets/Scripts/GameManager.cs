using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	Dungeon dungeon;
	[SerializeField] GameObject title;
	GameObject fade;
	static int stage = 1;
	Text stageText;

	void Awake () {

	}

	void Start () { }

	void Update () {

		switch (GameState.instance.state) {
			case GameState.Game.READY:
				title.SetActive (true);
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
				GameState.instance.state = GameState.Game.PLAYING;
				break;
			case GameState.Game.PLAYING:
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

		title.SetActive (false);
		GameState.instance.state = GameState.Game.PREPARE;

	}

	void Prepare () {
		fade = GameObject.Find ("Fade");
		dungeon = GameObject.Find ("Dungeon").GetComponent<Dungeon> ();
		stageText = GameObject.Find ("Stage").GetComponent<Text> ();
		stageText.text = "stage : " + stage;

		SetFadeIn ();
		dungeon.DungeonGenerate ();
	}
	void SetFadeIn () {
		fade.SetActive (true);
		fade.GetComponent<FadeControl> ().isFadeIn = true;
	}

	void SetFadeOut () {
		fade.SetActive (true);
		fade.GetComponent<FadeControl> ().isFadeOut = true;
		Debug.Log ("SetFadeOut");
	}

	public void LoadNextStage () {

		const int nextStage = 0;
		stage++;

		StartCoroutine ("LoadScene", nextStage);

	}

	private IEnumerator LoadScene (int sceneIndex) {

		SetFadeOut ();
		yield return new WaitForSeconds (1.0f);

		SceneManager.LoadScene (sceneIndex);
		yield return new WaitForSeconds (1.0f);

		GameState.instance.state = GameState.Game.PREPARE;

	}
}