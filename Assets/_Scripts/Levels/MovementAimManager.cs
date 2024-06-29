using UnityEngine;

public class MovementAimManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.instance.gameState = GameState.Challenge;
    }

    private void Start()
    {
        GameManager.instance.DisableCursor();
    }
}
