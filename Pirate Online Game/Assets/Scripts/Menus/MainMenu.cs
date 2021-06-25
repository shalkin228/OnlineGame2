using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void JoinSinglePlayer()
    {
        Application.LoadLevel(1);
    }

    public void JoinMultiPlayer()
    {
        Application.LoadLevel(2);
    }
}
