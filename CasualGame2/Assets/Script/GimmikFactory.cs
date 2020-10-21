using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmikFactory : MonoBehaviour
{
	[SerializeField, Header("ギミックオブジェクト")] GameObject[] g_GimmikObj=null;
	[SerializeField, Header("出る時間")] int Repop = 0;
	[SerializeField, Header("月オブジェクト")] GameObject g_MoonObj = null;
	bool Stop;
	int frame;

	private void Start()
	{
		frame = 0;
		Stop = false;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if(!Stop)
		{
			frame++;
			if (frame % Repop == 0)
			{
				int i = Random.Range(0, g_GimmikObj.Length);
				Instantiate(g_GimmikObj[i],
					new Vector3(g_GimmikObj[i].transform.position.x,
					this.transform.position.y,
					this.transform.position.z), Quaternion.identity);
			}
		}
	}

	public void CreateMoon()
	{
		Instantiate(g_MoonObj,
				new Vector3(g_MoonObj.transform.position.x,
				this.transform.position.y,
				this.transform.position.z), Quaternion.identity);
	}

	public void StopFactory()
	{
		Stop = true;
	}
}
