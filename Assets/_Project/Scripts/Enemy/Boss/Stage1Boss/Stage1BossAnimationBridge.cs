using UnityEngine;

public class Stage1BossAnimationBridge : MonoBehaviour
{
    [SerializeField] private Stage1Boss boss;

    void OnAnimatorMove()
    {
        Animator animator = GetComponent<Animator>();
        if (animator == null) return;

        Transform root = transform.parent != null ? transform.parent : transform;
        Vector3 newPosition = root.position + animator.deltaPosition;
        newPosition.y = root.position.y;
        root.position = newPosition;
    }

    public void OnFireAnimationEvent()
    {
        if (boss != null)
            boss.OnFireAnimationEvent();
    }

    public void OnAnimationCompleteEvent()
    {
        if (boss != null)
            boss.OnAnimationCompleteEvent();
    }
}
