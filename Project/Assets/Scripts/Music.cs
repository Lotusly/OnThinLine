using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

	private AudioSource aud;
	// Use this for initialization
	void Start ()
	{
		aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FadeOut()
	{
		StartCoroutine(fadeOut());
	}

	private IEnumerator fadeOut()
	{
		yield return null;
		float ori = aud.volume;
		float tmp = ori;
		while (true)
		{
			tmp = aud.volume - Time.deltaTime * ori;
			if (tmp < 0)
			{
				aud.volume = 0;
				aud.Stop();
				break;
			}
			else
			{
				aud.volume = tmp;
				yield return new WaitForEndOfFrame();
			}
		}
	}
}
