using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField, Header("障害物の動く速さ")] float speed=0;

	void Update()
	{
		transform.Translate(0.0f, -speed * Time.deltaTime, 0.0f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("DeadZone"))
		{
			//親がいる場合
			if (this.gameObject.transform.parent)
			{
				gameObject.transform.parent.gameObject.GetComponent<ObstacleCon>().SubChildCount();
			}
			Destroy(this.gameObject);
		}
	}
}
