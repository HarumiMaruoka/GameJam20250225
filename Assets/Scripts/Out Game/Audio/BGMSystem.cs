using Confront.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMSystem : MonoBehaviour
{
    [SerializeField] AudioClip titleBGM;
    [SerializeField] AudioClip inGameBGM;
    [SerializeField] AudioClip resultBGM;

    private const int _fadeLoop = 100;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(this);
        AudioManager.PlayBGM(titleBGM,1f);
        SceneManager.activeSceneChanged += ActiveSceneChanged;
    }

    void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        switch (nextScene.name)
        {
            case "Result":
                AudioManager.PlayBGM(resultBGM);
                break;
            case "Title":
                AudioManager.PlayBGM(titleBGM);
                break;
            case "In Game":
                AudioManager.PlayBGM(inGameBGM);
                break;
        }
    }
}
