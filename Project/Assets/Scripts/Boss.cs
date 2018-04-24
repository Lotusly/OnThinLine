using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

	public static Boss instance;
	[SerializeField] private float stableX;
	[SerializeField] private GameObject missle;
	[SerializeField] private Transform missleParent;
	private bool comingOut = false;

	private int state=0;

	private Coroutine cor;
	// 0: not coming out
	// 1: coming out
	// 2: stop 
	// 3: shooting missles
	// 4: stop shooting and explode
	private void Awake()
	{
		if (instance == null) instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (comingOut)
		{
			transform.localPosition += new Vector3((stableX - transform.localPosition.x)*Time.deltaTime, 0, 0);
			if (Mathf.Abs(transform.localPosition.x - stableX) < 0.1f)
			{
				comingOut = false;

				//cor = StartCoroutine("shootMissles");
			}
		}
		
	}

	public void EnterBattle()
	{
		state = 3;
		cor = StartCoroutine("shootMissles");
	}

	public void ComeOut()
	{
		state = 1;
		comingOut = true;
	}

	public void Explode()
	{
		state = 4;
		StopCoroutine(cor);
		GetComponent<Floating>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = false;
		ScoreManager.instance.Advance();
		StartCoroutine("delayDestroy");
	}

	IEnumerator delayDestroy()
	{
		yield return new WaitForSeconds(5);
		Destroy(gameObject);
	}

	IEnumerator shootMissles()
	{
		yield return null;
		while (true)
		{
			GameObject newMissle = Instantiate(missle, missleParent);
			newMissle.transform.position = transform.position;
			newMissle.tag = tag;
			switch (Mathf.FloorToInt(Random.value * 3))
			{
				case 0:
				{
					if (Drum.instance.living && Guitar.instance.living)
					{
						newMissle.GetComponent<Missle>().SetTarget(Drum.instance.transform);
					}
					else newMissle.GetComponent<Missle>().SetTarget(PlayerControl.instance.transform);
					break;
				}
				case 1:
				{
					if (Drum.instance.living)
					{
						newMissle.GetComponent<Missle>().SetTarget(Drum.instance.transform);
					}
					else if (Guitar.instance.living)
					{
						newMissle.GetComponent<Missle>().SetTarget(Guitar.instance.transform);
					}
					else
					{
						newMissle.GetComponent<Missle>().SetTarget(PlayerControl.instance.transform);

					}
					break;
				}
				case 2:
				{
					if (Guitar.instance.living)
					{
						newMissle.GetComponent<Missle>().SetTarget(Guitar.instance.transform);
					}
					else if (Drum.instance.living)
					{
						newMissle.GetComponent<Missle>().SetTarget(Drum.instance.transform);
					}
					else
					{
						newMissle.GetComponent<Missle>().SetTarget(PlayerControl.instance.transform);
					}
					break;
				}
			}
			newMissle.GetComponent<Missle>().SetInUse();

			
			yield return new WaitForSecondsRealtime((float)5/6);
		}
	}
}
