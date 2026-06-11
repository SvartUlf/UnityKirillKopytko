using Game.Player;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class BulletController : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    private Rigidbody2D _rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_speed, _rb.linearVelocity.y);
    }
}
