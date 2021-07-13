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

    protected int currentComboState = 0;

    private PhotonView pv;

    public virtual void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    public virtual void ComboHit()
    {
        pv.RPC("Hit", RpcTarget.All, currentComboState);
        currentComboState++;
    }

    [PunRPC]
    public virtual void Hit(int hitNumber)
    {
       
    }

}
