using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

	private AudioSource aud;

	public bool hit = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (!tag.Equals("Drum") && other.tag.Equals("Player"))
		{
			if (tag.Equals("Guitar"))
			{
				Guitar.instance.EnPower();
				ScoreManager.instance.AddScore(2);
			}
			else
			{
				ScoreManager.instance.AddScore();
			}
			hit = true;
			aud=GetComponent<AudioSource>();
			GetComponent<MeshRenderer>().enabled = false;
			aud.clip = Resources.Load<AudioClip>(name);
			aud.Play();
			StartCoroutine("DelayDestroy");
		}
	}

	IEnumerator DelayDestroy()
	{
		yield return new WaitForSeconds(3);
		Destroy(gameObject);
	}

	public void Hit()
	{
		hit = true;
		aud=GetComponent<AudioSource>();
		aud.clip = Resources.Load<AudioClip>(name);
		aud.Play();
		GetComponent<MeshRenderer>().enabled = false;
		StartCoroutine("DelayDestroy");
	}
	
}
