using UnityEngine;

public class PlayerHitController : MonoBehaviour, IDamageable
{
    public void Hit(Bullet other)
    {
        Debug.Log("Hit!!!");
    }
}
