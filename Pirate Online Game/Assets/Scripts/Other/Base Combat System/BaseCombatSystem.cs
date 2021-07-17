using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BaseCombatSystem : MonoBehaviour
{
    [SerializeField] protected List<CombatHit> combatHits = new List<CombatHit>();
    [SerializeField] protected Transform hitPos;
    [SerializeField] protected bool isPlayer;
    [SerializeField] protected float hitRadius;
    [SerializeField] protected float delayContinueCombo, delayContinueHit;

    protected bool canHit = true;

    protected int currentComboState = 0;

    protected PhotonView pv;

    public virtual void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    public virtual void ComboHit()
    {
        if (!canHit)
            return;

        pv.RPC("NetworkStartHit", RpcTarget.All, currentComboState);
    }

    [PunRPC]
    public void NetworkStartHit(int hitNumber)
    {
        StartHit(hitNumber);
    }

    public virtual void StartHit(int hitNumber)
    {

    }

    protected virtual IEnumerator ComboStateCoolDown()
    {
        float oldComboState = currentComboState;

        yield return new WaitForSeconds(delayContinueCombo);

        if(oldComboState == currentComboState)
        {
            currentComboState = 0;
        }
    }

    protected virtual IEnumerator HitCoolDown()
    {
        canHit = false;

        Debug.Log(1);

        yield return new WaitForSeconds(delayContinueHit);

        print(2);

        canHit = true;
    }


    private void OnDrawGizmosSelected()
    {
        if (hitPos == null)
            return;

        Gizmos.DrawWireSphere(hitPos.position, hitRadius);
    }
}
