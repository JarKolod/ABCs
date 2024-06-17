using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class LevelLoaderOnButtonEvent : MonoBehaviour
{
    [SerializeField] private Level levelToLoad;

    public Level LevelToLoad { get => levelToLoad; set => levelToLoad = value; }

    private void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        LevelManager.instance.LoadLevel(levelToLoad);
    }

    private void OnEnable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(OnButtonClick);
    }
}
