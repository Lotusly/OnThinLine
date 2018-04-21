using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerControl : MonoBehaviour
{

	private float original;
	[SerializeField] private LineRenderer history;

	[SerializeField] private float sinsitive;

	// Use this for initialization
	void Start ()
	{
		//original = Input.GetAxis("Vertical");
		original=0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float speed = sinsitive * (Input.GetAxis("Vertical")-original-transform.localPosition.y);
		transform.localPosition += Vector3.up * Time.deltaTime * speed;
		//transform.localPosition = Vector3.up * (Input.GetAxis("Vertical")-original);
		history.positionCount++;
		history.SetPosition(history.positionCount-1,transform.position);
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Piano"))
		{
			SoundController.instance.PlaySound(Int32.Parse(other.name));
			Destroy(other.gameObject);
		}
	}
}
