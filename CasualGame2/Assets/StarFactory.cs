using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFactory : MonoBehaviour
{
	[SerializeField, Header("星オブジェクト")] GameObject[] g_StarsObj;
	[SerializeField, Header("星種類")] int g_MaxType;

	int frame;

	private void Start()
	{
		frame = 0;

		g_MaxType = g_StarsObj.Length;
	}

	// Update is called once per frame
	void FixedUpdate()
    {
		frame++;

		if (frame%250==0)
		{
			int i=Random.Range(0, g_MaxType);
			Instantiate(g_StarsObj[i],this.transform.position,Quaternion.identity);
		}
    }
}
