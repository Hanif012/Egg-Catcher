using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TrajectoryPredictor))]
public class ProjectileThrow : MonoBehaviour
{
    TrajectoryPredictor trajectoryPredictor;

    [SerializeField]
    Rigidbody objectToThrow;

    [SerializeField, Range(0.0f, 100.0f)]
    float force;

    [SerializeField]
    Transform StartPosition;

    [SerializeField]
    Transform EndPosition;

    public InputAction fire;

    void OnEnable()
    {
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();

        if (StartPosition == null)
            StartPosition = transform;

        if (EndPosition == null)
        {
            EndPosition = transform;
            Debug.LogWarning("EndPosition not set. Defaulting to StartPosition");
        }

        fire.Enable();
        fire.performed += ThrowObject;
    }

    void OnDisable()
    {
        fire.Disable();
        fire.performed -= ThrowObject;
    }

    void Update()
    {
        Predict();
    }

    void Predict()
    {
        trajectoryPredictor.PredictTrajectory(ProjectileData());
    }

    ProjectileProperties ProjectileData()
    {
        ProjectileProperties properties = new ProjectileProperties();
        Rigidbody r = objectToThrow.GetComponent<Rigidbody>();

        // Calculate direction and ensure it points to the exact EndPosition
        properties.direction = (EndPosition.position - StartPosition.position).normalized;
        properties.initialPosition = StartPosition.position;
        properties.initialSpeed = force;
        properties.mass = r.mass;
        properties.drag = r.linearDamping;

        return properties;
    }

    void ThrowObject(InputAction.CallbackContext ctx)
    {
        Rigidbody thrownObject = Instantiate(objectToThrow, StartPosition.position, Quaternion.identity);
        Vector3 direction = EndPosition.position - StartPosition.position;
        Vector3 horizontalDirection = new Vector3(direction.x, 0f, direction.z);

        float horizontalDistance = horizontalDirection.magnitude;
        float verticalDistance = direction.y;
        float gravity = Mathf.Abs(Physics.gravity.y);

        // Time to reach the target horizontally: time = distance / speed
        float timeToTarget = horizontalDistance / force;
        // kinematic equation: y = v_y * t - 0.5 * g * t^2
        float verticalSpeed = (verticalDistance + 0.5f * gravity * timeToTarget * timeToTarget) / timeToTarget;
        Vector3 velocity = horizontalDirection.normalized * force + Vector3.up * verticalSpeed;

        thrownObject.linearVelocity = velocity;
    }
}
