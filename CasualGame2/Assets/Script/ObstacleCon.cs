using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCon : MonoBehaviour
{
	[SerializeField, Header("障害物の数")] int ChildCount = 0;

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
		//障害物がなくなったら
		if (ChildCount <= 0)
		{
			Destroy(this.gameObject);
		}
	}

	/// <summary>
	/// 障害物の個数を減らす
	/// /// </summary>
	public void SubChildCount()
	{
		ChildCount--;
	}
}
