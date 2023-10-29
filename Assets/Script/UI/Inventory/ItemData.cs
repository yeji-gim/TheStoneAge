using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Item/Item")]
public class ItemData : ScriptableObject
{
    [TextArea(15,20)]
    public string description;
    public Sprite thumbnail;
    public GameObject gameModel;
    public int quantity;

}
