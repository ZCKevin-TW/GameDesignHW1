using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Marks a trigger as a VictoryZone, usually used to end the current game level.
    /// </summary>
    public class VictoryZone : MonoBehaviour
    {
        [SerializeField] GameObject Hint;
        private void Awake()
        {
            Hint.SetActive(false); 
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            var p = collider.gameObject.GetComponent<PlayerController>();
            if (p != null) {
                if (p.KeyDone())
                {
                    var ev = Schedule<PlayerEnteredVictoryZone>();
                    ev.victoryZone = this;
                } else
                {
                    Hint.SetActive(true); 
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collider)
        {
            if (!collider.CompareTag("Player")) return;
            Hint.SetActive(false); 
        }
    }
}