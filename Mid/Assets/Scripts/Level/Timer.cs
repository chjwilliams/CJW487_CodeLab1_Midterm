using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChrsUtils.ChrsEventSystem.GameEvents;
using ChrsUtils.ChrsEventSystem.EventsManager;
using IonGameEvents;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	Timer: Updates the Timer and displays it on the Canvas                              */
/*																						*/
/*		Functions:																		*/
/*			private:																	*/
/*			    void Start ()                           								*/
/*			    void OnStartTimerEvent(GameEvent e)										*/
/*				void OnDestroy()														*/
/*																						*/
/*			public:																		*/
/*			    void AddDurationInSeconds(float t)										*/
/*				IEnumerator DecrementTimer()											*/
/*				string FloatToTime (float toConvert, string format)						*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class Timer : MonoBehaviour 
{
    //  Private Variables
	private float duration;                                 //  Current Time
	private Text currentTime;                               //  UI reference to current timer
	private StartTimerEvent.Handler onStartTimerEvent;      //  Handler for OnStartTimerEvent


	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Start: Runs once at the begining of the game. Initalizes variables.					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void Start () 
	{
		currentTime = GetComponent<Text>();
		onStartTimerEvent = new StartTimerEvent.Handler(OnStartTimerEvent);
		GameEventsManager.Instance.Register<StartTimerEvent>(onStartTimerEvent);
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	OnStartTimerEvent: Handler for OnStartTimerEvent Event								*/
	/*			param:																		*/
	/*				GameEvent ige - access to readonly variables in event					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void OnStartTimerEvent(GameEvent ige)
	{
		StartCoroutine(DecrementTimer());
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	OnDestroy: This function runs when this object is destroyed		               		*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
    void OnDestroy()
    {
        GameEventsManager.Instance.Unregister<StartTimerEvent>(OnStartTimerEvent);
        StopAllCoroutines();
    }

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	AddDurationInSeconds: Adds time to the timer		            					*/
	/*			param:																		*/
	/*				float t - The amount of time added to the timer in seconds				*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public void AddDurationInSeconds(float t)
	{
		duration += t;
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	DecrementTimer: Decrements timer			                                   		*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public IEnumerator DecrementTimer()
	{
		while (duration > 0)
		{
			yield return new WaitForSeconds(1);
			duration--;
			currentTime.text = FloatToTime(duration, "#00:00");
		}

		GameEventsManager.Instance.Fire(new TimeIsOverEvent(this));
		yield return new WaitForEndOfFrame();
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	FloatToTime: Converts a float to string formatted as a timer    					*/
	/*			param:																		*/
    /*				float toConvert - float to convert										*/
    /*				string format - the selected format to output							*/
    /*																						*/
    /*			return:																		*/
    /*				string (in the format of your choice)									*/
    /*																						*/
	/*--------------------------------------------------------------------------------------*/
	 public string FloatToTime (float toConvert, string format)
	 {
         switch (format)
		 {
             case "00.0":
                 return string.Format("{0:00}:{1:0}", 
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*10) % 10));//miliseconds
             break;
             case "#0.0":
                 return string.Format("{0:#0}:{1:0}", 
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*10) % 10));//miliseconds
             break;
             case "00.00":
                 return string.Format("{0:00}:{1:00}", 
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*100) % 100));//miliseconds
             break;
             case "00.000":
                 return string.Format("{0:00}:{1:000}", 
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*1000) % 1000));//miliseconds
             break;
             case "#00.000":
                 return string.Format("{0:#00}:{1:000}", 
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*1000) % 1000));//miliseconds
             break;
             case "#0:00":
                 return string.Format("{0:#0}:{1:00}",
                     Mathf.Floor(toConvert / 60),//minutes
                     Mathf.Floor(toConvert) % 60);//seconds
             break;
             case "#00:00":
                 return string.Format("{0:#00}:{1:00}", 
                     Mathf.Floor(toConvert / 60),//minutes
                     Mathf.Floor(toConvert) % 60);//seconds
             break;
             case "0:00.0":
                 return string.Format("{0:0}:{1:00}.{2:0}",
                     Mathf.Floor(toConvert / 60),//minutes
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*10) % 10));//miliseconds
             break;
             case "#0:00.0":
                 return string.Format("{0:#0}:{1:00}.{2:0}",
                     Mathf.Floor(toConvert / 60),//minutes
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*10) % 10));//miliseconds
             break;
             case "0:00.00":
                 return string.Format("{0:0}:{1:00}.{2:00}",
                     Mathf.Floor(toConvert / 60),//minutes
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*100) % 100));//miliseconds
             break;
             case "#0:00.00":
                 return string.Format("{0:#0}:{1:00}.{2:00}",
                     Mathf.Floor(toConvert / 60),//minutes
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*100) % 100));//miliseconds
             break;
             case "0:00.000":
                 return string.Format("{0:0}:{1:00}.{2:000}",
                     Mathf.Floor(toConvert / 60),//minutes
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*1000) % 1000));//miliseconds
             break;
             case "#0:00.000":
                 return string.Format("{0:#0}:{1:00}.{2:000}",
                     Mathf.Floor(toConvert / 60),//minutes
                     Mathf.Floor(toConvert) % 60,//seconds
                     Mathf.Floor((toConvert*1000) % 1000));//miliseconds
             break;
         }
         return "error";
     }
}
