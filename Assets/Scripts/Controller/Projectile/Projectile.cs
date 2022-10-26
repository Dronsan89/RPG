using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float lifeTime = 30f;
    //[SerializeField] protected float speed = 1;
    [SerializeField] LayerMask mask;
    [SerializeField] Vector3 rayOriginOffset;
    [SerializeField] Rigidbody rigidbody;

    [SerializeField] float raycastDistance = 1;
    [SerializeField] float radius = 0f;

    public event Action<Health> Hit;
    public float Timeout { get => lifeTime; set => lifeTime = value; }
    //public float Speed => speed;

    RaycastHit raycastHit;
    Func<bool> detectHit;

    Vector3 startPos;
    Vector3 endPos;

    public void Construct(float speed)
    {
        if (radius > Mathf.Epsilon)
            detectHit = SpherecastHit;
        else
            detectHit = RaycastHit;

        //Debug.Log("speed: " + speed);
        rigidbody.velocity = transform.forward * speed;
        Debug.Log("velocity: " + rigidbody.velocity);

        endPos = transform.position;
    }

    private void Update()
    {
        //transform.position += speed * transform.forward * Time.deltaTime;
        //float angle = Mathf.Atan2(rigidbody.velocity.y, rigidbody.velocity.z) * Mathf.Rad2Deg;
        //Debug.Log("angle: " + angle);
        //transform.localRotation = Quaternion.AngleAxis(angle, Vector3.right);
        //Quaternion newQuat = Quaternion.Euler(transform.localRotation.x + angle, transform.localRotation.y, transform.localRotation.z);
        //transform.localRotation = newQuat;
        //transform.localRotation = Quaternion.Euler(angle, 0, 0);

        //startPos = transform.position;

        //Vector3 direction = endPos - startPos;
        //transform.rotation = Quaternion.LookRotation(direction);
        //Debug.Log("startPos: " + startPos);
        //Debug.Log("endPos: " + endPos);
        //Debug.Log("direction: " + direction);

        //endPos = transform.position;

        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0)
            gameObject.SetActive(false); //TODO: add pool object

        if (detectHit())
            RegisterCollision(raycastHit.collider);
    }

    private void FixedUpdate()
    {
        
    }

    protected virtual void RegisterCollision(Collider collider)
    {
        Debug.Log(5);
        var health = collider.GetComponentInParent<Health>();
        if (health != null)
            Hit?.Invoke(health);

        rigidbody.velocity = Vector3.zero;
        rigidbody.isKinematic = true;

        //gameObject.SetActive(false); //TODO: add pool object
    }

    protected virtual bool RaycastHit() =>
        Physics.Raycast(transform.TransformPoint(rayOriginOffset), transform.forward, out raycastHit, raycastDistance, mask.value);
    protected virtual bool SpherecastHit() =>
        Physics.SphereCast(transform.TransformPoint(rayOriginOffset), radius, transform.forward, out raycastHit, raycastDistance, mask.value);

    private void OnDisable()
    {
        Hit = null;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.TransformPoint(rayOriginOffset), transform.forward * raycastDistance);
        if (radius > Mathf.Epsilon)
        {
            var origin = transform.TransformPoint(rayOriginOffset);
            Gizmos.DrawWireSphere(origin, radius);
            Gizmos.DrawWireSphere(origin + transform.forward * raycastDistance, radius);
        }
    }
#endif
}
