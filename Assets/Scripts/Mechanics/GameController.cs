using Platformer.Core;
using Platformer.Model;
using UnityEngine;
using TMPro;
using System;


namespace Platformer.Mechanics
{
    /// <summary>
    /// This class exposes the the game model in the inspector, and ticks the
    /// simulation.
    /// </summary> 
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        //This model field is public and can be therefore be modified in the 
        //inspector.
        //The reference actually comes from the InstanceRegister, and is shared
        //through the simulation and events. Unity will deserialize over this
        //shared reference when the scene loads, allowing the model to be
        //conveniently configured inside the inspector.
        public PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        public TMP_Text timeDisplay;
        private DateTime roundStartTime;

        void OnEnable()
        {
            Instance = this;
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        void Update()
        {
            if (Instance == this)
            {
                Simulation.Tick();
                timeDisplay.SetText(CurTime().ToString("D3"));
            }
        }
        private void Awake()
        {
            ResetTimer();
        }
        public void ResetTimer()
        {
            roundStartTime = System.DateTime.UtcNow;
        }
        int CurTime()
        {
            return (int)(System.DateTime.UtcNow - roundStartTime).TotalSeconds;
        }
    }
}