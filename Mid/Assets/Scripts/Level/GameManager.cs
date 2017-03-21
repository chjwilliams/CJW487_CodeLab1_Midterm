using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChrsUtils.ChrsCamera;
using ChrsUtils.ChrsEventSystem.EventsManager;
using ChrsUtils.ChrsEventSystem.GameEvents;
using IonGameEvents;

public class GameManager : MonoBehaviour 
{

	private static GameManager instance;
	public GameManager Insatnce
	{
		get 
		{
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				Destroy(gameObject);
			}

			return instance;
		}
		private set {}
	}

	private const string GAME_TIMER = "Timer";
	private const string PLAYER_1_SCORE = "Player1Score";
	private const string PLAYER_2_SCORE = "Player2Score";
	private Timer gameTimer;
	private Text player1Score;
	private Text player2Score;
	private ParticleEnteredZoneEvent.Handler onParticleEnter;
	private ParticleExitedZoneEvent.Handler onParticleExit;


	// Use this for initialization
	void Start () 
	{
		gameTimer = GameObject.Find(GAME_TIMER).GetComponent<Timer>();

		player1Score = GameObject.Find(PLAYER_1_SCORE).GetComponent<Text>();
		player2Score = GameObject.Find(PLAYER_2_SCORE).GetComponent<Text>();

		onParticleEnter = new ParticleEnteredZoneEvent.Handler(OnParticleEnter);
		onParticleExit = new ParticleEnteredZoneEvent.Handler(OnParticleExit);
		
		GameEventsManager.Instance.Register<ParticleEnteredZoneEvent>(onParticleEnter);
		GameEventsManager.Instance.Register<ParticleExitedZoneEvent>(onParticleExit);

		gameTimer.AddDurationInSeconds(61);


		GameEventsManager.Instance.Fire(new StartTimerEvent());
	}

	void OnParticleEnter(GameEvent ige)
	{
		((ParticleEnteredZoneEvent)ige).zone.score--;
		UpdateScore(((ParticleEnteredZoneEvent)ige).zone.tag, ((ParticleEnteredZoneEvent)ige).zone.score);
	}

	void OnParticleExit(GameEvent ige)
	{
		((ParticleExitedZoneEvent)ige).zone.score++;
		UpdateScore(((ParticleExitedZoneEvent)ige).zone.tag, ((ParticleExitedZoneEvent)ige).zone.score);
	}

	void UpdateScore(string tag, float score)
	{
		if(tag.Contains("1"))
		{
			player1Score.text = score.ToString();
		}
		else if (tag.Contains("2"))
		{
			player2Score.text = score.ToString();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
