using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FadeSprite : MonoBehaviour
{

	[SerializeField, Header("画像")] Image FadeSrc;
	[SerializeField, Header("メイン")] GameObject g_MainObj;
	[SerializeField, Header("リザルト")] GameObject g_ResultObj;
	[SerializeField, Header("チュートリアル")] GameObject g_TutorialObj;
	[SerializeField, Header("GameOver")] GameObject g_GameOverObj;
	[SerializeField, Header("オープニング")] GameObject g_OpeningObj;


	//チュートリアル起動

	//// Start is called before the first frame update
	//void Start()
 //   {
	//	FadeSrc = GetComponent<Image>();
	//	Sequence FadeOut = DOTween.Sequence()
	//					.OnStart(() =>
	//					{
	//						g_MainObj.SetActive(false);
	//					})
	//					.Append(DOTween.ToAlpha(() => FadeSrc.color, color => FadeSrc.color = color, 0, 2.5f))
	//					.OnComplete(() =>
	//					{
	//						//山から宇宙に行ってるアニメーション
	//						g_MainObj.SetActive(true);
	//					});
	//}

	public void FadeIn()
	{
		Sequence FadeIn = DOTween.Sequence()
						.OnStart(() =>
						{
							//スコアアニメーション起動
							GameObject.Find("ScoreText").GetComponent<Score>().AnimationResult();
						})
						.Append(DOTween.ToAlpha(() => FadeSrc.color, color => FadeSrc.color = color, 1, 2.5f))
						.OnComplete(() =>
						{
							g_MainObj.SetActive(false);
							g_ResultObj.SetActive(true);
						});
	}

	public void FadeInGameOver()
	{
		Sequence FadeIn = DOTween.Sequence()
						.OnStart(() =>
						{
							//スコアアニメーション起動
							GameObject.Find("ScoreText").GetComponent<Score>().AnimationResult();
						})
						.Append(DOTween.ToAlpha(() => FadeSrc.color, color => FadeSrc.color = color, 1, 2.5f))
						.OnComplete(() =>
						{
							g_MainObj.SetActive(false);
							g_GameOverObj.SetActive(true);
						});
	}
}
