using System;

using UnityEngine;

public abstract class BossPattern : MonoBehaviour
{
    [SerializeField] private float cooldown = 2f;
    [SerializeField] private float weight = 1f;

    private float lastExecuteTime = float.NegativeInfinity;

    public float Weight => weight;

    public bool CanExecute()
    {
        return Time.time >= lastExecuteTime + cooldown;
    }

    public void Execute(BossEnemy boss, Action onComplete)
    {
        lastExecuteTime = Time.time;
        ExecutePattern(boss, onComplete);
    }

    protected abstract void ExecutePattern(BossEnemy boss, Action onComplete);
}
