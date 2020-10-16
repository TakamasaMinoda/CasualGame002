using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
	[SerializeField, Header("星の動く速さ")] float speed = 0;

	[SerializeField, Header("親オブジェクト")] GameObject g_ParentObj=null;
	[SerializeField, Header("星の親プログラム")] StarCon g_StarConCS = null;
	
	private void Awake()
	{
		g_ParentObj = transform.parent.gameObject;
		g_StarConCS = g_ParentObj.GetComponent<StarCon>();
	}

	private void Update()
	{
		transform.Translate(0.0f, -speed * Time.deltaTime, 0.0f);

		//画面外に出たら削除
		if(transform.position.y<-6)
		{
			Destroy(this.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			g_StarConCS.SubChildCount();
			Destroy(this.gameObject);
		}
	}
}
