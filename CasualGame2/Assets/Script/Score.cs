using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Score : MonoBehaviour
{
	Text TextCS=null;
	[SerializeField, Header("点数")] int g_score=0;

    // Start is called before the first frame update
    void Awake()
    {
		TextCS = GetComponent<Text>();
		TextCS.text= "スコア:" + g_score.ToString();
	}

	/// <summary>
	/// 点数を加算しスコアに反映する
	/// </summary>
	/// <param name="ten">点数</param>
	public void AddScore(int ten)
	{
		g_score += ten;
		TextCS.text = "スコア:" + g_score.ToString();
	}

	public void AnimationResult()
	{
		Sequence Anim = DOTween.Sequence()
			.Append(DOTween.ToAlpha(() => TextCS.color, color => TextCS.color=color, 0, 2f))
			.Append(this.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(0, -200, 0), 0.1f))
			.Append(DOTween.ToAlpha(() => TextCS.color, color => TextCS.color = color, 1,3))
			;
	}
}
