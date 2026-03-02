using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 1000f;

    void Update()
    {
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
    }
}
