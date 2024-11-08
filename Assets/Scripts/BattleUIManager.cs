using DG.Tweening;
using TMPro;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text resText;
    [SerializeField] TMP_Text resUpdText;
    [SerializeField] TMP_Text waveText;

    float lastUpdateRes = 0f;

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

        resUpdText.rectTransform.position = new Vector2(246, resUpdText.rectTransform.position.y);
        resUpdText.gameObject.SetActive(false);
        DOTween.Kill(resUpdText);

        if (lastUpdateRes > 0f)
        {
            resUpdText.SetText($"+ {Mathf.Abs(lastUpdateRes - value):F0}");
            resUpdText.gameObject.SetActive(true);
            resUpdText.rectTransform.DOMoveX(360f, 1f).OnComplete(() =>
            {
                resUpdText.rectTransform.position = new Vector2(246, resUpdText.rectTransform.position.y);
                resUpdText.gameObject.SetActive(false);
            });
        }

        lastUpdateRes = value;
    }

    void CheckWaveIndex(int value)
    {
        waveText.SetText($"Wave: {value + 1}");
    }
}
