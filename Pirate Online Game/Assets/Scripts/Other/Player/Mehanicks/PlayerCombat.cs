using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : BaseCombatSystem
{
    private Animator anim;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Debug.Log(canHit);

        if (!canHit)
        {
            GUI.instance.SetSwordSlotActive(false);
        }
        else
        {
            GUI.instance.SetSwordSlotActive(true);
        }
    }

    public override void StartHit(int hitNumber)
    {
        base.StartHit(hitNumber);

        currentComboState = hitNumber;

        anim.SetInteger("hitState", currentComboState + 1);
        anim.SetTrigger("Hit");

        canHit = false;
    }


    public void Hit()
    {
        Debug.Log("Hit" + pv.Owner.NickName);

        Collider2D[] hittingTargets = Physics2D.OverlapCircleAll(hitPos.position, hitRadius);
        foreach (Collider2D target in hittingTargets)
        {
            if (target.gameObject.layer == 8)
            {
                target.GetComponent<IDamageable>().Damage(combatHits[currentComboState].Damage);

                if (combatHits[currentComboState].knockBackRage != 0)
                {
                    Vector3 direction = target.transform.position - transform.position;

                    target.attachedRigidbody.AddForce(direction.normalized * combatHits[currentComboState].knockBackRage);
                }
            }
        }
    }


    public void StopHit()
    {
        Debug.Log("Stop Hit" + pv.Owner.NickName);

        if (pv.IsMine)
        {
            StartCoroutine(ComboStateCoolDown());
            StartCoroutine(HitCoolDown());
        }
    }
}
