using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnButton : MonoBehaviour
{
	[SerializeField] GameObject Common;
	[SerializeField] GameObject Title;

	public void OnStartButton()
	{
		Common.SetActive(true);
		Title.SetActive(false);
	}
}
