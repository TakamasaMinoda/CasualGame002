using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

	[SerializeField, Header("障害物の動く速さ")] float speed=0;

	void Update()
	{
		transform.Translate(0.0f, -speed * Time.deltaTime, 0.0f);

		//画面外に出たら削除
		if (transform.position.y < -6)
		{
			if (this.transform.parent)
			{
				Destroy(this.transform.parent.gameObject);
			}
			else
			{
				Destroy(this.gameObject);
			}
		}
	}
}
