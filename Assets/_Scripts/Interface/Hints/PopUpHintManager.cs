using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace popuphints
{
    public class PopUpHintManager : MonoBehaviour
    {
        public static PopUpHintManager instance { get; private set; }

        GameManager gameManager;
        GameObject hintBeingDisplayed;
        [SerializeField] GameObject onLevelStartHint;

        private void Awake()
        {
            gameManager = GameManager.instance;
        }

        private void OnEnable()
        {
            CheckSingleton();
        }

        private void Start()
        {
            if(onLevelStartHint)
            {
                GameManager.instance.DisplayHint(onLevelStartHint);
            }
            else
            {
                Debug.Log("No Hint on Start up");
            }
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

        public void InstantiatePopUpHint(GameObject hintPrefab)
        {
            if (hintBeingDisplayed == null)
            {
                hintBeingDisplayed = Instantiate(hintPrefab);
            }
        }

        public void OnPopUpExitBtnClick()
        {
            GameManager.instance.OnHintDestroy();
            hintBeingDisplayed = null;
        }
    }
}
