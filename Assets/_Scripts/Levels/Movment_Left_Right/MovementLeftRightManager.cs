using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLeftRightManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.instance.gameState = GameState.Challenge;
    }
}
