using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBornEnemy : Enemy
{
    [SerializeField] GameObject[] children;

    private void Start()
    {
        SetMaxHealth();
        onDied += HideChildren;
        Invoke(nameof(InitChildren), 3f);
    }

    private void OnDestroy()
    {
        onDied -= HideChildren;
    }

    void InitChildren()
    {
        foreach (var item in children)
        {
            item.SetActive(true);
            item.transform.parent = null;
        }
    }

    void HideChildren()
    {
        foreach (var item in children)
        {
            item.transform.parent = transform;
            item.SetActive(false);
        }
    }
}
