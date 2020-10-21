using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Moon : MonoBehaviour
{
	[SerializeField, Header("プレイヤーオブジェクト")] GameObject g_PlayerObj = null;

	/// <summary>
	/// 月が中央に移動。止まる。月にに姫が着陸するアニメーションを作りたい
	/// </summary>

	private void Start()
	{
		//プレイヤー操作を削除
		g_PlayerObj = GameObject.Find("Player");
		Destroy(g_PlayerObj.GetComponent<PlayerController>());

		Sequence MoveMoon = DOTween.Sequence()
			.Append(transform.DOMove(new Vector3(0, 1.5f, 0), 3))
			.OnComplete(() =>
			{
				PlayerAnimation();
			});
	}

	private void PlayerAnimation()
	{
		Sequence PlayerArriveMoon = DOTween.Sequence()
			.Append(g_PlayerObj.transform.DOMove(new Vector3(0, 1.5f, 0), 2))
			.Append(g_PlayerObj.transform.DOScale(new Vector3(0.0f, 0.0f, 1), 2))
			.OnComplete(() =>
			{
				//クリア画面へ移行
				GameObject FadeObj = GameObject.Find("Fade");
				FadeObj.GetComponent<FadeSprite>().FadeIn();
			});
	}
}
