using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace popuphints
{
    public class PopUpHintManager : MonoBehaviour
    {
        public static PopUpHintManager instance { get; private set; }

        public GameObject onLevelStartHint;
        public GameObject onCollisionHint;

        GameObject hintBeingDisplayed;

        private void OnEnable()
        {
            CheckSingleton();
        }

        private void Start()
        {
            Invoke("DisplayHintOnStart", 0.5f);
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
            if (onLevelStartHint != null)
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

        public bool isHintBeingDisplayed()
        {
            return hintBeingDisplayed != null;
        }
    }
}
