using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBornEnemy : Enemy
{
    [SerializeField] GameObject[] children;
    Transform[] childrenInitPos = new Transform[3];

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
            item.SetActive(false);
            item.transform.parent = transform;
        }
    }
}
