using UnityEngine;

[RequireComponent(typeof(PlayerExecutionController))]
[RequireComponent(typeof(PlayerHitController))]
[RequireComponent(typeof(PlayerMovementController))]
[RequireComponent(typeof(PlayerShurikenController))]
public class PlayerController : MonoBehaviour
{
    private PlayerExecutionController playerExecutionController;
    private PlayerHitController playerHitController;
    private PlayerMovementController playerMovementController;
    private PlayerShurikenController playerShurikenController;

    void Awake()
    {
        playerExecutionController = GetComponent<PlayerExecutionController>();
        playerHitController = GetComponent<PlayerHitController>();
        playerMovementController = GetComponent<PlayerMovementController>();
        playerShurikenController = GetComponent<PlayerShurikenController>();
    }

    void Update()
    {
        playerHitController.UpdateParries();
        playerMovementController.UpdateGravity();
        playerShurikenController.UpdateCooldown();

        playerExecutionController.HandleExecution();
        playerHitController.HandleHit();
        playerMovementController.HandleMovement();
        playerShurikenController.HandleShuriken();
    }
}
