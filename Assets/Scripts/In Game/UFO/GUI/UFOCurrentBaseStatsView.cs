using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UFOCurrentBaseStatsView : MonoBehaviour
{
    [SerializeField]
    private Image _ufoImageA;
    [SerializeField]
    private Image _ufoImageB;
    [SerializeField]
    private Image _ufoImageX;
    [SerializeField]
    private Image _ufoImageY;

    [SerializeField]
    private float _animationDuration = 0.3f;

    private Image _last;

    private void Start()
    {
        var ufo = UFOController.Instance;
        ufo.OnBaseStatsChanged += OnBaseStatsChanged;
        OnBaseStatsChanged(ufo.UFOStats);
    }

    private void OnBaseStatsChanged(UFOStats stats)
    {
        if (_last) Animate(_last, 1, 0.5f);

        if (stats == UFOController.Instance.UFOStatsA) _last = _ufoImageA;
        else if (stats == UFOController.Instance.UFOStatsB) _last = _ufoImageB;
        else if (stats == UFOController.Instance.UFOStatsX) _last = _ufoImageX;
        else if (stats == UFOController.Instance.UFOStatsY) _last = _ufoImageY;

        Animate(_last, 0.5f, 1);
    }

    private Dictionary<Image, CancellationTokenSource> _ctsDic = new Dictionary<Image, CancellationTokenSource>();

    private async void Animate(Image iamge, float from, float to)
    {
        if (_ctsDic.TryGetValue(iamge, out var cts))
        {
            cts.Cancel();
            cts.Dispose();
        }

        cts = new CancellationTokenSource();
        _ctsDic[iamge] = cts;
        var token = cts.Token;

        var time = 0f;
        var startScale = iamge.transform.localScale;
        var endScale = new Vector3(to, to, to);

        while (time < _animationDuration)
        {
            if (token.IsCancellationRequested) return;

            time += Time.deltaTime;
            iamge.transform.localScale = Vector3.Lerp(startScale, endScale, time / _animationDuration);
            await UniTask.Yield();
        }

        iamge.transform.localScale = endScale;
    }
}
