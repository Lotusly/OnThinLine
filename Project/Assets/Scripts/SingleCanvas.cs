using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleCanvas : MonoBehaviour
{

	public static SingleCanvas instance;
	[SerializeField] private Image final;
	[SerializeField] private Image background;
	[SerializeField] private Text[] instructions;

	private void Awake()
	{
		if (instance == null) instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowInstruction(int i)
	{
		StartCoroutine(showInstruction(i));
	}

	private IEnumerator showInstruction(int i)
	{
		instructions[i].enabled = true;
		yield return new WaitForSeconds(6);
		instructions[i].enabled = false;
	}

	public void FadeOut()
	{
		StartCoroutine(fadeOut());
	}

	private IEnumerator fadeOut()
	{
		yield return null;
		while (background.color.a < 1)
		{
			background.color=new Color(0,0,0,Mathf.Min(background.color.a+Time.deltaTime,1));
			yield return new WaitForEndOfFrame();
		}
		while (final.color.a < 1)
		{
			background.color=new Color(0,0,0,Mathf.Min(final.color.a+Time.deltaTime,1));
			yield return new WaitForEndOfFrame();
		}
		Camera.main.transform.parent = null;
		Destroy(PlayerControl.instance.transform.root.gameObject);
	}

	

	
}
