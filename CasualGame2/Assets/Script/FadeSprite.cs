using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeSprite : MonoBehaviour
{

	[SerializeField, Header("画像")] Image FadeSrc;
	[SerializeField, Header("メイン")] GameObject g_MainObj;
	[SerializeField, Header("リザルト")] GameObject g_ResultObj;


	// Start is called before the first frame update
	void Start()
    {
		FadeSrc = GetComponent<Image>();
		Sequence FadeOut = DOTween.Sequence()
						.OnStart(() =>
						{
							g_MainObj.SetActive(false);
						})
						.Append(DOTween.ToAlpha(() => FadeSrc.color, color => FadeSrc.color = color, 0, 2.5f))
						.OnComplete(() =>
						{
							g_MainObj.SetActive(true);
						});
	}

	public void FadeIn()
	{
		Sequence FadeIn = DOTween.Sequence()
						.OnStart(() =>
						{
							
						})
						.Append(DOTween.ToAlpha(() => FadeSrc.color, color => FadeSrc.color = color, 1, 0.5f))
						.Append(DOTween.ToAlpha(() => FadeSrc.color, color => FadeSrc.color = color, 0, 0.5f))
						.OnComplete(() =>
						{
							g_MainObj.SetActive(false);
						});
	}
}
