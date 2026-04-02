using UnityEngine;

/// <summary>
/// 처형 완료 후 OnExecutionComplete 이벤트에 전달되는 결과 데이터.
/// </summary>
public struct ExecutionResult
{
    public Enemy Target;
    public Vector3 Position;
}
