using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EffectSettings : MonoBehaviour
{
    [SerializeField] private float _destroyTime;
    void Start()
    {
        CreateEffect("Eff_Confetti",gameObject);
    }

    public void CreateEffect(string effectName,GameObject parent)
    {
        var effectPrefab = Resources.Load<ParticleSystem>($"{effectName}");
        var effect = Instantiate(effectPrefab,parent.transform);
        Destroy(effect.gameObject,_destroyTime);
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
