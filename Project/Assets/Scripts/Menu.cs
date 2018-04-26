using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

	[SerializeField] private GameObject[] toBeEnabled;
	[SerializeField] private Vector3 startPosition;
	[SerializeField] private History history;
	[SerializeField] private Music menuMusic;
	public static Menu instance;


	void Awake()
	{
		if (instance == null) instance = this;
	}
	// Use this for initialization
	void Start ()
	{
		StartCoroutine(delayControl());
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
		menuMusic.FadeOut();
		Destroy(gameObject);

	}

	public void QuitGame()
	{
		Application.Quit();
	}

	private IEnumerator delayControl()
	{
		yield return new WaitForSecondsRealtime(0.5f);
		PlayerControl.instance.EnableControl();
	}

	
}
