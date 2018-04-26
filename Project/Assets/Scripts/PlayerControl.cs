using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;




public class PlayerControl : MonoBehaviour
{

	public static PlayerControl instance;
	private float original;
	[SerializeField] private LineRenderer history;

	[SerializeField] private float sinsitive;

	[SerializeField] private Material mat;

	private Vector3 speed = Vector3.zero;

	private Node drum;
	public bool inDrum;

	public bool twoDimensional = false;

	private bool inGame = false;

	public float control;

	public bool controlable = true;

	public bool living = true;

	public List<Vector3> historyRecords;
	
	//------------test0--------------------
	//public float lefttrigger, righttrigger;

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
		mat.color = new Color(1, 1, 1, 0.5f);
		historyRecords = new List<Vector3>();
		historyRecords.Add(history.GetPosition(0));
		historyRecords.Add(history.GetPosition(1));
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (living)
		{
			if (inGame)
			{
				//lefttrigger = Input.GetAxis("LeftTrigger");
				//righttrigger = Input.GetAxis("RightTrigger");
				if (Mathf.Abs(Input.GetAxis("LeftTrigger")) > 0.9f && Mathf.Abs(Input.GetAxis("RightTrigger")) > 0.9f)
				{
					Restart();
				}
			}
			if (controlable)
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
				//else
				//{
					//--------------2 dimensional control speed----------------------------
					//print("2dimen");
					/*Vector3 espectedSpeed=new Vector3(Input.GetAxisRaw("Vertical")-original-transform.localPosition.y,Input.GetAxisRaw("Horizontal")-transform.localPosition.z,0)*sinsitive;
					speed = speed + (espectedSpeed - speed) * Time.deltaTime * 10f;
					//if (speed.magnitude > 7) speed = speed * (7 / speed.magnitude);
					transform.localPosition += Vector3.up * Time.deltaTime * speed.x + Vector3.forward*Time.deltaTime*speed.y;
					transform.LookAt(transform.position+new Vector3(-3,speed.x,-speed.y));*/
		
					//-----------------end 2 dimensional control speed-------------------------------------------
		
				//}

				//--------------------control position-----------------------------------
				Vector3 expectedPosition = Vector3.up * (Input.GetAxisRaw("Vertical") - original) * 1.05f;
				transform.localPosition += (expectedPosition - transform.localPosition) * Time.deltaTime * sinsitive;
				//-----------------end control position-------------------------------------------
			}
			history.positionCount++;
			history.SetPosition(history.positionCount - 1, transform.position);
			historyRecords.Add(transform.position);
			if (history.positionCount > 1000)
			{
				historyRecords.RemoveRange(0,300);
				history.positionCount -=300;
				history.SetPositions(historyRecords.ToArray());
			}
		}

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

	public void MustKill()
	{
		StartCoroutine(mustKill());
	}

	public void Restart()
	{
		SingleCanvas.instance.FadeOut(false);
	}

	private IEnumerator mustKill()
	{
		//print("enter mastkill");
		controlable = false;
		while (mat.color.b>(float)10/255)
		{
			mat.color=new Color(mat.color.b-2*Time.deltaTime,1,mat.color.b-2*Time.deltaTime,mat.color.a-Time.deltaTime);
			transform.localScale+=new Vector3(1f,1f,1f)*Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		
		Missle missle = gameObject.AddComponent<Missle>();
		missle.SetMaxSpeed(10);
		missle.SetTarget(Boss.instance.transform);
		missle.SetAttack(3800);
		missle.SetInUse();
	}

	public void Die()
	{
		living = false;
		GetComponent<MeshRenderer>().enabled = false;
		GetComponent<TrailRenderer>().enabled = false;
	}

	public void EnterGame()
	{
		inGame = true;
		StartCoroutine(enterGame());
	}
	private IEnumerator enterGame()
	{
		GetComponent<TrailRenderer>().enabled = false;
		yield return new WaitForSecondsRealtime(0.5f);
		GetComponent<TrailRenderer>().enabled = true;
	}


}
