using UnityEngine;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] AllScenes _nextScene;
    private void Start()
    {
        BAHMANLoadingManager._INSTANCE._LoadScene(_nextScene);
    }


}
