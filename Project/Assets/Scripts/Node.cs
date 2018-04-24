using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

	private AudioSource aud;

	public bool hit = false;

	public Missle missle;

	//private bool inBattle = false;
	// Use this for initialization
	void Start () {
		missle = GetComponent<Missle>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (hit) return;
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
					//print("inMissle");
					hit = true;
					
					missle.SetTarget(Boss.instance.transform);
					missle.SetMaxSpeed(10);
					missle.SetInUse();
					//print("outMissle");
					Destroy(this);
					//this.enabled = false;
					
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
