using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
	[SerializeField] GameObject Main;
	private float frame=0;

	void Update()
	{
		//if(Main.activeSelf)
		{
			float scroll = Mathf.Repeat(frame * -0.02f, 1);
			//Debug.Log(scroll);
			Vector2 offset = new Vector2(0, scroll);
			GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
			frame += Time.deltaTime;
		}
	}
}
