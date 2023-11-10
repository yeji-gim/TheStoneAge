using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeTable : MonoBehaviour
{
    private void OnMouseDown()
    {
        UIManager.Instance.TogglemakingPanel();
    }
}
