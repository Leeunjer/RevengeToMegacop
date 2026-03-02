using UnityEngine;

public class SwordRotator : MonoBehaviour
{
    [SerializeField] private Transform sword;
    [SerializeField] private float rotationSpeed = 1f;

    void Update()
    {
        if (sword == null) return;
        sword.Rotate(Vector3.up, 360f * rotationSpeed * Time.deltaTime);
    }
}
