using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

	[SerializeField] private float stableX;
	[SerializeField] private GameObject missle;
	[SerializeField] private Transform missleParent;

	private int state=0;

	private Coroutine cor;
	// 0: not coming out
	// 1: coming out
	// 2: stop and shooting missles
	// 3: stop shooting and explode
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (state ==1)
		{
			transform.localPosition += new Vector3((stableX - transform.localPosition.x)*Time.deltaTime, 0, 0);
			if (Mathf.Abs(transform.localPosition.x - stableX) < 0.1f)
			{
				state = 2;
				cor = StartCoroutine("shootMissles");
			}
		}
		
	}

	public void ComeOut()
	{
		state = 1;
	}

	public void Explode()
	{
		state = 3;
		StopCoroutine(cor);
	}

	IEnumerator shootMissles()
	{
		yield return null;
		while (true)
		{
			GameObject newMissle = Instantiate(missle, missleParent);
			newMissle.transform.position = transform.position;
			newMissle.tag = tag;
			newMissle.GetComponent<Missle>().SetTarget(PlayerControl.instance.transform);
			yield return new WaitForSecondsRealtime(0.3f);
		}
	}
}
