using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ChrsUtils.ChrsCamera;
using ChrsUtils.ChrsEventSystem.EventsManager;
using ChrsUtils.ChrsEventSystem.GameEvents;
using IonGameEvents;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	GameManager: Updates Score and resets game											*/
/*																						*/
/*		Functions:																		*/
/*			private:																	*/
/*				void Start ()															*/
/*				void OnParticleEnter(GameEvent ige)										*/
/*				void OnParticleExit(GameEvent ige)										*/
/*				void OnTimeIsOver(GameEvent ige)										*/
/*				void UpdateScore(string tag, float score)								*/
/*				void RestartGame()														*/
/*				void Update () 															*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class GameManager : MonoBehaviour 
{
    private static GameManager _instance;
    public static  GameManager Instance
    {
        get { return _instance; }
        private set { }
    }

	//	Public Const Variable
	public const KeyCode RESTART_GAME = KeyCode.R;					//	The button to restart the game

	//	Private Const Variables
	private const string MAIN_SCENE = "Main";						//	Name of the main scene
	private const string GAME_TIMER = "Timer";						//	Name of the Timer GameObject
	private const string PLAYER_1_SCORE = "Player1Score";			//	Name of the Text UI for player 1
	private const string PLAYER_2_SCORE = "Player2Score";			//	Name of the Text UI for player 2
    private const string SPAWN_POINT = "SpawnPoint";

    public Transform spawnPoint;

	//	Private Variables
	private Timer gameTimer;										//	Reference to the game timer
	private Text player1Score;										//	Reference to player 1's score
	private Text player2Score;										//	Reference to player 2's score
	private ParticleEnteredZoneEvent.Handler onParticleEnter;		//	Handler for OnParticleEnteredZoneEvent
	private ParticleExitedZoneEvent.Handler onParticleExit;			//	Handler for OnParticleExitedZoneEvent
	private TimeIsOverEvent.Handler onTimeIsOver;					//	Handler for TimeIsOverEvent

    public int numberOfPlayers;
	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Start: Runs once at the begining of the game. Initalizes variables.					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void Start () 
	{
        numberOfPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
        if (_instance == null)
        {
            _instance = this;
        }

		gameTimer = GameObject.Find(GAME_TIMER).GetComponent<Timer>();

		player1Score = GameObject.Find(PLAYER_1_SCORE).GetComponent<Text>();
		player2Score = GameObject.Find(PLAYER_2_SCORE).GetComponent<Text>();

        spawnPoint = GameObject.Find(SPAWN_POINT).transform;

		//	Sets up the handlers
		onParticleEnter = new ParticleEnteredZoneEvent.Handler(OnParticleEnter);
		onParticleExit = new ParticleEnteredZoneEvent.Handler(OnParticleExit);
		onTimeIsOver = new TimeIsOverEvent.Handler(OnTimeIsOver);

        //	Registers for events
        Services.Events.Register<ParticleEnteredZoneEvent>(onParticleEnter);
        Services.Events.Register<ParticleExitedZoneEvent>(onParticleExit);
        Services.Events.Register<TimeIsOverEvent>(onTimeIsOver);

		//	Adds time to Timer
		gameTimer.AddDurationInSeconds(61);

        //	Fires StartTimerEvent
        Services.Events.Fire(new StartTimerEvent());
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	OnParticleEnter: Handler for OnParticleEnter Event									*/
	/*			param:																		*/
	/*				GameEvent ige - access to readonly variables in event					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void OnParticleEnter(GameEvent ige)
	{
		//	Decrements score
		((ParticleEnteredZoneEvent)ige).zone.score--;
		UpdateScore(((ParticleEnteredZoneEvent)ige).zone.tag, ((ParticleEnteredZoneEvent)ige).zone.score);
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	OnParticleExit: Handler for OnParticleExit Event									*/
	/*			param:																		*/
	/*				GameEvent ige - access to readonly variables in event					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void OnParticleExit(GameEvent ige)
	{
		//	Increments score
		((ParticleExitedZoneEvent)ige).zone.score++;
		UpdateScore(((ParticleExitedZoneEvent)ige).zone.tag, ((ParticleExitedZoneEvent)ige).zone.score);
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	OnTimeIsOver: Handler for OnTimeIsOver Event										*/
	/*			param:																		*/
	/*				GameEvent ige - access to readonly variables in event					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/	
	void OnTimeIsOver(GameEvent ige)
	{
		//	Pauses the game
		Time.timeScale = 0.0f;
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	UpdateScore: Updates the game's UI													*/
	/*			param:																		*/
	/*				string tag - determines which score should be updated					*/
	/*				float score - the new score												*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
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
	
	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	RestartGame: Handles clean up for error free transitions							*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void RestartGame()
	{
        Services.Events.Unregister<ParticleEnteredZoneEvent>(onParticleEnter);
        Services.Events.Unregister<ParticleExitedZoneEvent>(onParticleExit);
		//SceneManager.LoadScene(MAIN_SCENE, LoadSceneMode.Single);
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Update: Called once per frame														*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void Update () 
	{
		if(Input.GetKeyDown(RESTART_GAME))
		{	
			RestartGame();
		}

        numberOfPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
        if (numberOfPlayers < 2)
        {
            GameObject player = ObjectPool.GetFromPool(Poolable.types.PLAYER);
        }
	}
}
