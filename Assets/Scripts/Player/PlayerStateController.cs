using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    [field: SerializeField] public float Hp { get; private set; }
    [field: SerializeField] public float ExecutionGauge { get; private set; }
    [field: SerializeField] public float MaxExecutionGauge { get; private set; }
    [field: SerializeField] public float ExecutionGaugeIncreaseStep { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void TakeDamage(float damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseExecutionGauge()
    {
        ExecutionGauge += ExecutionGaugeIncreaseStep;
        if (MaxExecutionGauge < ExecutionGauge)
        {
            ExecutionGauge = MaxExecutionGauge;
        }
    }

    public bool CanExecute()
    {
        return MaxExecutionGauge <= ExecutionGauge;
    }

    public void Executed()
    {
        ExecutionGauge = 0;
    }
}
