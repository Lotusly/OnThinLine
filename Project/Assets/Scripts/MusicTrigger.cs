using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{

	[SerializeField] private int index;
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
			SoundController.instance.PlayMusic(index);
			Destroy(gameObject);
		}
	}
}
