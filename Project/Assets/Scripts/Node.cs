using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

	private AudioSource aud;

	public bool hit = false;

	//private bool inBattle = false;
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
			aud=GetComponent<AudioSource>();
			aud.clip = Resources.Load<AudioClip>(name);
			aud.Play();
			
			if (tag.Equals("Guitar"))
			{
				Guitar.instance.EnPower();
				ScoreManager.instance.AddScore();
			}
			else
			{
				if (ScoreManager.instance.InBossBattle)
				{
					Missle missle = GetComponent<Missle>();
					missle.enabled = true;
					missle.SetTarget(Boss.instance.transform);
					missle.SetMaxSpeed(10);
					this.enabled = false;
					return;
				}
				else
				{
					ScoreManager.instance.AddScore();
				}
			}
			
			hit = true;		
			GetComponent<MeshRenderer>().enabled = false;		
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
