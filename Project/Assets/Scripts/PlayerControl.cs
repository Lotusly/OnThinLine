using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerControl : MonoBehaviour
{

	private float original;
	[SerializeField] private LineRenderer history;

	[SerializeField] private float sinsitive;

	private Vector3 speed = Vector3.zero;

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
		Vector3 espectedSpeed=new Vector3(Input.GetAxis("Vertical")-original-transform.localPosition.y,0,0)*sinsitive;
		speed = speed + (espectedSpeed - speed) * Time.deltaTime * 10f;
		//if (speed.magnitude > 7) speed = speed * (7 / speed.magnitude);
		transform.localPosition += Vector3.up * Time.deltaTime * speed.x ;
		transform.LookAt(transform.position+new Vector3(-3,-speed.x,0));
		
		//float speed;
		//speed=sinsitive * (Input.GetAxis("Vertical")-original-transform.localPosition.y);
		//transform.localPosition += Vector3.up * Time.deltaTime * speed;
		//-----------------------end 1 dimensional control speed------------------------------------
		
		//--------------2 dimensional control speed----------------------------
		/*Vector3 espectedSpeed=new Vector3(Input.GetAxis("Vertical")-original-transform.localPosition.y,Input.GetAxis("Horizontal")-transform.localPosition.z,0)*sinsitive;
		speed = speed + (espectedSpeed - speed) * Time.deltaTime * 10f;
		//if (speed.magnitude > 7) speed = speed * (7 / speed.magnitude);
		transform.localPosition += Vector3.up * Time.deltaTime * speed.x + Vector3.forward*Time.deltaTime*speed.y;
		transform.LookAt(transform.position+new Vector3(-3,speed.x,-speed.y));*/
		
		//-----------------end 2 dimensional control speed-------------------------------------------
		
		
		
		//--------------------control position-----------------------------------
		//transform.localPosition = Vector3.up * (Input.GetAxis("Vertical")-original);
		//-----------------end control position-------------------------------------------
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
