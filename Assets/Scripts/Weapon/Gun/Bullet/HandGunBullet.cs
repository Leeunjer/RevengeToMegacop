using System.Collections;
using UnityEngine;

public class HandGunBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }
}