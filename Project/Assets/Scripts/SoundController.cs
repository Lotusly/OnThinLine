﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
	public static SoundController instance;

	[SerializeField] private AudioSource[] sources;

	void Awake()
	{
		if (instance == null) instance = this;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			PlaySound(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			PlaySound(2);
		}
	}

	public void PlaySound(int i)
	{
		sources[0].clip = Resources.Load<AudioClip>("piano" + i.ToString());
		sources[0].Play();
	}

	public void PlaySound(string s)
	{
		sources[1].clip = Resources.Load<AudioClip>(s);
		sources[1].Play();
	}

	public void PlayMusic(int i)
	{
		sources[i].Play();
	}

	public void StopMusic(int i)
	{
		StartCoroutine(stopMusic(i));
	}

	private IEnumerator stopMusic(int i)
	{
		yield return null;
		while (sources[i].volume > 0)
		{
			sources[i].volume -= Time.deltaTime;
			if (sources[i].volume < 0)
			{
				sources[i].volume = 0;
				sources[i].Stop();
				break;
			}
			yield return new WaitForEndOfFrame();
		}
	}
}
