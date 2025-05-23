using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


// 아이템을 슬롯에 반영
public class ItemSlot : MonoBehaviour
{

    public Image Icon;
    public Text CountText;
    public Text ItemName;

    private ItemData itemData;
    private int itemCount = 1;


    private void UpdateUI()
    {
        if(itemData != null)
        {
            Icon.sprite = itemData.Icon;
            ItemName.text  = itemData.itemName;

            if(itemCount > 1 )
            {
                CountText.text = itemCount.ToString();

            }
            else
            {
                CountText.text = "";
            }

        }
        else
        {
            Icon.sprite= null;
            CountText.text = "";
        }


    }

    public void SetUp(ItemData setitemData)
    {
        itemData = setitemData;
        UpdateUI();
    }
    public void AddCount(int amount)
    {
        itemCount += amount;
        UpdateUI();
    }
    public bool HasItem(ItemData checkItem)
    {
        return itemData == checkItem;
    }

    public void OnclickItem()
    {
        Player player = CharacterManager.Instance.Player;

       
        if (player != null)
        {
            itemData.Use(player);
        }

        itemCount--;
           UpdateUI();

        if (itemCount <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
