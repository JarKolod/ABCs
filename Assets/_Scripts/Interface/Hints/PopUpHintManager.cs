using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace popuphints
{
    public class PopUpHintManager : MonoBehaviour
    {
        public static PopUpHintManager instance { get; private set; }

        [SerializeField] GameObject onLevelStartHint;

        GameObject hintBeingDisplayed;

        private void OnEnable()
        {
            CheckSingleton();
        }

        private void Start()
        {
            // coinRotation Script breaks if not for 0.1s buffer i dont know why
            // not inilizaed maybe but it should wait
            Invoke("DisplayHintOnStart", 0.1f); 
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

        private void DisplayHintOnStart()
        {
            if (onLevelStartHint)
            {
                GameManager.instance.DisplayHint(onLevelStartHint);
            }
            else
            {
                Debug.Log("No Hint on Start up");
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
