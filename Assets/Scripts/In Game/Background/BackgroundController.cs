using System;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField]
    private GameObject _centerBG;
    [SerializeField]
    private float _width;
    [SerializeField]
    private float _height;

    private GameObject[,] _backgrounds = new GameObject[3, 3];

    private void Start()
    {
        // 8近傍に背景を生成。
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    _backgrounds[i + 1, j + 1] = _centerBG;
                    continue;
                }

                _backgrounds[i + 1, j + 1] = Instantiate(_centerBG, new Vector3(i * _width, j * _height, 0), Quaternion.identity);
            }
        }
    }

    private void Update()
    {
        // プレイヤーの位置によって背景を移動。
        Vector3 playerPos = UFOController.Instance.transform.position;
        int x = (int)(playerPos.x / _width);
        int y = (int)(playerPos.y / _height);
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                try
                {
                    _backgrounds[i + 1, j + 1].transform.position = new Vector3((x + i) * _width, (y + j) * _height, 0);
                }
                catch
                {
                    Debug.Log("");
                }
            }
        }
    }
}
