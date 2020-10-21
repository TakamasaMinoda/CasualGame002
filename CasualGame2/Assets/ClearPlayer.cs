using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearPlayer : MonoBehaviour
{
	Image m_Image;
	//Set this in the Inspector
	public Sprite[] m_Sprite;
	int cnt;
	float frame;
	[SerializeField, Header("アニメーション時間")] float WaitTime;

	// Start is called before the first frame update
	void Start()
    {
		m_Image = GetComponent<Image>();
		cnt = 0;
	}

    // Update is called once per frame
    void Update()
    {
		frame += Time.deltaTime;
		if (frame> WaitTime)
		{
			cnt++;
			if(cnt>= m_Sprite.Length)
			{
				cnt = 0;
			}
			frame = 0;
			m_Image.sprite = m_Sprite[cnt];
		}
		
	}
}
