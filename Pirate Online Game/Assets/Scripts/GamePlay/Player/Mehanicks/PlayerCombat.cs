using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : BaseCombatSystem
{
    private Animator anim;

    [SerializeField] float KnockTime;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //GetComponent<Rigidbody2D>().isKinematic = false;
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(2, 0), ForceMode2D.Force);
        //GetComponent<Rigidbody2D>().isKinematic = true;
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

        Debug.Log(currentComboState);

        anim.SetInteger("hitState", currentComboState + 1);
        anim.SetTrigger("Hit");

        canHit = false;
    }


    public void Hit()
    {
        Collider2D[] hittingTargets = Physics2D.OverlapCircleAll(hitPos.position, hitRadius);
        foreach (Collider2D target in hittingTargets)
        {
            if (target.gameObject.layer == 8 && PhotonNetwork.IsMasterClient)
            {
                target.GetComponent<IDamageable>().Damage(combatHits[currentComboState].Damage);

                if (combatHits[currentComboState].knockBackRage != 0)
                {
                    Vector2 direction = target.transform.position - transform.position;

                    target.attachedRigidbody.AddForce(direction.normalized * combatHits[currentComboState].knockBackRage, ForceMode2D.Impulse);

                    StartCoroutine(knockingStop(target.attachedRigidbody, combatHits[currentComboState].knockBackTime));
                }
            }
        }
    }

    public void StopHit()
    {
        currentComboState++;
        if (pv.IsMine)
        {
            StartCoroutine(ComboStateCoolDown());
            StartCoroutine(HitCoolDown());
        }
    }

    IEnumerator knockingStop(Rigidbody2D stoppingTargetsRigidbody, float knockTime)
    {
        yield return new WaitForSeconds(knockTime);

        stoppingTargetsRigidbody.velocity = Vector2.zero;

    }
}