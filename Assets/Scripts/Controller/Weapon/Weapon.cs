using UnityEngine;

public enum WeaponType { Range, Melee }

public abstract class Weapon : MonoBehaviour
{
    public abstract WeaponType Type { get; }

    [SerializeField] protected DamageSource damageSource;
    [SerializeField] protected float fireRate;

    public DamageType DamageType => damageSource.Type;
    public DamageSource DamageSource => damageSource;
    public int Damage => damageSource.Damage;
    public float CritChance => damageSource.CritChance;
    public float CritDamage => damageSource.CritDamage;
    public float FireRate => fireRate;
    public bool IsActive { get; set; }
    public bool IsAttaking { get; set; }
    protected Health Enemy { get; set; }

    protected float currentFireRate;

    public abstract void MakeDamage();

    public virtual void Update()
    {
        currentFireRate += Time.deltaTime;

        /*if (IsAttaking && currentFireRate > fireRate - 0.1f)
        {
            IsAttaking = false;
        }*/

        if (currentFireRate > FireRate && IsAttaking)
        {
            Debug.Log(2);
            //IsAttaking = true;
            MakeDamage();
            currentFireRate = 0;
            IsAttaking = false;
        }
    }
}
