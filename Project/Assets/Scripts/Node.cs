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
		if (other.tag.Equals("Player"))
		{
			hit = true;
			aud=GetComponent<AudioSource>();
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
}
