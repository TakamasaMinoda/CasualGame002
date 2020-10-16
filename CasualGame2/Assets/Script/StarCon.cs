using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCon : MonoBehaviour
{
	[SerializeField, Header("星の動く速さ")] float speed = 0;

	[SerializeField, Header("星の数")] int ChildCount = 0;
	[SerializeField, Header("完璧テキストオブジェクト")] GameObject g_CompleteObj;

	[SerializeField, Header("スコアオブジェクト")] GameObject g_ScoreObj = null;
	[SerializeField, Header("スコアプログラム")] Score g_ScoreCS = null;

	private void Awake()
	{
		g_ScoreObj = GameObject.Find("ScoreText");
		g_ScoreCS = g_ScoreObj.GetComponent<Score>();
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
		if (ChildCount<=0)
		{
			g_ScoreCS.AddScore(1000);
			Destroy(this.gameObject);
		}

		transform.Translate(0.0f, -speed * Time.deltaTime, 0.0f);
		if(transform.position.y<-6)
		{
			Destroy(this.gameObject);
		}
	}

	/// <summary>
	/// 星を取れた数を数え、スコアを加算する
	/// </summary>
	public void SubChildCount()
	{
		ChildCount--;
		g_ScoreCS.AddScore(100);
	}
}
