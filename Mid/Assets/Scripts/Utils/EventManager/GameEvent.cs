using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChrsUtils
{
    namespace ChrsEventSystem
    { 
        namespace GameEvents
        {
            public abstract class GameEvent 
            {
                public delegate void Handler(GameEvent e);
            }
        }
    }
}