using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultButton : MonoBehaviour
{
    [SerializeField] Button _resultButton;
    [SerializeField] string _sceneName;
    void Start()
    {
        _resultButton.onClick.AddListener(SceneChange);
    }

    void SceneChange()
    {
        SceneManager.LoadScene(_sceneName);
    }
    
}
