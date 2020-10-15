using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFactory : MonoBehaviour
{
	[SerializeField, Header("ギミックオブジェクト")] GameObject[] g_GimmikObj;
	[SerializeField, Header("手を出す方向")] float g_DirX;
	[SerializeField, Header("出る時間")] int Repop;

	int frame;

	private void Start()
	{
		frame = 0;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		frame++;

		if (frame % Repop == 0)
		{
			int i = Random.Range(0, g_GimmikObj.Length);
			GameObject copy=Instantiate(g_GimmikObj[i], this.transform.position, Quaternion.identity);
			copy.GetComponent<Hand>().SetDirX(g_DirX);
		}
	}
}
