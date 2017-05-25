using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IonGameEvents;
using ChrsUtils.ChrsEventSystem.EventsManager;
using ChrsUtils.ChrsEventSystem.GameEvents;

public class BackgroundFade : MonoBehaviour
{
    public Animator anim;

    private TenSecondsLeftEvent.Handler onTenSecondsLeft;
	// Use this for initialization
	private void Start ()
    {
        anim = GetComponent<Animator>();

        onTenSecondsLeft = new TenSecondsLeftEvent.Handler(OnTenSecondsLeft);

        GameEventsManager.Instance.Register<TenSecondsLeftEvent>(onTenSecondsLeft);
	}

    private void OnTenSecondsLeft(GameEvent e)
    {
        bool flash = ((TenSecondsLeftEvent)e).tenSecondsLeft;
        anim.SetBool("TenSecondsLeft", flash);
    }
	

}
