//  LongPressButton.cs
//  http://kan-kikuchi.hatenablog.com/entry/LongPressButton
//
//  Created by kan.kikuchi on 2016.04.18.

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ボタンを長押しすると、連続でメソッドを実行出来るようにするクラス
/// </summary>
public class Move : MonoBehaviour {
	Vector3 MOVEX = new Vector3 (0.5f, 0, 0);
	Vector2 movePosition;

	//長押しと判定する時間、メソッドを実行する間隔
	[SerializeField]
	private float _longPressTime = 1f, _invokeInterval = 0.2f;

	//長押しと判定するまで or 次のメソッドを実行するまでの時間
	private float _waitTime = 0;

	//押しているか
	private bool _isPressing = false;

	//一度でもメソッドを実行したか
	private bool _isInvokedEvent = false;

	//ボタンを押した瞬間に実行されるメソッド
	public void PressDown () {
		// Debug.Log ("down");
		_isPressing = true;
		_isInvokedEvent = false;
		_waitTime = _longPressTime;
	}

	//ボタンを離した瞬間に実行されるメソッド
	public void PressUp () {
		// Debug.Log ("up");
		_isPressing = false;
	}

	//クリックした瞬間に実行されるメソッド
	public void Click () {
		Debug.Log ("click");
		//一度もイベントが実行されていなければ実行
		if (!_isInvokedEvent) {
			Debug.Log ("1click");
			// _event.Invoke ();
			SetTargetPosition ();
		}
	}

	private void Update () {
		//ボタンが押されていない時はスルー
		if (!_isPressing) {
			return;
		}
		//待ち時間を減らす
		Debug.Log ("1 " + _waitTime);
		_waitTime -= Time.deltaTime;
		Debug.Log ("2 " + _waitTime);
		//待ち時間がまだある時はスルー
		if (_waitTime > 0) {
			return;
		}

		//メソッド実行、待ち時間設定
		// _event.Invoke ();
		SetTargetPosition ();
		_waitTime = _invokeInterval;
		_isInvokedEvent = true;
	}

	public void SetTargetPosition () {

		movePosition = transform.position - MOVEX; //.x -= STEP; // * Time.deltaTime;

		toMove ();
	}

	void toMove () {

		transform.position = movePosition;

	}

}