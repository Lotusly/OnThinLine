using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

	[SerializeField] private GameObject[] toBeEnabled;
	[SerializeField] private Vector3 startPosition;
	[SerializeField] private History history;
	

	// Use this for initialization
	void Start ()
	{
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame()
	{
		for (int i = 0; i < toBeEnabled.Length; i++)
		{
			toBeEnabled[i].active = true;
		}
		PlayerControl.instance.EnterGame();
		history.RotateToFaceFront();
		history.MoveTo(startPosition);
		RootTransfer.instance.transform.position = startPosition;
		RootTransfer.instance.transform.LookAt(RootTransfer.instance.transform.position+Vector3.forward);
		RootTransfer.instance.EnterGame();
		Destroy(gameObject);

	}

	
}
