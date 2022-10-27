using UnityEngine;

[System.Serializable]
public class DamageSource
{
    [SerializeField] private DamageType type;
    [SerializeField] private int damage;
    [SerializeField, Range(0, 1)] private float critChance;
    [SerializeField, Range(0, 1)] private float critDamage;

    public DamageType Type { get => type; set => type = value; }
    public int Damage { get => damage; set => damage = value; }
    public float CritChance { get => critChance; set => critChance = value; }
    public float CritDamage { get => critDamage; set => critDamage = value; }
    public GameObject Emitter { get; private set; }
    public GameObject Receiver { get; private set; }

    public void AddCrit(float critChance, float critDamage)
    {
        CritChance += critChance;
        CritDamage += critDamage;
    }

    public void DealDamage(GameObject emitter, Health health)
    {
        bool isCritical = Random.value < CritChance;
        int dmgBonus = 0;
        if (isCritical)
            dmgBonus += (int)(Damage * CritDamage);

        Damage += dmgBonus;
        Emitter = emitter;
        Receiver = health.gameObject;

        health.React(this);
    }
}
