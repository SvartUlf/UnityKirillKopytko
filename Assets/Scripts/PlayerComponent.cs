using UnityEngine;
namespace Game.Player {
    [RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D), typeof(PlayerMovementComponent))]
    public class PlayerComponent : MonoBehaviour
    {
        private Rigidbody2D _rb;

        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _jumpForce = 8f;
        internal float Speed => _speed;
        internal float JumpForce => _jumpForce;
        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

    }
}
