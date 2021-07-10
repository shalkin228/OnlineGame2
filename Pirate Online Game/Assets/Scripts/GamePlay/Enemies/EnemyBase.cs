using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [HideInInspector] public int currentHp = 0;

    [SerializeField] private int maxHp;
    [SerializeField] private float maxTargetingPosition;
    [SerializeField] private Transform hitPos;
    [SerializeField] private float hitRadius;

    private NavMeshAgent agent;
    private Animator anim;
    private PhotonView pv;
    private bool isHitting = false;

    private void Start()
    {
        currentHp = maxHp;

        anim = GetComponent<Animator>();

        pv = GetComponent<PhotonView>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (isHitting)
                return;

            List<Transform> players = new List<Transform>();

            if (FindObjectsOfType<PlayerMovement>() == null)
                return;

            foreach (var player in FindObjectsOfType<PlayerMovement>())
            {
                players.Add(player.transform);
            }

            float minDistance = Vector3.Distance(players[0].position, transform.position);
            int minDistancePlayerIndex = 0;
            int currentIterationIndex = 0;
            foreach (var player in players)
            {
                if (Vector3.Distance(player.position, transform.position) < minDistance)
                {
                    minDistance = Vector3.Distance(player.position, transform.position);
                    minDistancePlayerIndex = currentIterationIndex;
                }
                currentIterationIndex++;
            }

            if (Vector3.Distance(players[minDistancePlayerIndex].position, transform.position) > maxTargetingPosition)
            {
                agent.isStopped = true;

            }
            else
            {
                agent.isStopped = false;
            }


            agent.SetDestination(players[minDistancePlayerIndex].position);

            Vector3 playerLocalPos = transform.InverseTransformPoint(players[minDistancePlayerIndex].position);
            if (playerLocalPos.x >= 0)
            {
                Flip(Direction.Right);
            }
            else
            {
                Flip(Direction.Left);
            }

            if (agent.velocity != Vector3.zero)
            {
                anim.SetBool("IsRunning", true);
            }
            else
            {
                anim.SetBool("IsRunning", false);            
            }

            if (Vector3.Distance(transform.position, players[minDistancePlayerIndex].position) <= 2)
            {
                TryAttackPlayer(players[minDistancePlayerIndex].position);
            }
        }

    }

    private void Flip(Direction dir)
    {
        if(dir == Direction.Left)
        {
            foreach (Transform child in transform.GetChild(0))
            {
                child.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else
        {
            foreach (Transform child in transform.GetChild(0))
            {
                child.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

      private void TryAttackPlayer(Vector3 playerPos)
      {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(hitPos.position, hitRadius);

        foreach(Collider2D player in hitPlayer)
        {
            if (player.tag == "Other Player" || player.tag == "Player")
            {
                pv.RPC("NetworkAttackPlayer", RpcTarget.All);
            }
        }
      }

    [PunRPC]
    public void NetworkAttackPlayer()
    {
        anim.SetTrigger("Hit");
        agent.isStopped = true;
        isHitting = true;
    }
}
public enum Direction { Right, Left }
