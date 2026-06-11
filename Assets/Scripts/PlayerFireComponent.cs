using UnityEngine;
namespace Game.Player
{
    public class PlayerFireComponent : MonoBehaviour
    {
        internal delegate void Fired();
        internal Fired Fire;
        private Rigidbody2D _rb;
        private float _timeOfLastShot = 0f;
        [SerializeField] private GameObject Bullet;
        [SerializeField] private InputComponent _input;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            //if (_input.GetFire())
            //{
            //    if (Fire != null)
            //    {
            //        Fire();
            //    }
            //    FireFrontGun();
            //}
        }

        private void FireFrontGun()
        {
            if (Time.time - _timeOfLastShot >= 0.3f) //If the time elapsed is more than the fire rate, allow a shot
            {
                var frontGunPosition = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                var bullet = Instantiate(Bullet, frontGunPosition, Quaternion.identity);
                _timeOfLastShot = Time.time;   //set new time of last shot
            }

        }
    }
}
