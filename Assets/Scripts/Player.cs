using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Gameplay parameters
    public float Speed; // Top Speed
    public float Acceleration; // 
    public float SpeedReverse; // Reverse Top Speed
    public float AccelerationReverse; // 
    public float BrakeForce;
    public float LeanForce;

    // References to other game objects
    public WheelJoint2D BackWheelJoint;
    public Timer TimerScript; // For the purpose of stopping timer on finish
    public GameObject NextLevelButton;
    public GameObject ModelNormal; // Three objects with three sprites of a driver for animation
    public GameObject ModelForward;
    public GameObject ModelBack;
    public ParticleSystem ExhaustParticles;

    Vector2 zeroVector = new Vector2(0, 0);

    private Rigidbody2D rb; // Variable to store this game object's rigid body
    InputAction moveAction; // Variable to reference move actions


    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Reference to this game object's rigid body
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void FixedUpdate()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        if (moveValue[0] > 0)
        {
            // Lean forward
            rb.AddTorque(LeanForce * (-1) * Mathf.Deg2Rad);
            ModelNormal.SetActive(false);
            ModelForward.SetActive(true);
            ModelBack.SetActive(false);
        }
        else if (moveValue[0] < 0)
        {
            // Lean backward
            rb.AddTorque(LeanForce * Mathf.Deg2Rad);
            ModelNormal.SetActive(false);
            ModelForward.SetActive(false);
            ModelBack.SetActive(true);
        }
        else
        {
            ModelNormal.SetActive(true);
            ModelForward.SetActive(false);
            ModelBack.SetActive(false);
        }

        JointMotor2D motor = new JointMotor2D(); // Creating motor component; we'll set it up and attach it to the wheel joint

        if (moveValue[1] > 0)
        {
            // Acceleration
            motor.motorSpeed = Speed;
            motor.maxMotorTorque = Acceleration;
        }
        else if (moveValue[1] < 0)
        {
            if (rb.linearVelocity.magnitude > 0.2f && rb.linearVelocity.normalized[0] > 0)
            {
                // Break
                motor.motorSpeed = 0;
                motor.maxMotorTorque = BrakeForce;
            }
            else
            {
                // Reverse
                motor.motorSpeed = SpeedReverse;
                motor.maxMotorTorque = AccelerationReverse;
            }
        }
        else
        {
            motor.maxMotorTorque = 0; // Motor is "neutral" if neither Left nor Right are pressed
        }

        BackWheelJoint.motor = motor; // Attach new motor to the wheel joint
    }


    // Finish
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "FinishActual")
        {
            TimerScript.StopTimer();
            NextLevelButton.SetActive(true);
        }
    }
}
