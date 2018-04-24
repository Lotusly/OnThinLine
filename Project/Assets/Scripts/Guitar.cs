using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Guitar : MonoBehaviour
{
	public static Guitar instance;

	[SerializeField] private LineRenderer history;
	[SerializeField] private float unitPower;
	[SerializeField] private Material ballMat;
	[SerializeField] private Vector3 newPosition;
	public bool living = true;
	private Coroutine cor;
	private Coroutine followCor;
	[SerializeField] private GameObject missle;
	[SerializeField] private Transform missleParent;

	private bool followDrum = false;

	// Use this for initialization
	void Awake()
	{
		if (instance == null) instance = this;
	}
	void Start ()
	{
		CleanMat();
		//original=0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		history.positionCount++;
		history.SetPosition(history.positionCount-1,transform.position);

		if (followDrum && Vector3.Distance(transform.position, Drum.instance.transform.position)>2)
		{
			followDrum = false;
			flyToDrum();
		}
	
	}

	public void EnPower()
	{
		if(ballMat.color.a<0.9f)ballMat.color+=new Color(0,0,0,unitPower/255);
	}

	public void DePower()
	{
		if (ballMat.color.a > 0.1f) ballMat.color -= new Color(0, 0, 0, unitPower*8 / 255);
		else
		{
			living = false;
			LooseBound();
			GetComponent<Floating>().enabled = false;
			SoundController.instance.StopMusic(2);
			
			StopCoroutine(cor);
			transform.parent = null;
			ballMat.color = Color.clear;

		}
		
	}

	public void LooseBound()
	{
		if(followCor!=null) StopCoroutine(followCor);
		followDrum = false;
	}

	public void FlyAway()
	{
		cor=StartCoroutine("flyAway");
	}

	private IEnumerator flyAway()
	{
		print("flyAway");
		yield return null;
		transform.parent = RootTransfer.instance.transform;
		while (Vector3.Distance(transform.localPosition, newPosition) > 0.1f)
		{
			transform.localPosition += (newPosition - transform.localPosition) * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		GetComponent<Floating>().enabled = true;
		yield return new WaitForSeconds(2);
		if (!ScoreManager.instance.InBossBattle)
		{
			print("not in battle");
			followDrum = true;
		}
		else
		{
			print("battle");
			Drum.instance.FindGuitarInDanger();

		}

	}
	
	public void CleanMat()
	{
		ballMat.color = new Color(1, 1, 0, 0);
	}
	
	public void EnterBattle()
	{
		cor = StartCoroutine("inBattle");
		if(followCor!=null) StopCoroutine(followCor);
		followDrum = false;
		newPosition = new Vector3(3,1.5f,0);
		StartCoroutine(flyAway());
		GetComponent<Floating>().enabled = false;
	}

	private IEnumerator inBattle()
	{
		yield return new WaitForSecondsRealtime((float)5/6);
		while (true)
		{
			for (int i = 0; i < 7; i++)
			{
				GameObject newMissle = Instantiate(missle, missleParent);
				newMissle.GetComponent<Missle>().enabled = true;
				newMissle.transform.position = transform.position;
				newMissle.tag = tag;
				newMissle.GetComponent<Missle>().SetTarget(Boss.instance.transform);
				newMissle.GetComponent<Missle>().SetMaxSpeed(10);
				newMissle.GetComponent<Missle>().SetInUse();
				yield return new WaitForSecondsRealtime((float)5/24);
			}
			yield return new WaitForSecondsRealtime((float)5/24);
		}
		
	}

	private void flyToDrum()
	{
		//newPosition = Drum.instance.transform.localPosition;
		followCor=StartCoroutine(flyToDrumCor());
	}

	private IEnumerator flyToDrumCor()
	{
		print("flyToDrum");
		GetComponent<Floating>().enabled = false;
		yield return null;
		float speed = 0;
		Vector3 direction;
		while (Vector3.Distance(transform.position, Drum.instance.transform.position) > 1)
		{
			speed = Mathf.Min(10, speed + Time.deltaTime * 10);
			direction = (Drum.instance.transform.position - transform.position);
			transform.localPosition += Mathf.Min(speed,direction.magnitude*3)*direction.normalized * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		GetComponent<Floating>().enabled = true;
		GetComponent<Floating>().SetGoodOrigin();
		followDrum = true;
		Drum.instance.FindGuitarClose();
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
