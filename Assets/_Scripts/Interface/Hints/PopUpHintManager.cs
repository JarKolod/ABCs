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

        public bool isHintBeingDisplayed()
        {
            return hintBeingDisplayed != null;
        }
    }
}
