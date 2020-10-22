using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCon : MonoBehaviour
{
	[SerializeField, Header("星の動く速さ")] float speed;
	[SerializeField, Header("星の数")] int ChildCount = 0;
	[SerializeField, Header("星取得失敗数")] int FailedCount = 0;
	[SerializeField, Header("完璧テキストオブジェクト")] RatingText g_RatingCS;
	[SerializeField, Header("スコアプログラム")] Score g_ScoreCS = null;

	//星の仕様をどうしたいの？？


	private void Awake()
	{
		GameObject g_ScoreObj = GameObject.Find("ScoreText");
		g_ScoreCS = g_ScoreObj.GetComponent<Score>();

		GameObject g_CompleteObj = GameObject.Find("RatingText");
		g_RatingCS = g_CompleteObj.GetComponent<RatingText>();
	}

	void Start()
	{
		foreach (Transform child in transform)
		{
			ChildCount++;
		}
	}

	void Update()
	{
		//星をすべて取れた
		if (FailedCount == 0&& ChildCount==0)
		{
			g_ScoreCS.AddScore(5000);
			g_RatingCS.ActiveText();
			Destroy(this.gameObject);
		}
		else if(ChildCount == 0)
		{
			Destroy(this.gameObject);
		}
		
	}

	/// <summary>
	/// 星を取れた数を数え、スコアを加算する
	/// </summary>
	public void Success()
	{
		ChildCount--;
		g_ScoreCS.AddScore(500);
	}

	/// <summary>
	/// 失敗した場合
	/// </summary>
	public void Failed()
	{
		ChildCount--;
		FailedCount++;
	}

}
