using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Item/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea(15,20)]
    public string description;
    public Sprite thumbnail;
    public GameObject gameModel;
    public int quantity;

}
