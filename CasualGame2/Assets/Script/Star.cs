using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Star : MonoBehaviour
{
	[SerializeField, Header("星の動く速さ")] float speed = 0;

	[SerializeField, Header("親オブジェクト")] GameObject g_ParentObj=null;
	[SerializeField, Header("星の親プログラム")] StarCon g_StarConCS = null;

	private void Awake()
	{
		g_ParentObj = transform.parent.gameObject;
		g_StarConCS = g_ParentObj.GetComponent<StarCon>();
	}

	private void Update()
	{
		transform.Translate(0.0f, -speed * Time.deltaTime, 0.0f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			GetAnimation();
		}

		if(collision.gameObject.CompareTag("DeadZone"))
		{
			g_StarConCS.Failed();
			Destroy(this.gameObject);
		}
	}

	void GetAnimation()
	{
		Sequence GetStar = DOTween.Sequence()
					.Append(transform.DOScale(new Vector3(0.1f, 0.1f, 1), 1.0f))
					.Join(transform.DOMove(new Vector3(0.0f, 7.5f, 0), 1))
					.OnComplete(() =>
					{
						g_StarConCS.Success();
						Destroy(this.gameObject);
					});
	}
}
