using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Gage : MonoBehaviour
{
	[SerializeField, Header("クリアの距離")] int GoalDis=0;
	[SerializeField, Header("今の距離")] float NowDis=0;
	[SerializeField, Header("プレイヤー")] GameObject PlayerObj=null;
	[SerializeField, Header("カメラ")] GameObject CameraObj = null;


	[SerializeField, Header("Main")] GameObject MainObj = null;
	[SerializeField, Header("Result")] GameObject ResultObj = null;
	[SerializeField, Header("GameOver")] GameObject GameOverObj = null;
	[SerializeField, Header("Factory")] GimmikFactory FactoryCS = null;
	[SerializeField, Header("Crear")] GameObject CrearObj = null;

	[SerializeField, Header("スコア")] GameObject MainScore;
	[SerializeField, Header("スコア")] GameObject ClearScore;
	[SerializeField, Header("スコア")] Score g_ScoreCS;

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
	bool bag = false;

	private void Start()
	{
		ImageCS.fillAmount = 0;
		NowDis = 0;
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
			else if(ImageCS.fillAmount==1)  //クリア
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

				//月生成　//プレイヤーとカメラを上にうごかすかつ月までの道をtrueにする
				//FactoryCS.CreateMoon();
				if(!bag)
				{
					bag = true;
					Sequence Ending = DOTween.Sequence()
						.OnStart(() =>
						{
							CrearObj.SetActive(true);
						})
						.Append(PlayerObj.transform.DOMove(new Vector3(0f, 21.5f, 0), 7.0f))
						.Join(CameraObj.transform.DOMove(new Vector3(0f, 21.5f, -10), 7.0f))
						.OnComplete(() =>
						{
							Sequence PlayerArriveMoon = DOTween.Sequence()
							.Append(PlayerObj.transform.DOScale(new Vector3(0.0f, 0.0f, 1), 2))
							.OnComplete(() =>
							{
								//クリア画面へ移行
								GameObject FadeObj = GameObject.Find("Fade");
								FadeObj.GetComponent<FadeSprite>().FadeIn();

								//スコア受け渡し
								int temp = g_ScoreCS.GetScore();
								MainScore.SetActive(false);
								ClearScore.SetActive(true);
								ClearScore.GetComponent<Text>().text = "スコア:" + temp.ToString();
							});
						});
				}
				
			}
		}
		else //ゲームオーバー
		{
			UnInit();
			if (!bag)
			{
				bag = true;
				GameObject FadeObj = GameObject.Find("Fade");
				FadeObj.GetComponent<FadeSprite>().FadeInGameOver();
				MainScore.SetActive(false);
			}
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
