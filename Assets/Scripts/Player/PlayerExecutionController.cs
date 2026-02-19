using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStateController))]
public class PlayerExecutionController : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerStateController playerStateController;

    [SerializeField] private LayerMask enemyLayerMask;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerStateController = GetComponent<PlayerStateController>();
    }

    public void HandleExecution()
    {
        if (Input.GetMouseButtonDown(0) && playerStateController.CanExecute())
        {
            TryExecute();
        }
    }

    private void TryExecute()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, enemyLayerMask))
        {
            Execute(hit.collider.gameObject);
        }
    }

    void Update()
    {
        HandleExecution();
    }

    private void Execute(GameObject enemy)
    {
        Vector3 enemyPosition = enemy.transform.position;
        Destroy(enemy);
        characterController.enabled = false;
        transform.position = enemyPosition;
        characterController.enabled = true;
        playerStateController.Executed();
    }
}
