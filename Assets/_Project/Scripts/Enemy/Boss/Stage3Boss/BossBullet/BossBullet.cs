using UnityEngine;
namespace Boss3
{
    public class BossBullet : MonoBehaviour
    {
    [SerializeField] float _bulletSpeed;

    [SerializeField] private Vector3 _moveDirection;
    [SerializeField] private float _lifeTime = 2f;
    private float _timer;
    
    
    public bool IsReturned { get; set; }

        void OnEnable()
        {
            _timer = 0;
        }

        void Update()
    {
        if (IsReturned) return;

        transform.position += _moveDirection * _bulletSpeed * Time.deltaTime;

        _timer += Time.deltaTime;
            if (_timer > _lifeTime)
            {
                Boss3BulletPool.Instance.ReturnBullet(gameObject);
            }
    }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("Player Hit");
        }

        public void SetOwner(GameObject ownerObject)
        {
            
        }

        public void Fire(Vector3 position, Quaternion rotation, float speed, GameObject owner)
        {
        transform.position = position;
        transform.rotation = rotation;
        SetOwner(owner);

        _moveDirection = transform.forward;
        _bulletSpeed = speed;
    }
    }

    
}

