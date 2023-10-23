using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.EventSystems;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using TMPro;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 5;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public TMP_Text tokenDisplay, keyDisplay, scoreDisplay;
        
        public Health health;
        public bool controlEnabled = true;
        private bool Invinsible = false;
        [SerializeField] private DataHolder data;
        //private int Score = 0;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        // Collecting Items
        private int[] TokenCount = { 0, 0 };
        public bool KeyDone()
        {
            return TokenCount[(int)TokenTypes.Key] == 2;
        }
        public void AddScore(int x)
        {
            data.Score += x;
            if (scoreDisplay != null)
            {
                scoreDisplay.SetText(data.Score.ToString()); 
            }
        }
        IEnumerator BecomeInvinsibleForSec(float last_time)
        {
            Invinsible = true;
            yield return new WaitForSeconds(last_time);
            Invinsible = false;
        }
        public void GetHurt()
        {
            if (Invinsible) return;
            StartCoroutine(BecomeInvinsibleForSec(2));
            animator.SetTrigger("hurt");
            health.Decrement();
        }
        public void OnPlayerTokenCollision(TokenTypes tp)
        { 
            ++TokenCount[(int)tp];
            switch (tp)
            {
                case TokenTypes.Diamond:
                    AddScore(100);
                    if (tokenDisplay != null)
                        tokenDisplay.SetText(TokenCount[(int)tp].ToString());
                    break;
                case TokenTypes.Key:
                    if (keyDisplay != null)
                        keyDisplay.SetText(TokenCount[(int)tp].ToString() + "/2");
                    break;
            }
            // Handle the collision event here
            // You can access event.token and event.player to get information about the collision.
        }

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            data.Score = 0;
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxisRaw("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButton("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                // Debug.Log("Stopping the jump");
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
                // Debug.Log($"move x y :{move.x}, {move.y}");
                // Debug.Log(move.x, move.y);
            } 

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
            // Debug.Log($"move x y :{move.x}, {move.y}");

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}