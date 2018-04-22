using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Drum : MonoBehaviour
{

	[SerializeField] private LineRenderer history;


	// Use this for initialization
	void Start ()
	{
	
		//original=0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		history.positionCount++;
		history.SetPosition(history.positionCount-1,transform.position);
	
	}

	/*void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Node"))
		{
			SoundController.instance.PlaySound(other.name);
			Destroy(other.gameObject);
		}
	}*/
}
