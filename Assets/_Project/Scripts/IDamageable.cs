/// <summary>
/// 피격 처리 인터페이스. 총알에 맞았을 때 호출된다.
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// 피격 처리. 총알의 데미지를 받아 HP 감소, 사망 판정 등을 수행한다.
    /// </summary>
    void Hit(Bullet bullet);
}
