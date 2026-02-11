public class HandGun : GunWeapon
{
    protected override void Use()
    {
        if (CanFire()) Fire();
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}