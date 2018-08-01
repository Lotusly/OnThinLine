using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SingleCanvas : MonoBehaviour
{

	public static SingleCanvas instance;
	[SerializeField] private Image final;
	[SerializeField] private Image background;
	[SerializeField] private Text[] instructionsControllerMode;
	[SerializeField] private Text[] instructionsMouseMode;
	private Coroutine cor;

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
		if (PlayerControl.instance.controllerMode)
		{
			instructionsControllerMode[i].enabled = true;
			yield return new WaitForSeconds(6);
			instructionsControllerMode[i].enabled = false;
		}
		else
		{
			instructionsMouseMode[i].enabled = true;
			yield return new WaitForSeconds(6);
			instructionsMouseMode[i].enabled = false;
		}
	}

	public void FadeOut(bool withCG)
	{
		if(cor==null)
			cor=StartCoroutine(fadeOut(withCG));
	}

	private IEnumerator fadeOut(bool withCG)
	{
		print("fade out with: "+(withCG?"true":"false"));
		yield return null;
		while (background.color.a < 1)
		{
			background.color=new Color(0,0,0,Mathf.Min(background.color.a+Time.deltaTime,1));
			yield return new WaitForEndOfFrame();
		}
		
		if (withCG)
		{
			ShowCG();
		}
		else
		{
			Time.timeScale = 1;
			SceneManager.LoadScene("Main");
		}
		cor = null;
		//print("1");

		//print("3");
	}


	public void ShowCG()
	{
		
		//if(cor==null)
			cor=StartCoroutine(showCG());
	}

	private IEnumerator showCG()
	{
		yield return null;
		while (final.color.a < 1)
		{
			final.color=new Color(1,1,1,Mathf.Min(final.color.a+Time.deltaTime,1));
			yield return new WaitForEndOfFrame();
		}
		//print("2");
		Camera.main.transform.parent = null;
		SoundController.instance.transform.parent = null;
		PlayerControl.instance.transform.parent = null;
		Destroy(RootTransfer.instance.gameObject);
		PlayerControl.instance.EnableControl();
		yield return new WaitForSeconds(10);
		if(PlayerControl.instance.controllerMode)
			instructionsControllerMode[2].enabled = true;
		else
			instructionsMouseMode[2].enabled = true;
	}

	

	
}
