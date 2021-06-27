using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TreasureIsland : MonoBehaviour
{
    public List<TreasurePoint> treasurePoints = new List<TreasurePoint>();

    [SerializeField] private Transform points;

    public static TreasureIsland instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach(Transform point in points)
        {
            treasurePoints.Add(point.GetComponent<TreasurePoint>());
        }
    }
}
