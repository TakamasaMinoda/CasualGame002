using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmikFactory : MonoBehaviour
{
	[SerializeField, Header("ギミックオブジェクト")] GameObject[] g_GimmikObj;
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
			Instantiate(g_GimmikObj[i], this.transform.position, Quaternion.identity);
		}
	}
}
