using UnityEngine;

public class ShurikenController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    [SerializeField] private float lifeTime = 3f;


    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
