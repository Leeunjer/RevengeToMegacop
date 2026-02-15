using System.Collections;
using UnityEngine;

public class HandGunBullet : Bullet
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }


    void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        damageable?.Hit(this);
    }
}