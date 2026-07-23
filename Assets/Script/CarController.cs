using UnityEngine;

// Drives a car built from a body Rigidbody2D + two wheels connected
// with WheelJoint2D (front and rear). Accelerate/brake just drive
// the wheel motors; Unity's 2D physics + the WheelJoint2D suspension
// handle the "climbing hills" feel for free.
[RequireComponent(typeof(Rigidbody2D))]
public class CarController : MonoBehaviour
{
    [Header("Wheels (assign in Inspector)")]
    public WheelJoint2D frontWheel;
    public WheelJoint2D rearWheel;

    [Header("Driving")]
    public float motorSpeed = 1200f;   // degrees/sec at full throttle
    public float motorTorque = 800f;

    private float input; // 1 = accelerate, -1 = brake/reverse, 0 = idle

    void Update()
    {
        input = 0f;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.W)) input = 1f;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.S)) input = -1f;

        // For WebGL/mobile builds, wire two on-screen buttons to call
        // SetInput(1), SetInput(-1) and SetInput(0) instead of reading keys.

        if (GameManager.Instance != null && GameManager.Instance.isGameOver) input = 0f;
    }

    // Hook this up to UI buttons for touch controls.
    public void SetInput(float value) => input = value;

    void FixedUpdate()
    {
        ApplyDrive(frontWheel);
        ApplyDrive(rearWheel);
    }

    void ApplyDrive(WheelJoint2D wheel)
    {
        if (wheel == null) return;
        JointMotor2D motor = wheel.motor;
        motor.motorSpeed = -input * motorSpeed; // flip sign if the car drives backwards
        motor.maxMotorTorque = motorTorque;
        wheel.motor = motor;
        wheel.useMotor = true;
    }
}