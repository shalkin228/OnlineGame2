using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : BaseCombatSystem
{
    private Animator anim;
    private Rigidbody2D rb;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnClickHitButton()
    {
        ComboHit();
    }

    public override void Hit(int hitNumber)
    {
        base.Hit(hitNumber);
        Collider2D[] hittingTargets = Physics2D.OverlapCircleAll(hitPos.position, hitRadius);

        foreach(Collider2D target in hittingTargets)
        {
            if(target.gameObject.layer == 8)
            {
                target.GetComponent<IDamageable>().Damage(combatHits[hitNumber].Damage);

                if(combatHits[hitNumber].knockBackRage != 0)
                {
                    Vector3 direction = target.transform.position - transform.position;

                    target.attachedRigidbody.AddForce(direction.normalized * combatHits[hitNumber].knockBackRage);
                }
            }
        }
    }
}
