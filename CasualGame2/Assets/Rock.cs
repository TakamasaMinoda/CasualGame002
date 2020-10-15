using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
	[SerializeField, Header("星の動く速さ")] float speed;

	// Update is called once per frame
	void Update()
    {
		transform.Translate(0.0f, -speed * Time.deltaTime, 0.0f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("DeadZone"))
		{
			Destroy(this.gameObject);
		}
	}
}
