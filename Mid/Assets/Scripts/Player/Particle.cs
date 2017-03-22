using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	Particle: Base class for all particles that are affected by magnetic forces			*/
/*																						*/
/*		Functions:																		*/
/*			private:																	*/
/*				void Start () 															*/
/*																						*/
/*			public:																		*/
/*				void UpdateColor()														*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class Particle : MonoBehaviour 
{

	//	Public Variables
	public float charge = 1.0f;											//	This particle's charge
	public Color nodeCharged;											//	Color of the node (Player)
	public Color nodeDischarged = new Color (0.234f, 0.234f, 0.234f);	//	Color of the node after you reverse polarity
	public Color positiveColor = new Color (1.0f, 1.0f, 0f);			//	Color of positively charged particles
	public Color negativeColor = new Color (0.234f, 0.449f, 0.691f);	//	Color of  negatively charged particles

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Start: Runs once at the begining of the game. Initalizes variables.					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void Start () 
	{
		if (tag.Contains("Player") || tag.Contains("Zone"))
		{
			nodeCharged = gameObject.GetComponent<SpriteRenderer>().color;
		}
		else
		{
			UpdateColor();
		}	
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	UpdateColor: Updtaes color based on charge											*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public void UpdateColor()
	{
		Color color = charge > 0? positiveColor: negativeColor;
		GetComponent<Renderer>().material.color = color;
	}
}
