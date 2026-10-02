using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public LayerMask collisionLayers;
    public Transform target;
    public Vector3 offset = new Vector3(0f, 1.5f, -4f);
    public float sensitivity = 0.1f;
    public float pitchMin = -40f;
    public float pitchMax = 80f;

    private float yaw;
    private float pitch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        yaw += mouseDelta.x * sensitivity;
        pitch -= mouseDelta.y * sensitivity;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 pivotPosition = target.position + Vector3.up * offset.y;
        Vector3 targetPosition = target.position + rotation * offset;

        if (Physics.Linecast(pivotPosition, targetPosition, out RaycastHit hit, collisionLayers))
        {
            targetPosition = hit.point;
        }

        transform.SetPositionAndRotation(targetPosition, rotation);
    }
}