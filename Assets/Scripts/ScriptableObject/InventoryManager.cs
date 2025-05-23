using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;

    public Transform ItemSlotParent;
    public GameObject ItemSlotPrefab;
    public GameObject InventoryPannel;
    

    private bool inventoryOpen =  false;

    private List<ItemSlot> slots = new List<ItemSlot>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance =this;

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyUp(KeyCode.Tab))
        {
            inventoryOpen = !inventoryOpen;
            InventoryPannel.SetActive(inventoryOpen);
        }
        
    }



    // 아이템을 추가하는 함수

    public void AddItem(ItemData newItem)
    {

        // 동일한 아이템이 있는지 없는지 확인
        ItemSlot exitsSlot = null;
        for (int i = 0; i < slots.Count; i++)
        {
            if(slots[i] != null && slots[i].HasItem(newItem))
            {

                // 동일한 아이템
                exitsSlot = slots[i];
                break;
            }
        }

        if (exitsSlot !=null)
        {
            exitsSlot.AddCount(1);
        }
        else
        {

            // Instantiate() (생성할 오브젝트 생성될위치)
            GameObject newSlot = Instantiate(ItemSlotPrefab, ItemSlotParent);
            ItemSlot itemSlot = newSlot.GetComponent<ItemSlot>();
            itemSlot.SetUp(newItem);
            slots.Add(itemSlot);
        }
    }





}
