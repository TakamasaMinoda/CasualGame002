using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gage : MonoBehaviour
{
	[SerializeField, Header("クリアの距離")] int GoalDis;
	[SerializeField, Header("今の距離")] float NowDis;
	[SerializeField, Header("プレイヤー")] GameObject PlayerObj;

	Image ImageCS;

	void Start()
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
			}

		}
		else
		{
			//ゲームオーバー
		}
		
	}
}
