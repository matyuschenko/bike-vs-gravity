using UnityEngine;

// Camera follows a game object on one or more axes
public class CameraFollowAxis : MonoBehaviour
{
    public Transform Target; // Reference to the target object to follow
    private Vector3 _offset; // Variable to store the initial offset between the camera and the object

    void Start()
    {
        // Calculate the initial offset between the camera and the target object
        _offset = transform.position - Target.position;
    }

    void LateUpdate()
    {
        // Update the camera's position, copying the target's position on one or more axes
        // the camera's original position on other axes may be left as it was with transform.position.[axis name]
        transform.position = new Vector3(Target.position.x + _offset.x, Target.position.y + _offset.y, transform.position.z);
    }
}
