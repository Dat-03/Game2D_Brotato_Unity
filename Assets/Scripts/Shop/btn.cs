using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn : MonoBehaviour
{
    // Start is called before the first frame update
    public ShopItemSO[] shopItemSO;
    public Button[] btnbuy;
    public GameObject[] btnused;
    public GameObject shop;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i < shopItemSO.Length;i++)
        {
            if (shopItemSO[i].statusBuy)
            {
                btnused[i].SetActive(true);
            } else
            {
                btnused[i].SetActive(false);
                if(shop.GetComponent<ShopManager>().coins >= shopItemSO[i].baseCast)
                {
                    btnbuy[i].interactable = true;
                } else
                {
                    btnbuy[i].interactable = false;
                }
            }
        }
    }
}
