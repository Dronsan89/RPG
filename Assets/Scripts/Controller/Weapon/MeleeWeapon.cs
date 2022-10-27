using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private float range;
    [SerializeField] private LayerMask enemyLayer;

    public override WeaponType Type => WeaponType.Melee;
    public float Range => range;

    private Player player;
    private Collider playerCollider;
    readonly Collider[] enemies = new Collider[10];

    public void Construct(Player p)
    {
        player = p;
        playerCollider = p.GetComponent<Collider>();
    }

    public override void Update()
    {
        if (!IsActive)
            return;

        bool hasValidEnemies = TryGetClosestEnemy(player.Position, out Health enemy);
        Enemy = enemy;

        if (!hasValidEnemies)
            return;

        base.Update();
    }

    private bool TryGetClosestEnemy(Vector3 pos, out Health closestEnemy)
    {
        int enemiesInRange = Physics.OverlapSphereNonAlloc(
            player.Position, range, enemies, enemyLayer.value, QueryTriggerInteraction.Ignore);

        float minDstSqr = float.MaxValue;
        closestEnemy = null;
        for (int i = 0; i < enemiesInRange; i++)
        {
            if (!(enemies[i] != playerCollider
                && enemies[i].TryGetComponent(out Health enemy)
                && enemy.CanBeDamagedBy(player.gameObject)))
                continue;

            float distSqr = (pos - enemy.transform.position).sqrMagnitude;
            if (distSqr < minDstSqr & !enemy.Dead)
            {
                closestEnemy = enemy;
                minDstSqr = distSqr;
            }
        }
        return closestEnemy != null;
    }
    public override void MakeDamage()
    {
        damageSource.DealDamage(player.gameObject, Enemy);
    }

    private void Loading()
    {

    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
#endif
}