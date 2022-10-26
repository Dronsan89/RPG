using UnityEngine;

public class Bow : RangeWeapon
{
    [SerializeField] private GameObject projectile;
    //[SerializeField] private Transform ropeTransform;
    [SerializeField] private AnimationCurve ropeReturnAnimation;
    //[SerializeField] private float returnTime = 0.3f;
    [SerializeField] private float speed = 20;

    public float Speed => speed;

    private float tension;
    private bool isPressed;

    //private Vector3 ropeNearPosition;
    //private Vector3 ropeFarPosition;

    public override void Construct(Player p)
    {
        base.Construct(p);
        //ropeNearPosition = ropeTransform.localPosition;
        IsActive = true;
    }

    public override void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isPressed = false;
            IsAttaking = true;
            //StartCoroutine(RopeReturn());
        }
        if (isPressed)
        {
            if (tension < FireRate)
            {
                tension += Time.deltaTime;
            }
            //ropeTransform.localPosition = Vector3.Lerp(ropeNearPosition, ropeFarPosition, tension);
        }

        base.Update();
    }

    public override void MakeDamage()
    {
        //Debug.Log(1);
        var newProjectile = Instantiate(projectile, fireFrom.position, player.ShootDirection);
        //Debug.Log("player.ShootDirection: " + player.ShootDirection);
        //Debug.Log("speed bow1: " + Speed);
        //Debug.Log("tension: " + tension);
        //Debug.Log("tension*Speed: " + tension * Speed);
        newProjectile.GetComponent<Projectile>().Construct(Speed * tension);
        newProjectile.GetComponent<Projectile>().Hit += Hit;
        tension = 0;

    }

    private void Hit(Health health) => damageSource.DealDamage(player.gameObject, health);

    /// <summary>
    /// For animation rope
    /// </summary>
    /// <returns></returns>
    /*private IEnumerator RopeReturn()
    {
        Vector3 startPosition = ropeTransform.localPosition;

        for (float i = 0; i < FireRate; i += Time.deltaTime/returnTime)
        {
            ropeTransform.localPosition = Vector3.LerpUnclamped(startPosition, ropeNearPosition, ropeReturnAnimation.Evaluate(i));
            yield return null;
        }

        ropeTransform.localPosition = ropeNearPosition;
    }*/
}
