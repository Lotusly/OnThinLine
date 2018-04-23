using System;
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

	// Use this for initialization
	void Awake()
	{
		if (instance == null) instance = this;
	}
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

	public void EnPower()
	{
		if(ballMat.color.a<200)ballMat.color+=new Color(0,0,0,unitPower/255);
		
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

	/*void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Node"))
		{
			SoundController.instance.PlaySound(other.name);
			Destroy(other.gameObject);
		}
	}*/
}
