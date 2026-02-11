using UnityEngine;

public abstract class GunWeapon : Weapon
{
    [field: SerializeField] public int MaxAmmo { get; private set; }

    [field: SerializeField] public int Ammo { get; private set; }

    [SerializeField] protected GameObject bulletPrefab;

    [SerializeField] protected Transform firePoint;

    protected virtual void Awake()
    {
        if (MaxAmmo < Ammo) Ammo = MaxAmmo;
    }

    protected bool CanFire()
    {
        return 0 < Ammo;
    }
}