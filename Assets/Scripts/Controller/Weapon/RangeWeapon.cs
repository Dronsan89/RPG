using UnityEngine;

public abstract class RangeWeapon : Weapon
{
    [SerializeField] protected Transform fireFrom;

    public override WeaponType Type => WeaponType.Range;

    public float Length => weaponLength;

    public int Shots
    {
        get => Shots;
        private set => Mathf.Clamp(value, 0, maxShots);
    }

    [SerializeField] float weaponLength;
    [SerializeField] int maxShots;

    protected Player player;

    public virtual void Construct(Player p)
    {
        player = p;
    }

    public bool AddAmmo(int shots)
    {
        if (Shots == maxShots)
            return false;
        else
        {
            Shots += shots;
            return true;
        }
    }

    public override void Update()
    {
        /*if (Shots == 0)
            IsActive = false;*/

        if (IsActive)
        {
            base.Update();
        }
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + transform.TransformDirection(new Vector3(0, Length, 0)), 0.1f);
        }
    }
#endif
}
