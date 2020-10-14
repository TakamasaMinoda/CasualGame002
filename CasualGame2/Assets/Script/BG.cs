using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
	void Update()
	{
		float scroll = Mathf.Repeat(Time.time * -0.02f, 1);
		Vector2 offset = new Vector2(0, scroll);
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
	}
}
