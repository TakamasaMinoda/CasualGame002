﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using AIE2D;

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
	[SerializeField, Header("プレイヤーのスプライト")] private SpriteRenderer g_SpriteSrc=null;

	[SerializeField, Header("残像エフェクト")] StaticAfterImageEffect2DPlayer g_AfterCS;

	[SerializeField, Header("スコアプログラム")] Score g_ScoreCS = null;

	[SerializeField, Header("演出エフェクト")] GameObject g_GoodEffectObj;

	bool tutorialPlayed=false;

	private void Start()
	{
		Direction = "None";
		PlayerStatus = "None";
		g_EffectObj.SetActive(false);
		g_AfterCS = GetComponent<StaticAfterImageEffect2DPlayer>();
		g_GoodEffectObj.SetActive(false);
	}

	void Flick()
	{
		if (Input.GetMouseButtonDown(0))
		{
			touchStartPos = new Vector3(Input.mousePosition.x,
										Input.mousePosition.y,
										Input.mousePosition.z);
			g_EffectObj.SetActive(true);
		}

		if (Input.GetMouseButton(0))
		{
			//エフェクトをカーソルの位置に出す
			Vector3 position = Input.mousePosition;
			position.z = 10f;
			touchNowPos = Camera.main.ScreenToWorldPoint(position);
			g_EffectObj.transform.position = touchNowPos;
		}

		if (Input.GetMouseButtonUp(0))
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

		if (SceneManager.GetActiveScene().name == "Main")
		{
			if (this.transform.position.y <= -4)
			{
				Death();
			}

			switch (Direction)
			{
				case "up":
					if (PlayerStatus == "None")
					{
						Sequence JumpAnim = DOTween.Sequence()
						.OnStart(() =>
						{
							PlayerStatus = "Jump";
							g_AfterCS.SetActive(false);
						})
						.Append(transform.DOScale(new Vector3(1.25f, 1.25f, 1), 1.0f))
						.Append(transform.DOScale(new Vector3(1.0f, 1.0f, 1), 1.0f))
						.OnComplete(() =>
						{
							PlayerStatus = "None";
							g_AfterCS.SetActive(true);
						});
					}

					break;

				case "down":
					//下フリックされた時の処理
					if (PlayerStatus == "None")
					{
						Sequence SlidingAnim = DOTween.Sequence()
						.OnStart(() =>
						{
							PlayerStatus = "Sliding";
							g_AfterCS.SetActive(false);
						})
						.Append(transform.DOScale(new Vector3(0.75f, 0.75f, 1), 1.0f))
						.Append(transform.DOScale(new Vector3(1.0f, 1.0f, 1), 1.0f))
						.OnComplete(() =>
						{
							PlayerStatus = "None";
							g_AfterCS.SetActive(true);
						})
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
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("JumpObj"))
		{
			if(PlayerStatus != "Jump" && PlayerStatus != "Damaged")
			{
				Damaged();
			}
			if(PlayerStatus == "Jump" && PlayerStatus != "Damaged")
			{
				//アクロバットポイント
				g_ScoreCS.AddScore(2000);
				StartCoroutine("SetGoodEffect");
			}
		}

		if (collision.gameObject.CompareTag("SlidingObj"))
		{
			if (PlayerStatus != "Sliding" && PlayerStatus != "Damaged")
			{
				Damaged();
			}

			if (PlayerStatus == "Sliding" && PlayerStatus != "Damaged")
			{
				//アクロバットポイント
				g_ScoreCS.AddScore(2000);
				StartCoroutine("SetGoodEffect");
			}
		}

		if (collision.gameObject.CompareTag("Block"))
		{
			if (PlayerStatus != "Damaged")
			{
				Damaged();
			}

		}
	}

	void Damaged()
	{
		NextPos = this.transform.position.y - 1.5f;

		Sequence damage = DOTween.Sequence()
						.OnStart(() =>
						{
							PlayerStatus = "Damaged";
							g_AfterCS.SetActive(false);
						})
						.Append(transform.DOMoveY(NextPos, 1.5f))
						.Join(DOTween.ToAlpha(() => g_SpriteSrc.color, color => g_SpriteSrc.color = color, 0, 0.5f).SetLoops(4, LoopType.Yoyo))
						.OnComplete(() =>
						{
							PlayerStatus = "None";
							g_SpriteSrc.color = new Color(1, 1, 1, 1);
							g_AfterCS.SetActive(true);
						});
	}

	void Death()
	{
		Sequence Death = DOTween.Sequence()
						.OnStart(() =>
						{
							PlayerStatus = "Death";
							g_AfterCS.SetActive(false);
						})
						.Append(transform.DOScale(new Vector3(0.1f, 0.1f, 1), 2.0f))
						.Join(transform.DOLocalRotate(new Vector3(0, 0, 360f), 2f, RotateMode.FastBeyond360))
						.OnComplete(() => gameObject.SetActive(false));
	}

	IEnumerator SetGoodEffect()
	{
		g_GoodEffectObj.SetActive(true);
		yield return new WaitForSeconds(2);
		g_GoodEffectObj.SetActive(false);
	}
}
