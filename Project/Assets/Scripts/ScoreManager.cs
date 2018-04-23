
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

	private int level = 0;

	//public int MaxScore;


	public static ScoreManager instance;
	
	[SerializeField] private Slider scoreSlider;
	[SerializeField] private Slider redSlider;
	[SerializeField] private Text levelText;
	[SerializeField] private Text fail;

	void Awake()
	{
		if (instance == null) instance = this;
		scoreSlider.value = 0;
		redSlider.value = 0;
		scores = new Score[3];
	}
	// Use this for initialization
	void Start ()
	{
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddScore(int index=0)
	{
		scores[index].score++;
		//TotalScore++;
		if(index==0) scoreSlider.value = (float) scores[0].score / MaxScores[level];
		if(scores[0].red+scores[0].score>=MaxScores[level]) Advance();
	}

	public void Miss(int index=0)
	{
		scores[index].red++;
		if(index==0)redSlider.value = (float) scores[0].red / MaxScores[level];
		if (redSlider.value > 0.2f)
		{
			fail.enabled = true;
			Time.timeScale = 0.1f;
		}
		else
		{
			if(scores[0].red+scores[0].score>=MaxScores[level]) Advance();
		}
		
	}

	public void Advance()
	{
		level++;
		levelText.text = "level " + level.ToString();
		scoreSlider.value = 0;
		redSlider.value = 0;
		scores[0].score = 0;
		scores[0].red = 0;
	}
	
	
}
