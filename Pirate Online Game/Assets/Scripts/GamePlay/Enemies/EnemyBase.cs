using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [HideInInspector] public int currentHp = 0;

    [SerializeField] private int maxHp;

    private NavMeshAgent agent;
    private PhotonView photonView;
    private Animator anim;

    void Start()
    {
        currentHp = maxHp;

        photonView = GetComponent<PhotonView>();

        anim = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
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

            if (Vector3.Distance(players[minDistancePlayerIndex].position, transform.position) > 50)
            {
                agent.isStopped = true;

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
        }

    }

    private void Flip(Direction dir)
    {
        if(dir == Direction.Left)
        {
            foreach(Transform child in transform.GetChild(0))
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

}
public enum Direction { Right, Left }
