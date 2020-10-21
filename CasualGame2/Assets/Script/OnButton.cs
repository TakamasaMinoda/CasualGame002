using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class OnButton : MonoBehaviour
{
	[SerializeField] GameObject Common;
	[SerializeField] GameObject Title;
	[SerializeField] GameObject Main;
	[SerializeField, Header("プレイヤー")] GameObject g_PlayerObj;
	[SerializeField, Header("カメラ")] GameObject g_CameraObj;

	public void OnStartButton()
	{
		//オープニングを始める //カメラとキャラクターを移動 //移動終了したらMainActiveをtrueにする
		Sequence Opening = DOTween.Sequence()
						.OnStart(() =>
						{
							Destroy(gameObject.GetComponent<Image>());
							Common.SetActive(true);
						})
						.Append(g_PlayerObj.transform.DOMove(new Vector3(0f, 0f, 0), 7.0f))
						.Join(g_PlayerObj.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 7.0f))
						.Join(g_CameraObj.transform.DOMove(new Vector3(0f, 0f, -10), 7.0f))
						.OnComplete(() =>
						{
							Title.SetActive(false);
							Main.SetActive(true);
						});
	}
}
