using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutItem : MonoBehaviour
{
    public List<Sprite> ItemList = new List<Sprite>();
    public Image Item;

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        Item.sprite = ItemList[Random.Range(0, 377)];
    }
}
