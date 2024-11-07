using TMPro;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text resText;

    private void OnEnable()
    {
        GameplayManager.instance.onResourcesChange += CheckResourceUpdate;
    }

    private void OnDestroy()
    {
        GameplayManager.instance.onResourcesChange -= CheckResourceUpdate;
    }

    void CheckResourceUpdate(float value)
    {
        resText.SetText($"Resources: {value}");
    }
}
