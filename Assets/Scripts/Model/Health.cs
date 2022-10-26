using Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] Tags[] ignoreDamageFromTags;
    [SerializeField] protected int maxHP = 100;

    public event Action<Health> Respawned;
    public event Action<Health> Died;
    public event Action<Health> Changed;

    public bool Dead { get; private set; }
    public bool Invul { get; set; }

    public int Hp
    {
        get => hp;
        set
        {
            hp = value.MinMax(0, maxHP);
            Changed?.Invoke(this);

            if (hp == 0 && !Dead)
            {
                Dead = true;
                Died?.Invoke(this);
            }
        }
    }

    int hp;
    bool immortal;

    public bool CanBeDamagedBy(GameObject emitter)
    {
        foreach (var currentTag in ignoreDamageFromTags)
            if (emitter.CompareTag(currentTag.ToString()))
                return false;

        return true;
    }

    public virtual void React(DamageSource damageSource)
    {
        if (CanBeDamagedBy(damageSource.Emitter) || immortal)
            return;

        Hp -= damageSource.Damage;

        //Разобраться
        //Damaged?.Invoke(pDamageInstance);

        //if (DRS.Dic.TryGetValue(pDamageInstance.Type, out DamageTypeReaction dtr))
        //    CallReactions(pDamageInstance, Dead ? dtr.Reactions.Death : dtr.Reactions.Damage);
        //else
        //    CallReactions(pDamageInstance, Dead ? DRS.Fallback.Death : DRS.Fallback.Damage);

        //CallReactions(pDamageInstance, Dead ? DRS.Common.Death : DRS.Common.Damage);
    }
}
