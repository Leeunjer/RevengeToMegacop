using UnityEngine;

public class Stage1Boss : BossEnemy
{
    [SerializeField] private BasicShotPattern basicShotPattern;
    [SerializeField] private GuidedMissilePattern guidedMissilePattern;
    [SerializeField] private BombPattern bombPattern;
    [SerializeField] private WavePattern wavePattern;

    [SerializeField] private Transform player;

    protected override void Start()
    {
        base.Start();
        if (player != null) ActivateBoss(player);
    }

    protected override void Update()
    {
        base.Update();
        if (Target == null) return;

        Vector3 direction = Target.position - transform.position;
        direction.y = 0f;
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);
    }

    protected override BossPattern[] GetPatternsForPhase(int phaseIndex)
    {
        return new BossPattern[]
        {
            basicShotPattern,
            guidedMissilePattern,
            bombPattern,
            wavePattern
        };
    }

    protected override void OnPhaseChanged(int phaseIndex, BossPhaseData data) { }
}
