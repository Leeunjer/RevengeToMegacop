using UnityEngine;

public class PlayerShurikenController : MonoBehaviour
{
    [SerializeField] private PlayerMovementController controller;
    [SerializeField] private GameObject shurikenPrefab;
    [SerializeField] private float coolTime = 3f;

    private float currentCooldown;

    private GameObject shuriken = null;


    void Update()
    {
        UpdateCooldown();
        InputThrowKey();
    }

    private void UpdateCooldown()
    {
        if(0 < currentCooldown) currentCooldown -= Time.deltaTime;
    }

    private void InputThrowKey()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (InCoolTime()) return;

            if(HasFlyingShuriken()) Teleport();
            else ThrowShuriken();
        }
    }

    private bool InCoolTime()
    {
        return 0 < currentCooldown;
    }

    private bool HasFlyingShuriken()
    {
        return shuriken != null;
    }

    private void Teleport()
    {
        controller.Teleport(shuriken.transform.position);
        Destroy(shuriken);
        shuriken = null;
        currentCooldown = coolTime;
    }

    private void ThrowShuriken()
    {
        shuriken = Instantiate(shurikenPrefab);
        shuriken.transform.position = transform.position;
        shuriken.transform.forward = transform.forward;
    }
}
