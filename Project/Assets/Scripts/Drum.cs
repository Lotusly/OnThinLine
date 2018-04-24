﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Drum : MonoBehaviour
{
	public static Drum instance;

	[SerializeField] private LineRenderer history;
	[SerializeField] private float unitPower;
	[SerializeField] private Material ballMat;
	[SerializeField] private Vector3 newPosition;
	private bool controable = false;
	public bool living = true;
	private Coroutine cor;
	private Coroutine followCor;
	[SerializeField] private GameObject missle;
	[SerializeField] private Transform missleParent;


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
			SoundController.instance.StopMusic(1);
			transform.parent = null;
			StopCoroutine(cor);
		}
	}

	public void FlyAway()
	{
		StartCoroutine("flyAway");
	}

	private IEnumerator flyAway()
	{
		yield return null;
		transform.parent = RootTransfer.instance.transform;
		while (Vector3.Distance(transform.localPosition, newPosition) > 0.1f)
		{
			transform.localPosition += (newPosition - transform.localPosition) * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

	public void CleanMat()
	{
		ballMat.color = new Color(ballMat.color.r, ballMat.color.g, ballMat.color.b, 0);
	}

	public void EnterBattle()
	{
		if(followCor!=null) StopCoroutine(followCor);
		cor = StartCoroutine("inBattle");
	}

	private IEnumerator inBattle()
	{
		yield return new WaitForSecondsRealtime((float)5/6);
		while (true)
		{
			for (int i = 0; i < 3; i++)
			{
				GameObject newMissle = Instantiate(missle, missleParent);
				newMissle.GetComponent<Missle>().enabled = true;
				newMissle.transform.position = transform.position;
				newMissle.tag = tag;
				newMissle.GetComponent<Missle>().SetTarget(Boss.instance.transform);
				newMissle.GetComponent<Missle>().SetMaxSpeed(10);
				newMissle.GetComponent<Missle>().SetInUse();

				yield return new WaitForSecondsRealtime((float)5/12);
			}
			yield return new WaitForSecondsRealtime((float)5/12);
		}
	}

	public void FindGuitarClose()
	{
		if(followCor!=null) StopCoroutine(followCor);

		followCor = StartCoroutine(findGuitarCloseCor());
	}

	private IEnumerator findGuitarCloseCor()
	{
		yield return new WaitForSeconds(5);
		newPosition = new Vector3(Random.Range(-3f,10.7f),Random.Range(-3f,4.2f),0);
		while (Vector3.Distance(newPosition, transform.localPosition) < 5)
		{
			newPosition = new Vector3(Random.Range(-3f,10.7f),Random.Range(-3f,4.2f),0);
		}
		followCor = StartCoroutine(flyAway());
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
