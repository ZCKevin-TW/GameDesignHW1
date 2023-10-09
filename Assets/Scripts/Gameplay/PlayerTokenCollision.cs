using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a player collides with a token.
    /// </summary>
    /// <typeparam name="PlayerCollision"></typeparam>
    public class PlayerTokenCollision : Simulation.Event<PlayerTokenCollision>
    {
        public PlayerController player;
        public TokenInstance token;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            player.OnPlayerTokenCollision(token.GetTokenType());
            Debug.Log("Player Token Collision with" + (int)token.GetTokenType());
            AudioSource.PlayClipAtPoint(token.tokenCollectAudio, token.transform.position, 1.5f);
        }
    }
}