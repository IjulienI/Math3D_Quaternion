using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] private float HorizontalRotationSpeed = 5;
    [SerializeField] private float verticalRotationSpeed = 5;
    [SerializeField] private float horizontalRotation = 45;
    [SerializeField] private float tiltPower = 5;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    private float speed;

    private float horizontalInput;
    private float verticalInput;

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if(speed < minSpeed)
        {
            speed = minSpeed;
        }
        else if(speed > maxSpeed)
        {
            speed = maxSpeed;
        }
        else
        {
            speed += Input.mouseScrollDelta.y;
        }

        Vector3 rotX = new Vector3(transform.position.x * verticalRotationSpeed * verticalInput,0,0);

        transform.localRotation *= ConvertToQuaternion(Vector3.right, verticalRotationSpeed * -verticalInput * Time.deltaTime) * ConvertToQuaternion(Vector3.forward, -horizontalRotation * horizontalInput * Time.deltaTime) * ConvertToQuaternion(Vector3.right, tiltPower * verticalInput * Time.deltaTime);
    }

    private Quaternion ConvertToQuaternion(Vector3 axis, float angleInDegrees)
    {
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        float halfAngle = angleInRadians / 2;

        float x = axis.x * Mathf.Sin(halfAngle);
        float y = axis.y * Mathf.Sin(halfAngle);
        float z = axis.z * Mathf.Sin(halfAngle);
        float w = Mathf.Cos(halfAngle);
        return new Quaternion(x, y, z, w);
    }

    private void OnGUI()
    {
        GUILayout.Label("Horizontal : " + horizontalInput + "\n" + "Vertical : " + verticalInput);
    }
}