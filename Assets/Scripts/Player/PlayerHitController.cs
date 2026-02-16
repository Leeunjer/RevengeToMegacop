using System;

using UnityEngine;

public class PlayerHitController : MonoBehaviour, IDamageable
{

    [Range(-1f, 1f)]
    [SerializeField]
    private float parryThreshold = 0.5f;

    [SerializeField]
    private float parryDuration = 0.5f;

    private ParryController parryController = new ParryController();

    // Update is called once per frame
    void Update()
    {
        parryController.RemoveTooEarlyParries(parryDuration);
        InputParry();
    }

    private void InputParry()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            parryController.StackParry();
        }
    }

    public void Hit(Bullet bullet)
    {
        if (CanParry(bullet))
        {
            Parry(bullet);
            return;
        }

        TakeDamage();
    }

    private bool CanParry(Bullet bullet)
    {
        return IsBulletInFront(bullet) && parryController.CanParry();
    }

    private bool IsBulletInFront(Bullet bullet)
    {
        Vector3 directionToBullet = bullet.transform.forward.normalized * -1f;

        directionToBullet.y = 0f;
        Vector3 playerForward = transform.forward.normalized;
        playerForward.y = 0f;

        float dot = Vector3.Dot(directionToBullet, playerForward);

        return parryThreshold < dot;
    }

    private void Parry(Bullet bullet)
    {
        bullet.Reflect();
        parryController.Parry();
    }

    private void TakeDamage()
    {
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawRay(transform.position, transform.forward * 2f);

        float angle = Mathf.Acos(parryThreshold) * Mathf.Rad2Deg;

        Vector3 leftDirection = Quaternion.Euler(0, -angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftDirection * 2f);

        Vector3 rightDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, rightDirection * 2f);
    }
}
