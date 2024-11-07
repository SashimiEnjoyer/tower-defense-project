using TMPro;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text resText;
    [SerializeField] TMP_Text waveText;

    private void Awake()
    {
        GameplayManager.instance.onResourcesChange += CheckResourceUpdate;
        GameplayManager.instance.onWaveChange += CheckWaveIndex;
    }

    private void OnDestroy()
    {
        GameplayManager.instance.onResourcesChange -= CheckResourceUpdate;
        GameplayManager.instance.onWaveChange -= CheckWaveIndex;
    }

    void CheckResourceUpdate(float value)
    {
        resText.SetText($"Resources: {value}");
    }

    void CheckWaveIndex(int value)
    {
        waveText.SetText($"Wave: {value + 1}");
    }
}
