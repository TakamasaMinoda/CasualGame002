using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hand : MonoBehaviour
{
	[SerializeField, Header("星の動く速さ")] float speed;
	[SerializeField, Header("手を出す方向")] float g_DirX;

	public void SetDirX(float _DirX)
	{
		g_DirX = _DirX;
	}

	private void Start()
	{
		transform.Rotate(0, 0, 90* g_DirX);

		transform.DOMoveX(2 * g_DirX, 2);
	}

	// Start is called before the first frame update
	void Update()
	{
		transform.Translate(g_DirX*speed * Time.deltaTime,0, 0.0f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("DeadZone"))
		{
			Destroy(this.gameObject);
		}
	}
}
