using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeMove : MonoBehaviour
{

    public MonoBehaviour snapTurnMovement;
    public MonoBehaviour continuousMove;

    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            snapTurnMovement.enabled = true;
            continuousMove.enabled = false;
        }
        if (val == 1)
        {
            snapTurnMovement.enabled = false;
            continuousMove.enabled = true;
        }
    }
}
