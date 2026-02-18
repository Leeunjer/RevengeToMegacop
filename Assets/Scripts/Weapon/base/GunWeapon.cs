using System.Collections;

using UnityEngine;

public abstract class GunWeapon : Weapon
{
    [field: SerializeField] public int MaxAmmo { get; private set; }

    [field: SerializeField] public int Ammo { get; private set; }

    [field: SerializeField] public float BulletSpeed { get; private set; }

    [SerializeField] protected GameObject bulletPrefab;

    [SerializeField] protected Transform firePoint;

    [SerializeField] private float reloadTime = 2f;

    private bool isReloading = false;

    public bool CanFire()
    {
        return 0 < Ammo;
    }

    public void Reload()
    {
        if (isReloading) return;
        isReloading = true;
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(reloadTime);
        Ammo = MaxAmmo;
        isReloading = false;
    }

    protected override void Awake()
    {
        base.Awake();
        if (MaxAmmo < Ammo) Ammo = MaxAmmo;
    }

    protected override void Use()
    {
        if (CanFire()) Fire();
    }

    private void Fire()
    {
        Ammo--;
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).GetComponent<Bullet>();
        if (bullet != null) bullet.Speed = BulletSpeed;
    }
}