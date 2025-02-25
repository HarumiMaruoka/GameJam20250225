using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EffectSettings : MonoBehaviour
{
    public void CreateEffect(string effectName,GameObject parent,float destroyTime)
    {
        var effectPrefab = Resources.Load<ParticleSystem>($"{effectName}");
        var effect = Instantiate(effectPrefab,parent.transform);
        Destroy(effect.gameObject,destroyTime);
    }

    private async UniTask CreateEffectAsync(string effectName, CancellationTokenSource cts, GameObject parent)
    {
        if (cts.IsCancellationRequested)
        {
            Debug.LogWarning("Effectがキャンセルされました");
            return;
        }
        
        var effectPrefab = Resources.Load<ParticleSystem>($"{effectName}");
        Instantiate(effectPrefab,parent.transform);
        
        await UniTask.Delay((int)effectPrefab.main.duration * 1000);
        
        Destroy(effectPrefab);
    }
}
