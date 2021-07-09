using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public int hp;

    private NavMeshAgent agent;
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
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

            foreach(var player in FindObjectsOfType<PlayerMovement>())
            {
                players.Add(player.transform);
            }

            float minDistance = Vector3.Distance(players[0].position, transform.position);
            int minDistancePlayerIndex = 0;                                                           
            int currentIterationIndex = 0;
            foreach(var player in players)
            {
                if(Vector3.Distance(player.position, transform.position) < minDistance)
                {
                    minDistance = Vector3.Distance(player.position, transform.position);
                    minDistancePlayerIndex = currentIterationIndex;
                }
                currentIterationIndex++;
            }

            agent.SetDestination(players[minDistancePlayerIndex].position);
        }
    }

}
