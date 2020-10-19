using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFactory : MonoBehaviour
{
	[SerializeField, Header("イベントトリガー")] List<bool> EventTirgger=new List<bool>();
	[SerializeField, Header("チュートリアルゲームオブジェクト")] GameObject[] TutorialObj=null;
	[SerializeField, Header("チュートリアルテキスト")] GameObject TutorialText=null;
	[SerializeField, Header("チュートリアルテキストスクリプト")] TutorialText TutorialTextCS=null;
	[SerializeField, Header("Playerオブジェクト")] GameObject PlayerObj=null;
	[SerializeField, Header("Resultオブジェクト")] GameObject ResultObj = null;
	[SerializeField, Header("Mainオブジェクト")] GameObject MainObj = null;
	private int cnt=0;

	private void Awake()
	{
		for (int i = 0; i < TutorialObj.Length; i++)
		{
			EventTirgger.Add(false);
		}
		TutorialTextCS = TutorialText.GetComponent<TutorialText>();
		cnt = 0;
	}

	// Start is called before the first frame update
	void Start()
    {
		StartCoroutine("EventPlay");
	}

	/// <summary>
	/// n番目のオブジェクトを生成する->そのチュートリアルを成功できたらn+1番目のオブジェクトを生成する
	/// 失敗したらもう一度n番目のオブジェクトを生成するのループ
	/// </summary>

	private IEnumerator EventPlay()
	{
		if(cnt < EventTirgger.Count)
		{
			GameObject temp = Instantiate(TutorialObj[cnt], this.transform.position, Quaternion.identity);
			TutorialTextCS.SetText(cnt);

			//EventTirgger[i]がtrueになるまでループする
			while (!EventTirgger[cnt])
			{
				//プレイヤーが消えてオブジェクトが破壊された場合
				if (!PlayerObj.activeSelf && temp == null)
				{
					TutorialTextCS.SetFailedText();
					yield return new WaitForSeconds(2);
					temp = Instantiate(TutorialObj[cnt],this.transform.position,Quaternion.identity);
					PlayerObj.SetActive(true);
					TutorialTextCS.SetText(cnt);
				}
				//プレイヤーが生きててオブジェクトが破壊された場合
				else if(PlayerObj.activeSelf && temp == null)
				{
					EventTirgger[cnt] = true;
				}

				yield return new WaitForEndOfFrame();
			}

			//次のイベントを起動する
			cnt++;
			TutorialTextCS.SetClearText();
			yield return new WaitForSeconds(2);
			yield return EventPlay();
		}

		TutorialTextCS.SetEndText();
		yield return new WaitForSeconds(2);
		ResultObj.SetActive(false);
		MainObj.SetActive(true);
	}
}
