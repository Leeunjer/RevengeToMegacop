using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed;

    private float realSpeed = 0f;

    private float gravity = -9.81f;
    private Vector3 velocity;

    private Plane groundPlane;

    void Awake()
    {
        realSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        InputDash();
        InputMove();
        InputRotation();
        Gravity();
    }

    void InputDash()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            realSpeed = speed * 2;
        }
        else
        {
            realSpeed = speed;
        }
    }

    void InputMove()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        Vector3 dir = (Vector3.right * h + Vector3.forward * v).normalized;
        controller.Move(dir * (realSpeed * Time.deltaTime));
    }

    void InputRotation()
    {
        groundPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);

            hitPoint.y = transform.position.y;

            transform.LookAt(hitPoint);
        }
    }

    void Gravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}