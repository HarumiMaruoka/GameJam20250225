using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class StartCountDownController : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI _text;
    [SerializeField]
    private AnimationCurve _countDownCurve3;
    [SerializeField]
    private AnimationCurve _countDownCurve2;
    [SerializeField]
    private AnimationCurve _countDownCurve1;
    [SerializeField]
    private AnimationCurve _goCurve;

    public static bool IsCountingDown = false;

    private void Start()
    {
        StartCountDown();
    }

    private async void StartCountDown()
    {
        if (IsCountingDown) return;
        IsCountingDown = true;

        _text.text = "3";
        if (!await CountDown(_countDownCurve3)) return;
        _text.text = "2";
        if (!await CountDown(_countDownCurve2)) return;
        _text.text = "1";
        if (!await CountDown(_countDownCurve1)) return;
        IsCountingDown = false;
        _text.text = "GO!";
        if (!await CountDown(_goCurve)) return;
        _text.text = "";
    }

    private async UniTask<bool> CountDown(AnimationCurve goCurve)
    {
        var timer = 0.0f;
        var duration = goCurve.keys[goCurve.length - 1].time;
        while (timer < duration)
        {
            if (!this) return false;
            timer += Time.deltaTime;
            _text.transform.localScale = Vector3.one * goCurve.Evaluate(timer);
            await UniTask.Yield();
        }
        _text.transform.localScale = Vector3.one * goCurve.Evaluate(duration);
        return true;
    }
}
