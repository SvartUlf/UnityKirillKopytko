using System;
using UnityEngine;
namespace Game.Player
{
    public class PlayerAnimationComponent : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private InputComponent _input;
        private Rigidbody2D _rb;
        private PlayerMovementComponent _playerMovement;
        private PlayerFireComponent _playerFireComponent;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            _playerMovement = GetComponent<PlayerMovementComponent>();
            _playerFireComponent = GetComponent<PlayerFireComponent>();
            _playerFireComponent.Fire += DoFire;
        }

        // Update is called once per frame
        void Update()
        {
            float moveX = Math.Abs(_input.GetMove().x);
            float moveY = _rb.linearVelocityY;
            _animator.SetFloat("MoveX", moveX);
            _animator.SetFloat("MoveY", moveY);
            _animator.SetBool("IsGrounded", _playerMovement.IsGrounded);
            _animator.SetBool("IsSecondJump", _playerMovement.IsSecondJump);
        }

        private void DoFire()
        {
            _animator.SetTrigger("IsFire");
        }
    }
}