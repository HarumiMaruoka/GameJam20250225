using System;
using UnityEngine;

public class TimeView : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI _text;

    private void Update()
    {
        float currentTime = TimeManager.Instance.Time;

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        int milliseconds = Mathf.FloorToInt((currentTime - Mathf.Floor(currentTime)) * 100f);

        _text.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}
