using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGuideManager : MonoBehaviour
{
    public static MovementGuideManager instance { get; private set; }

    [SerializeField] GameObject HintOnObsticaleHit;

    bool hasPlayerHitObsticale = false;

    private void Awake()
    {
        CheckSingleton();
    }

    private void Start()
    {
        GameManager.instance.SetGameState(GameState.Guide);
    }

    private void CheckSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnPlayerObsticaleHit()
    {
        if(!hasPlayerHitObsticale && HintOnObsticaleHit != null)
        {
            hasPlayerHitObsticale = true;
            GameManager.instance.DisplayHint(HintOnObsticaleHit);
        }
        else
        {
            // end the tutorial? restart? just a menu i guess if he want to continue or restart or end
        }
    }
}
