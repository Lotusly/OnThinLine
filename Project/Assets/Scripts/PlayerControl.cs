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
		
		//----------------1 dimensional control speed-----------------------
		
		//float speed;
		//speed=sinsitive * (Input.GetAxis("Vertical")-original-transform.localPosition.y);
		//transform.localPosition += Vector3.up * Time.deltaTime * speed;
		//-----------------------end------------------------------------
		
		//--------------2 dimensional control speed----------------------------
		Vector3 speed;
		speed=new Vector2((Input.GetAxis("Vertical")-original-transform.localPosition.y),Input.GetAxis("Horizontal")-transform.localPosition.x)*sinsitive;
		transform.localPosition += Vector3.up * Time.deltaTime * speed.x + Vector3.right*Time.deltaTime*speed.y;
		//-----------------end-------------------------------------------
		
		//--------------------control position-----------------------------------
		//transform.localPosition = Vector3.up * (Input.GetAxis("Vertical")-original);
		//-----------------end-------------------------------------------
		history.positionCount++;
		history.SetPosition(history.positionCount-1,transform.position);
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Node"))
		{
			SoundController.instance.PlaySound(other.name);
			Destroy(other.gameObject);
		}
	}
}
