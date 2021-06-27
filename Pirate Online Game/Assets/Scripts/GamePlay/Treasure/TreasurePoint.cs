using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasurePoint : MonoBehaviour
{
    public List<Transform> collidingPlayers = new List<Transform>();

    [SerializeField] private GameObject Hole;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Other Player" || collision.tag == "Player")
        {
            collidingPlayers.Add(collision.transform);
            collision.GetComponent<Digging>().canDig = true;
            collision.GetComponent<Digging>().digHole = Hole;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Other Player" || collision.tag == "Player")
        {
            collidingPlayers.Remove(collision.transform);
            collision.GetComponent<Digging>().canDig = false;
        }
    }
}
