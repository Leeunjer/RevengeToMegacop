using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
          weapon?.TryUse();   
        }
    }
}