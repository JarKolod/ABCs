using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAimGuideManager : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.SetGameState(GameState.Guide);
    }
}
