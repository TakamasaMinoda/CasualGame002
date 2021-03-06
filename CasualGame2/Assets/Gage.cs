﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gage : MonoBehaviour
{
	[SerializeField, Header("クリアの距離")] int GoalDis=0;
	[SerializeField, Header("今の距離")] float NowDis=0;
	[SerializeField, Header("プレイヤー")] GameObject PlayerObj=null;

	Image ImageCS;

	void Awake()
	{
		ImageCS = GetComponent<Image>();
		ImageCS.fillAmount = 0;
		NowDis = 0;
	}

	private void Update()
	{
		if(PlayerObj.activeSelf)
		{
			if (ImageCS.fillAmount < 1)
			{
				//ゲーム中
				NowDis += Time.deltaTime;
				ImageCS.fillAmount = NowDis / GoalDis;
			}
			else
			{
				//クリア
				Debug.Log("クリア");
			}

		}
		else
		{
			//ゲームオーバー
			Debug.Log("ゲームオーバー");
		}
		
	}
}
