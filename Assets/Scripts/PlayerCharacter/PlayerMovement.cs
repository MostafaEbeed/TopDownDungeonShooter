using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController cc;
    private PlayerStats stats;
    private Camera mainCam;

    private Vector2 moveInput;
    private Vector3 lookTarget;
    private bool hasLookTarget;

    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private LayerMask groundMask; // Make sure to assign this in the Inspector

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        stats = GetComponent<PlayerStats>();
        mainCam = Camera.main;
    }

    public void SetMoveInput(Vector2 input) => moveInput = input;

    private void Update()
    {
        HandleAiming();
        HandleMovement();
    }

    private void HandleAiming()
    {
        if (mainCam == null)
        {
            Debug.LogWarning("Main Camera not found.");
            return;
        }

        Ray ray = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundMask))
        {
            lookTarget = hit.point;
            hasLookTarget = true;

            Vector3 direction = lookTarget - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            hasLookTarget = false;
        }
    }

    private void HandleMovement()
    {
        if (!hasLookTarget) return;

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 moveDir = (right * moveInput.x + forward * moveInput.y).normalized;

        if (moveDir.sqrMagnitude > 0.01f)
        {
            Vector3 move = moveDir * stats.MoveSpeed;
            move.y += Physics.gravity.y * Time.deltaTime; // Optional gravity support
            cc.Move(move * Time.deltaTime);
        }
    }
}
