using UnityEngine;

public class SpaceShipScript : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 50;
    [SerializeField] private float horizontalRotationSpeed;
    [SerializeField] private float verticallRotationSpeed;
    [SerializeField] private float maxAngletilt = 45;

    private Vector2 input;
    private float acceleration = 2;

    private void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if(acceleration <= maxSpeed)
            {
                if (acceleration >= 2)
                {
                    acceleration += Input.GetAxis("Mouse ScrollWheel");
                }
                else
                {
                    acceleration = 2;
                }
            }
            else
            {
                acceleration = maxSpeed;
                 
            }
           

        }

        transform.position += transform.forward * acceleration * 10 * Time.deltaTime;

        transform.rotation *= ConvertToQuaternion(transform.up * acceleration / 20, input.x);
        transform.rotation *= ConvertToQuaternion(transform.right * acceleration / 20, input.y); // la ligne qui fait qu'on peut bouger de haut en bas

        var tiltChild = transform.GetChild(0);
        if(input.x == 0)
        {
            tiltChild.transform.localRotation *= ConvertToQuaternion(transform.up * acceleration/20, (Vector3.Angle(transform.up, -tiltChild.transform.right)-90)/50);
        }
        else
        {
            if(Mathf.Abs(Vector3.Angle(transform.up, -tiltChild.transform.right) - 90) <= maxAngletilt / 1+(acceleration*0.1f))
            {
                tiltChild.transform.localRotation *= ConvertToQuaternion(transform.up * acceleration / 50, input.x);
            }
            else
            {
                tiltChild.transform.localRotation *= ConvertToQuaternion(transform.up * acceleration / 20, (Vector3.Angle(transform.up, -tiltChild.transform.right) - 90) / 50);
            }
            
        }
       

    }

    public Quaternion ConvertToQuaternion(Vector3 axis, float angleInDegrees)
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
