using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP = 3;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        // -1 for killing
        public bool IsAlive => currentHP >= 0;

        int currentHP;


        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            if (currentHP < 0)
            {
                Awake();
                return;
            } 
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement()
        {
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
            Debug.Log("Health decrease, now " + currentHP);
            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>

        // When doing the whole resetting scene, setting HP to -1 means it should not die one more
        // This function is called ONLY when resetting scene
        public void Die()
        {
            currentHP = -1;
        //    while (currentHP > 0) Decrement();
        }

        void Awake()
        {
            Debug.Log("Awaking player with HP " + maxHP);
            currentHP = maxHP;
        }
    }
}
