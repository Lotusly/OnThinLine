
using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	[Serializable] 
	public struct Score
	{
		public int score;
		public int red;

		
	};

	[SerializeField] private Score [] scores;
	[SerializeField] private int[] MaxScores;
	[SerializeField] private int bossLifeMax;
	[SerializeField] private int heroLifeMax;
	public int bossLife;
	public int heroLife;

	private int level = 0;
	public bool InBossBattle=false;

	//public int MaxScore;


	public static ScoreManager instance;
	
	[SerializeField] private Slider scoreSlider;
	[SerializeField] private Slider redSlider;
	[SerializeField] private Slider bossSlider;
	[SerializeField] private Slider heroSlider;
	[SerializeField] private Text levelText;
	[SerializeField] private Text heroText;
	[SerializeField] private Text fail;

	void Awake()
	{
		if (instance == null) instance = this;
		
	}
	// Use this for initialization
	void Start ()
	{
		bossLife = bossLifeMax;
		heroLife = heroLifeMax;
		scoreSlider.value = 0;
		redSlider.value = 0;
		scores = new Score[6];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddScore(int index=0)
	{
		if (level >= 5) return;
		scores[index].score++;
		//TotalScore++;
		if(index==0) scoreSlider.value = (float) scores[0].score / MaxScores[level];
		if(scores[0].red+scores[0].score>=MaxScores[level]) Advance();
	}

	public void Miss(int index=0)
	{
		if (level >= 5) return;
		scores[index].red++;
		if(index==0)redSlider.value = (float) scores[0].red / MaxScores[level];
		if (redSlider.value > 0.25f)
		{
			
			lose();	
		}
		else
		{
			if(scores[0].red+scores[0].score>=MaxScores[level]) Advance();
		}
		
	}

	public void EnterBattle()
	{
		InBossBattle = true;
	}
	

	public void Advance()
	{
		
		level++;
		if (level < 5)
		{
			scoreSlider.value = 0;
			redSlider.value = 0;
			scores[0].score = 0;
			scores[0].red = 0;
			levelText.text = "Level " + level.ToString()+".";
		}
		if (level == 5)
		{
			//InBossBattle = true;
			levelText.text = "Boss";
			heroText.enabled = true;
			scoreSlider.gameObject.active = false;
			redSlider.gameObject.active = false;
			heroSlider.gameObject.active = true;
			heroSlider.value = 1;
			bossSlider.gameObject.active = true;
			bossSlider.value = 1;
			Boss.instance.ComeOut();
		}
		if (level > 5)
		{
			return;
			/*InBossBattle = false;
			levelText.text = "Level Max";
			heroText.enabled = false;
			heroSlider.gameObject.active = false;
			bossSlider.gameObject.active = false;
			scoreSlider.gameObject.active = true;
			scoreSlider.value = 1;*/

		}

	}

	public void ClearLife()
	{
		heroLife = 0;
		heroSlider.value = (float)heroLife / heroLifeMax;
	}

	public void GetHit(string t,int i)
	{
		switch (t)
		{
			case "Player":
			{
				//print(t);
				if (heroLife > 0)
				{
					heroLife -= i;
					if (heroLife <= 0)
					{
						heroSlider.value = 0;
						heroSlider.value = (float) heroLife / heroLifeMax;
						
						lose();
						
						//SingleCanvas.instance.FadeOut();
					}
					else heroSlider.value = (float) heroLife / heroLifeMax;
					return;
				}
				break;
			}
			case "Boss":
			{
				//print(t);
				if (bossLife > 0)
				{
					bossLife -= i;
					if (bossLife <= 0)
					{
						bossSlider.value = 0;
						bossSlider.value = (float) bossLife / bossLifeMax;
						Boss.instance.Explode();
						//Advance();
					}
					else bossSlider.value = (float) bossLife / bossLifeMax;
					return;
				}
				break;
			}
			case "Drum":
			{
				//print(t);
				Drum.instance.DePower();
				return;
				break;
			}
			case "Guitar":
			{
				//print(t);
				Guitar.instance.DePower();
				return;
				break;
			}
			
		}
		print("no matching: "+t);
		
		
	}

	public void lose()
	{
		//Drum.instance.CleanMat();
		//Guitar.instance.CleanMat();
		PlayerControl.instance.DisableControl();
		PlayerControl.instance.living = false;
		fail.enabled = true;
		Time.timeScale = 0.25f;
		PlayerControl.instance.Restart();
	}
	
	
}
