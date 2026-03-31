using UnityEngine;

namespace Boss3
{
    public class Stage3Boss : BossEnemy
{
    [SerializeField] private FireAtRandom _fireAtRandomPattern;
    
    [SerializeField] private SmokeBomb _smokeBombPattern;
    [SerializeField] private OscillatingBulletPattern _OscillatingBulletPattern;
    
    

    protected override BossPattern[] GetPatternsForPhase(int phaseIndex)
    {
        if (phaseIndex == 0)
        {
            return new BossPattern[] 
            {
                _OscillatingBulletPattern,
                _smokeBombPattern
                
            };
        }
        if (phaseIndex == 1)
        {
            return new BossPattern[]
            {
                _fireAtRandomPattern
            };
        }

        return new BossPattern[0];
    }

    protected override void OnPhaseChanged(int phaseIndex, BossPhaseData data)
    {
        
    }
}
}