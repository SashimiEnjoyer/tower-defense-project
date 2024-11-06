using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text resText;

    private void OnEnable()
    {
        GameplayManager.instance.onResourcesChange += CheckResourceUpdate;
    }

    private void OnDisable()
    {
        GameplayManager.instance.onResourcesChange += CheckResourceUpdate;
    }

    void CheckResourceUpdate(float value)
    {
        resText.SetText($"Resource: {value}");
    }
}
