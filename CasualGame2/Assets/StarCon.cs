using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCon : MonoBehaviour
{
	[SerializeField, Header("星の動く速さ")] float speed;

	[SerializeField, Header("星の数")] int ChildCount = 0;

	// Start is called before the first frame update
	void Start()
	{
		foreach (Transform child in transform)
		{
			ChildCount++;
		}
	}

	// Update is called once per frame
	void Update()
	{
		transform.Translate(0.0f, speed * Time.deltaTime, 0.0f);
	}
}
