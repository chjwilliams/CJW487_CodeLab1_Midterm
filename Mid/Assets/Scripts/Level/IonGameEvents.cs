using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsEventSystem.GameEvents;

namespace IonGameEvents
{
	public class ParticleEnteredZoneEvent : GameEvent
	{
		public readonly ZoneManager zone;

		public ParticleEnteredZoneEvent(ZoneManager zoneEntered)
		{
			zone = zoneEntered;
		}
	}

	public class ParticleExitedZoneEvent : GameEvent
	{
		public readonly ZoneManager zone;

		public ParticleExitedZoneEvent(ZoneManager zoneEntered)
		{
			zone = zoneEntered;
		}
	}

	public class StartTimerEvent : GameEvent
	{
		public StartTimerEvent () {	}
	}

    public class TenSecondsLeftEvent : GameEvent
    {
        public readonly bool tenSecondsLeft;
        public TenSecondsLeftEvent(bool _tenSecondsLeft)
        {
            tenSecondsLeft = _tenSecondsLeft;
        }
    }

    public class TimeIsOverEvent : GameEvent
	{
		public readonly Timer timer;

		public TimeIsOverEvent (Timer gameTimer)
		{
			timer = gameTimer;
		}
	}
}