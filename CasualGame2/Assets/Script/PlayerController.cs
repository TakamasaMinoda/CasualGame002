using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
	[SerializeField, Header("タッチした座標方向")] private Vector3 touchStartPos = new Vector3(0, 0, 0);
	[SerializeField, Header("タッチを離した座標方向")] private Vector3 touchEndPos = new Vector3(0, 0, 0);
	private float NextPos = 0;

	[SerializeField, Header("フリックした方向")] private string Direction="None";
	[SerializeField, Header("プレイヤーの状態")] private string PlayerStatus = "None";

	[SerializeField, Header("フリックした方向のX量")] float directionX=0;
	[SerializeField, Header("フリックした方向のY量")] float directionY=0;

	//エフェクト追加
	[SerializeField, Header("エフェクトオブジェクト")] private GameObject g_EffectObj=null;
	[SerializeField, Header("スクリーン上の現在位置座標")] private Vector3 touchNowPos=new Vector3(0,0,0);

	private void Start()
	{
		Direction = "None";
		PlayerStatus = "None";
		g_EffectObj.SetActive(false);
	}

	void Flick()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			touchStartPos = new Vector3(Input.mousePosition.x,
										Input.mousePosition.y,
										Input.mousePosition.z);
			g_EffectObj.SetActive(true);
		}

		if (Input.GetKey(KeyCode.Mouse0))
		{
			//エフェクトをカーソルの位置に出す
			Vector3 position = Input.mousePosition;
			position.z = 10f;
			touchNowPos = Camera.main.ScreenToWorldPoint(position);
			g_EffectObj.transform.position = touchNowPos;
		}

		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			touchEndPos = new Vector3(Input.mousePosition.x,
									  Input.mousePosition.y,
									  Input.mousePosition.z);
			GetDirection();
			g_EffectObj.SetActive(false);
		}
	}

	void GetDirection()
	{
		directionX = touchEndPos.x - touchStartPos.x;
		directionY = touchEndPos.y - touchStartPos.y;

		if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
		{
			if (30 < directionX)
			{
				//右向きにフリック
				Direction = "right";
			}
			else if (-30 > directionX)
			{
				//左向きにフリック
				Direction = "left";
			}
		}
		else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
		{
			if (30 < directionY)
			{
				//上向きにフリック
				Direction = "up";
			}
			else if (-30 > directionY)
			{
				//下向きのフリック
				Direction = "down";
			}
		}
	}

	private void Update()
	{
		Flick();

		
		switch (Direction)
		{
			case "up":
				if (PlayerStatus =="None")
				{
					Sequence JumpAnim = DOTween.Sequence()
					.OnStart(() => PlayerStatus = "Jump")
					.Append(transform.DOScale(new Vector3(2.0f, 2.0f, 1), 1.0f))
					.Append(transform.DOScale(new Vector3(1.0f, 1.0f, 1), 1.0f))
					.OnComplete(() => PlayerStatus = "None")
					;
				}

				break;

			case "down":
				//下フリックされた時の処理
				if(PlayerStatus == "None")
				{
					Sequence SlidingAnim = DOTween.Sequence()
					.OnStart(() => PlayerStatus = "Sliding")
					.Append(transform.DOScale(new Vector3(0.3f, 0.3f, 1), 1.0f))
					.Append(transform.DOScale(new Vector3(1.0f, 1.0f, 1), 1.0f))
					.OnComplete(() => PlayerStatus = "None")
					;
				}
				
				break;

			case "right":
				//右フリックされた時の処理
				if (PlayerStatus == "None")
				{
					NextPos = this.transform.position.x + 1.9f;
					if (NextPos > 2)
					{
						return;
					}

					//次の座標へ移動する
					Sequence RightMoveAnim = DOTween.Sequence()
					.OnStart(() => PlayerStatus = "Move")
					.Append(transform.DOMoveX(NextPos, 1))
					.OnComplete(() => PlayerStatus = "None")
					;
				}
				break;

			case "left":
				//左フリックされた時の処理
				if (PlayerStatus == "None")
				{
					NextPos = this.transform.position.x - 1.9f;
					if (NextPos < -2)
					{
						return;
					}
					//次の座標へ移動する
					Sequence LeftMoveAnim = DOTween.Sequence()
					.OnStart(() => PlayerStatus = "Move")
					.Append(transform.DOMoveX(NextPos, 1))
					.OnComplete(() => PlayerStatus = "None")
					;
				}
				break;
		}

		Direction = "None";
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("JumpObj"))
		{
			if(PlayerStatus != "Jump")
			{
				this.gameObject.SetActive(false);
			}
		}

		if (collision.gameObject.CompareTag("SlidingObj"))
		{
			if (PlayerStatus != "Sliding")
			{
				this.gameObject.SetActive(false);
			}
		}

		if (collision.gameObject.CompareTag("Block"))
		{

			this.gameObject.SetActive(false);

		}
	}
}
