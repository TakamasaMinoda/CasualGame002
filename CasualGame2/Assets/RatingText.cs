using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RatingText : MonoBehaviour
{
	[SerializeField, Header("テキスト機能")] Text TextCS;

	private void Awake()
	{
		TextCS = GetComponent<Text>();
		TextCS.color =new Color(1,1,1,0);
	}

	public void ActiveText()
	{
		StartCoroutine("Active");
	}

	IEnumerator Active()
	{
		DOTween.ToAlpha(() => TextCS.color, color => TextCS.color = color,1.0f,1.0f);

		yield return new WaitForSeconds(2);

		DOTween.ToAlpha(() => TextCS.color, color => TextCS.color = color, 0.0f, 1.0f);
	}
}
