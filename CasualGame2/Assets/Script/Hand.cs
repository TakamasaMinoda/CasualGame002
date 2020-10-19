using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hand : MonoBehaviour
{
	[SerializeField, Header("星の動く速さ")] float speed=0;
	[SerializeField, Header("手を出す方向")] float g_DirX = 0;
	[SerializeField, Header("手を距離")] float g_Dis = 0;


	private void Start()
	{
		transform.DOMoveX(1*g_DirX, 2);

	}

	// Start is called before the first frame update
	void Update()
	{
		transform.Translate(0.0f, -speed * Time.deltaTime, 0.0f, Space.World);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("DeadZone"))
		{
			//親がいる場合
			if (this.gameObject.transform.parent)
			{
				gameObject.transform.parent.gameObject.GetComponent<ObstacleCon>().SubChildCount();
			}
			Destroy(this.gameObject);
		}
	}
}
