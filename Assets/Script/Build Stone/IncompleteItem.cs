using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IncompleteItem : IEnumerable<GameObject>
{
    public GameObject[] incomplete;

    public IEnumerator<GameObject> GetEnumerator()
    {
        return ((IEnumerable<GameObject>)incomplete).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return incomplete.GetEnumerator();
    }
}
