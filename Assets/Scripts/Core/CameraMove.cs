using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private Vector3 _offset;
    

    void LateUpdate()
    {
        if (_player)
        {
            Vector3 playerPosition = new Vector3(
                _player.position.x + _offset.x,
                _player.position.y + _offset.y,
                transform.position.z
                );
            Vector3 smoothPos = Vector3.Lerp(transform.position, playerPosition, _smoothSpeed);
            transform.position = smoothPos;
        }
    }
}
