using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
	[SerializeField, Header("親オブジェクト")] GameObject g_ParentObj;
	[SerializeField, Header("星の親プログラム")] StarCon g_StarConCS;

	private void Start()
	{
		g_ParentObj = transform.parent.gameObject;
		g_StarConCS = g_ParentObj.GetComponent<StarCon>();
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
