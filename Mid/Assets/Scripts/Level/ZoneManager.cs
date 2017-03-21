using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChrsUtils.ChrsEventSystem.EventsManager;
using IonGameEvents;

public class ZoneManager : MonoBehaviour 
{

	private const string PLAYER = "Player";
	private const string SCORE = "Score";
	private const string PARTICLE = "MovingParticle";
	public int score;

	// Use this for initialization
	void Start () 
	{
		score  = 0;
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		/*
			When object with Particle tag enters fire an event to decrement score
		*/
		if (other.tag.Contains(PARTICLE))
		{
			GameEventsManager.Instance.Fire(new ParticleEnteredZoneEvent(this));
		}
	}


	void OnTriggerExit2D(Collider2D other)
	{
		/*
			When object with Particle tag exits fire an event to increment score 
		*/
		if (other.tag.Contains(PARTICLE))
		{
			GameEventsManager.Instance.Fire(new ParticleExitedZoneEvent(this));
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
