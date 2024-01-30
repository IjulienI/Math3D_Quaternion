using UnityEngine;

public class SpaceShipScript : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 100;
    [SerializeField] private float horizontalRotationSpeed;
    [SerializeField] private float verticallRotationSpeed;

    private Vector2 input;
    private float acceleration;

    private void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if(acceleration >= 0 && acceleration < maxSpeed)
            {
                acceleration += Input.GetAxis("Mouse ScrollWheel")*5;
            }
        }
        if (acceleration < 0)
        {
            acceleration = 0;
        }

        transform.position += transform.forward * (acceleration * 2) * Time.deltaTime;

        transform.localRotation *= ConvertToQuaternion((Vector3.up * 0.2f) * 1/acceleration, input.x);
        var tiltChild = transform.GetChild(0);
        tiltChild.transform.localRotation = Quaternion.Lerp(tiltChild.transform.localRotation , ConvertToQuaternion(-Vector3.forward*45, input.x), 3 * Time.deltaTime);
    }

    private Quaternion ConvertToQuaternion(Vector3 axis, float angleInDegrees)
    {
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        float halfAngle = angleInRadians/2;

        float x = axis.x * Mathf.Sin(halfAngle);
        float y = axis.y * Mathf.Sin(halfAngle);
        float z = axis.z * Mathf.Sin(halfAngle);
        float w = Mathf.Cos(halfAngle);
        return new Quaternion(x, y, z, w);
    }

}
