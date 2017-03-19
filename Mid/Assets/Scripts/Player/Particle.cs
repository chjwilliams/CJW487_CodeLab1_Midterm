﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour 
{

	public float charge = 1.0f;
	public Color nodeCharged = Color.white;
	public Color nodeDischarged = new Color (0.234f, 0.234f, 0.234f);
	public Color positiveColor = new Color (1.0f, 1.0f, 0f);
	public Color negativeColor = new Color (0.234f, 0.449f, 0.691f);
	// Use this for initialization
	void Start () 
	{
		if (!CompareTag("Player"))
		{
			UpdateColor();
		}	
	}

	public void UpdateColor()
	{
		Color color = charge > 0? positiveColor: negativeColor;
		GetComponent<Renderer>().material.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}