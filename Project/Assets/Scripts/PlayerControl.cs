﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour
{

	public static PlayerControl instance;
	private float original;
	[SerializeField] private LineRenderer history;

	[SerializeField] private float sinsitive;

	[SerializeField] private Material mat;

	private Vector3 speed = Vector3.zero;

	private Node drum;
	private bool inDrum;

	//public bool twoDimensional = false;

	private bool inGame = false;

	public bool controllerMode = true;
	[SerializeField] private Text controlModeDisplay;
	[SerializeField] private Text controlInstruction;
	[SerializeField] private GameObject startButton; 
	private float lineHeight;
	private float pixelMultiplier;

	private float control;

	private bool controlable = false;

	public bool living = true;

	public List<Vector3> historyRecords;
	private enum Trinary
	{
		UNSET,ZERO,ONE
	};

	private Trinary leftTriggerLast = Trinary.UNSET, rightTriggerLast = Trinary.UNSET, leftTrigger = Trinary.UNSET, rightTrigger = Trinary.UNSET;
	
	//------------test0--------------------
	//public float lefttrigger, righttrigger;

	// Use this for initialization
	void Awake()
	{
		if (instance == null) instance = this;
		lineHeight = Camera.main.WorldToScreenPoint(transform.position).y;
		pixelMultiplier = 1/(Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.05f).y - lineHeight);
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

	public void SwitchMode()
	{
		controllerMode = !controllerMode;
		controlModeDisplay.text = controllerMode
			? "XBox Controller"
			: "Keyboard & Mouse";
		controlInstruction.text = controllerMode
			? "Press down both Left and Right Triggers to Start.\nDuring the game, you can use it to come back to menu."
			: "During the game, to come back to menu,\npress down both Escape and Enter on the keyboard.";

		if (!controllerMode)
		{
			lineHeight = Camera.main.WorldToScreenPoint(transform.position).y;
			pixelMultiplier = 1 / (Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.05f).y - lineHeight);
			startButton.SetActive(true);
		}
		else
		{
			startButton.SetActive(false);
		}


		// Clear Triggers
		leftTrigger = Trinary.UNSET;
		leftTriggerLast = Trinary.UNSET;
		rightTrigger = Trinary.UNSET;
		rightTriggerLast = Trinary.UNSET;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// TEST
		//if (Input.GetMouseButtonDown(0))
		//{
		//	print(Input.mousePosition.y);
		//}
		// TEST END
		
		leftTrigger = 
			(controllerMode?(Mathf.Abs(Input.GetAxis("LeftTrigger"))>0.9f):(Input.GetKey(KeyCode.Escape)) )
				?Trinary.ONE : Trinary.ZERO;
		rightTrigger = 
			(controllerMode?(Mathf.Abs(Input.GetAxis("RightTrigger")) > 0.9f):(Input.GetKey(KeyCode.Return)) )
			?Trinary.ONE : Trinary.ZERO;
		if (controlable)
		{
			if ((leftTriggerLast.Equals(Trinary.ZERO) || rightTriggerLast.Equals(Trinary.ZERO)) &&
			    leftTrigger.Equals(Trinary.ONE) && rightTrigger.Equals(Trinary.ONE))
			{
				if (inGame)
				{
					Restart();
				}
				else if (Menu.instance != null && controllerMode)
				{
					print("start game");
					Menu.instance.StartGame();
				}
			}
		}
		leftTriggerLast = leftTrigger;
		rightTriggerLast = rightTrigger;
		if (living)
		{
			if (controlable)
			{
				if (inDrum && drum != null && (controllerMode?Input.GetButtonDown("Fire1") : Input.GetKeyDown(KeyCode.Space)))
				{
					hitDrum();
					ScoreManager.instance.AddScore(1);
				}
				control = controllerMode?Input.GetAxisRaw("Vertical"):(Input.mousePosition.y-lineHeight)*pixelMultiplier;
				control = Math.Min(1f,Math.Max(-1f,control));
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
				Vector3 expectedPosition = Vector3.up * (control - original) * 1.05f;
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

	public void EnableControl()
	{
		controlable = true;
	}

	public void DisableControl()
	{
		controlable = false;
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
		missle.SetAttack(3700);
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
