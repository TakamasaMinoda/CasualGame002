using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gage : MonoBehaviour
{
	[SerializeField, Header("クリアの距離")] int GoalDis=0;
	[SerializeField, Header("今の距離")] float NowDis=0;
	[SerializeField, Header("プレイヤー")] GameObject PlayerObj=null;


	[SerializeField, Header("Main")] GameObject MainObj = null;
	[SerializeField, Header("Result")] GameObject ResultObj = null;
	[SerializeField, Header("GameOver")] GameObject GameOverObj = null;

	Image ImageCS;

	void Awake()
	{
		ImageCS = GetComponent<Image>();
		ImageCS.fillAmount = 0;
		NowDis = 0;

		if(MainObj == null)
		{
			MainObj = GameObject.Find("Main");
		}
		if (ResultObj == null)
		{
			ResultObj = GameObject.Find("Result");
		}
		if (GameOverObj == null)
		{
			GameOverObj = GameObject.Find("GameOver");
		}
	}

	private void Update()
	{
		if(PlayerObj.activeSelf)
		{
			if (ImageCS.fillAmount < 1)  //ゲーム中
			{
				
				NowDis += Time.deltaTime;
				ImageCS.fillAmount = NowDis / GoalDis;
			}
			else  //クリア
			{
				
				ResultObj.SetActive(true);
				MainObj.SetActive(false);
			}
		}
		else //ゲームオーバー
		{
			GameOverObj.SetActive(true);
			MainObj.SetActive(false);
		}
		
	}
}
