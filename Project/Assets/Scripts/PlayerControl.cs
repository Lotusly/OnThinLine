using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerControl : MonoBehaviour
{

	public static PlayerControl instance;
	private float original;
	[SerializeField] private LineRenderer history;

	[SerializeField] private float sinsitive;

	private Vector3 speed = Vector3.zero;

	private Node drum;
	public bool inDrum;

	public bool twoDimensional = false;

	public float control;

	// Use this for initialization
	void Awake()
	{
		if (instance == null) instance = this;
	}
	void Start ()
	{
		//original = Input.GetAxis("Vertical");
		//Drum.instance.CleanMat();
		//Guitar.instance.CleanMat();
		original=0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (inDrum && drum != null && Input.GetButtonDown("Fire1"))
		{
			hitDrum();
			ScoreManager.instance.AddScore(1);
		}
		control = Input.GetAxisRaw("Vertical");
		//----------------1 dimensional control speed-----------------------
		//if (!twoDimensional)
		//{
			/*Vector3 espectedSpeed = new Vector3(Input.GetAxisRaw("Vertical") - original - transform.localPosition.y, 0, 0)*sinsitive;
			speed = speed + (espectedSpeed - speed) * Time.deltaTime * 10f;
			transform.localPosition += Vector3.up * Time.deltaTime * speed.x;
			transform.LookAt(transform.position + new Vector3(-3, -speed.x, 0));*/
		//}


		//-----------------------end 1 dimensional control speed------------------------------------
		/*else
		{
			//--------------2 dimensional control speed----------------------------
			//print("2dimen");
			Vector3 espectedSpeed=new Vector3(Input.GetAxisRaw("Vertical")-original-transform.localPosition.y,Input.GetAxisRaw("Horizontal")-transform.localPosition.z,0)*sinsitive;
			speed = speed + (espectedSpeed - speed) * Time.deltaTime * 10f;
			//if (speed.magnitude > 7) speed = speed * (7 / speed.magnitude);
			transform.localPosition += Vector3.up * Time.deltaTime * speed.x + Vector3.forward*Time.deltaTime*speed.y;
			transform.LookAt(transform.position+new Vector3(-3,speed.x,-speed.y));

			//-----------------end 2 dimensional control speed-------------------------------------------

		}*/

		//--------------------control position-----------------------------------
		Vector3  expectedPosition = Vector3.up * (Input.GetAxisRaw("Vertical") - original)*1.05f;
		transform.localPosition += (expectedPosition - transform.localPosition)*Time.deltaTime*sinsitive;
		//-----------------end control position-------------------------------------------
		history.positionCount++;
		history.SetPosition(history.positionCount-1,transform.position);
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Drum"))
		{
			inDrum = true;
			drum = other.gameObject.GetComponent<Node>();
			//SoundController.instance.PlaySound(other.name);
			//Destroy(other.gameObject);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag.Equals("Drum") && inDrum && drum == other.gameObject.GetComponent<Node>())
		{
			inDrum = false;
			drum = null;
		}
	}

	private void hitDrum()
	{
		drum.Hit();
		Drum.instance.EnPower();
		ScoreManager.instance.AddScore();
		inDrum = false;
		drum = null;
	}


}
