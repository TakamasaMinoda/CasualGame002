using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
	private Text g_TextCS = null;
	[SerializeField, Header("テキスト集")] string[] g_Sentence=null;
	[SerializeField, Header("クリアテキスト")] string g_Clear = null;
	[SerializeField, Header("失敗テキスト")] string g_Failed = null;
	[SerializeField, Header("終了テキスト"), TextArea(1, 3)] string g_End = null;

	// Start is called before the first frame update
	void Awake()
    {
		g_TextCS = GetComponent<Text>();
	}

	/// <summary>
	/// チュートリアルテキストの文を設定する
	/// </summary>
	/// <param name="cnt">要素番号</param>
	/// 

	public void SetText(int cnt)
	{
		g_TextCS.text = g_Sentence[cnt];
	}
	
	/// <summary>
	/// クリアテキストをセットする
	/// </summary>
	public void SetClearText()
	{
		g_TextCS.text = g_Clear;
	}

	/// <summary>
	/// 失敗テキストをセットする
	/// </summary>
	public void SetFailedText()
	{
		g_TextCS.text = g_Failed;
	}

	/// <summary>
	/// 終了テキストをセットする
	/// </summary>
	public void SetEndText()
	{
		g_TextCS.text = g_End;
	}
}
