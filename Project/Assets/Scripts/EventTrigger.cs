using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
	[SerializeField] private string t;

	[SerializeField] private UnityEvent e;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals(t))
		{
			e.Invoke();
			Destroy(gameObject);
		}
	}
}
