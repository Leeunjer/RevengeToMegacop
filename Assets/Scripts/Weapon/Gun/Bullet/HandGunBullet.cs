using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HandGunBullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    IEnumerator Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;

        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }
}