using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;

    public void Construct(Transform transformTarget)
    {
        target = transformTarget;
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.transform.position + offset;
        //transform.position = desiredPosition;
    }
}
