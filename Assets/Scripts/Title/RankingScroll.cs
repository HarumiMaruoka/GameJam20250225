using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingScroll : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 10f;
    [SerializeField] private GameObject _scrollViewContent;
    private RectTransform _rect;

    private void Start()
    {
        _rect = _scrollViewContent.GetComponent<RectTransform>();
    }

    void Update()
    {
        float v = UnityEngine.Input.GetAxisRaw("Vertical");
        _rect.position += new Vector3(0, _scrollSpeed * v * -1, 0);
    }
}
