using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private float useDelay;
    private float leftTimeToUse;

    void Awake()
    {
        leftTimeToUse = useDelay;
    }

    public void TryUse()
    {
        // if (leftTimeToUse <= 0)
        // {
        // leftTimeToUse = useDelay;
        Use();
        // }
    }

    protected abstract void Use();
}