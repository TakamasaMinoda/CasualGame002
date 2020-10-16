using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hand : MonoBehaviour
{
	[SerializeField, Header("星の動く速さ")] float speed=0;
	[SerializeField, Header("手を出す方向")] float g_DirX = 0;

	private void Start()
	{
		transform.DOMoveX(1*g_DirX, 2);
	}

	// Start is called before the first frame update
	void Update()
	{
		transform.Translate(0, -speed * Time.deltaTime, 0.0f, Space.World);

		//画面外に出たら削除
		if (transform.position.y < -6)
		{
			if (this.transform.parent)
			{
				Destroy(this.transform.parent.gameObject);
			}
			else
			{
				Destroy(this.gameObject);
			}
		}
	}
}
