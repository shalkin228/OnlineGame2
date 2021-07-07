using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDigZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        Digging.instance.canDig = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Digging.instance.canDig = false;
    }
}
