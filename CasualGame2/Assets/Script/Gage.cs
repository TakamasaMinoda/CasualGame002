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
	[SerializeField, Header("Factory")] GimmikFactory FactoryCS = null;

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
		if (FactoryCS == null)
		{
			GameObject FactoryObj = GameObject.Find("GimmikFactory");
			FactoryCS = FactoryObj.GetComponent<GimmikFactory>();
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
				//月出現//スクロール停止

				//工場停止
				FactoryCS.StopFactory();

				//オブジェクトが全部消えたら月生成
				GameObject[] Gomi = GameObject.FindGameObjectsWithTag("JumpObj");
				foreach (GameObject gomi in Gomi)
				{
					if (gomi != null)
					{
						return;
					}
				}
				
				Gomi = GameObject.FindGameObjectsWithTag("SlidingObj");
				foreach (GameObject gomi in Gomi)
				{
					if (gomi != null)
					{
						return;
					}
				}
				Gomi = GameObject.FindGameObjectsWithTag("Block");
				foreach (GameObject gomi in Gomi)
				{
					if (gomi != null)
					{
						return;
					}
				}
				Gomi = GameObject.FindGameObjectsWithTag("Star");
				foreach (GameObject gomi in Gomi)
				{
					if (gomi != null)
					{
						return;
					}
				}

				//月生成
				FactoryCS.CreateMoon();
				Destroy(this);
			}
		}
		else //ゲームオーバー
		{
			UnInit();

			GameObject FadeObj = GameObject.Find("Fade");
			FadeObj.GetComponent<FadeSprite>().FadeInGameOver();
		}	
	}

	public void UnInit()
	{
		GameObject[] Gomi = GameObject.FindGameObjectsWithTag("JumpObj");
		foreach (GameObject gomi in Gomi)
		{
			Destroy(gomi);
		}

		Gomi = GameObject.FindGameObjectsWithTag("SlidingObj");
		foreach (GameObject gomi in Gomi)
		{
			Destroy(gomi);
		}

		Gomi = GameObject.FindGameObjectsWithTag("Block");
		foreach (GameObject gomi in Gomi)
		{
			Destroy(gomi);
		}

		Gomi = GameObject.FindGameObjectsWithTag("Star");
		foreach (GameObject gomi in Gomi)
		{
			Destroy(gomi);
		}
	}
}
