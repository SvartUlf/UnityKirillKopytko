using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
namespace Game.Player
{
    public class PlayerMovementComponent : MonoBehaviour
    {
        [SerializeField] private InputComponent _input;
        [SerializeField] private Tilemap _tileMap;
        private PlayerComponent _player;
        private bool _isGrounded = true;
        internal bool IsGrounded => _isGrounded;
        private int _jumpCount = 0;
        public bool IsSecondJump => _jumpCount >= 2;
        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
            _player = GetComponent<PlayerComponent>();
        }
        void Update()
        {
            if (_input.GetJump() && (_isGrounded || _jumpCount < 2))
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _player.JumpForce);
                _isGrounded = false;
                _jumpCount++;
            }

            if (_input.GetClick())
            {
                Vector3 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int tilePosition = _tileMap.WorldToCell(clickPoint);
                Debug.Log("ClickPosition: " + clickPoint);
                Debug.Log("ClickTile: "+ tilePosition);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Ground")
            {
                _isGrounded = true;
                _jumpCount = 0;
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.tag == "Ground")
            {
                _isGrounded = false;
                _jumpCount = 1;
            }
        }

        void FixedUpdate()
        {
            Vector3 moveDir = _input.GetMove();
            _rb.linearVelocity = new Vector2(moveDir.x * _player.Speed, moveDir.y * _player.Speed);
            if(moveDir.x < 0)
            {
                _sr.flipX = true;
            }
            else if (moveDir.x > 0)
            {
                _sr.flipX = false;
            }
        }
    }
}
