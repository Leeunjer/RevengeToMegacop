using System.Collections.Generic;
using UnityEngine;




namespace Boss3
{
    public class Boss3BulletPool : MonoBehaviour
    {
    private static Boss3BulletPool instance;

    public static Boss3BulletPool Instance => instance;

    private Queue<GameObject> _bulletPool = new Queue<GameObject>();

    [SerializeField] private GameObject BulletPrefab;

        void Awake()
        {
           if(instance != null && instance != this )
            {
                Destroy(gameObject);
                return;
            }
           
            instance = this;

        }
        void OnDestroy()
        {
        if (instance == this) instance = null;
        }

        public GameObject GetBullet()
        {   
            
            
            if(_bulletPool.Count <= 0)
            {
                CreateBullet();
            }


            GameObject currentBullet = _bulletPool.Dequeue();
            currentBullet.SetActive(true);

            BossBullet bullet = currentBullet.GetComponent<BossBullet>();
            if (bullet != null)
            bullet.IsReturned = false;
            return currentBullet;
        }
            
        public void ReturnBullet (GameObject bulletObj)
        {
            BossBullet bullet = bulletObj.GetComponent<BossBullet>();
            if (bullet != null && bullet.IsReturned) return;

            if (bullet != null)
            bullet.IsReturned = true;

            bulletObj.SetActive(false);
            _bulletPool.Enqueue(bulletObj);
            
        }
        private void CreateBullet()
        {
            GameObject newBullet = Instantiate(BulletPrefab, transform);
            newBullet.SetActive(false);
            _bulletPool.Enqueue(newBullet);
        }

        
    }
}

