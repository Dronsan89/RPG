using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private Vector3 rayOriginOffset;
    [SerializeField] private Rigidbody rigidbody;

    [SerializeField] private float lifeTime = 30f;
    [SerializeField] private float raycastDistance = 1;
    [SerializeField] private float radius = 0f;

    public event Action<Health> Hit;
    public float Timeout { get => lifeTime; set => lifeTime = value; }

    private RaycastHit raycastHit;
    private Func<bool> detectHit;

    public void Construct(float speed)
    {
        if (radius > Mathf.Epsilon)
            detectHit = SpherecastHit;
        else
            detectHit = RaycastHit;

        rigidbody.velocity = transform.forward * speed;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0)
            gameObject.SetActive(false); //TODO: add pool object

        if (detectHit())
            RegisterCollision(raycastHit.collider);
    }

    protected virtual void RegisterCollision(Collider collider)
    {
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
