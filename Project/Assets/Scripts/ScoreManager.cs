
using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	[Serializable] 
	public class Score
	{
		public int score;
		public int red;

		Score()
		{
			score = 0;
			red = 0;
		}
	};

	[SerializeField] public Score [] scores;
	[SerializeField] private int[] MaxScores;

	private int level = 0;

	//public int MaxScore;


	public static ScoreManager instance;
	
	[SerializeField] private Slider scoreSlider;
	[SerializeField] private Slider redSlider;
	[SerializeField] private Text levelText;

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
	}

	public void Miss(int index=0)
	{
		scores[index].red++;
		if(index==0)redSlider.value = (float) scores[0].red / MaxScores[level];
		
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
