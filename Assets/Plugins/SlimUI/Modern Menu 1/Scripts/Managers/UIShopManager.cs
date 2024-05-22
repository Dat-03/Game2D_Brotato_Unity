using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIShopManager : MonoBehaviour
{
    public GameObject panelSkins, panelWeapons;
    void Update()
    {
        
    }
    public void onSkins()
    {
        panelSkins.SetActive(true);
        panelWeapons.SetActive(false);
    }
    public void onWeapons()
    {
        panelWeapons.SetActive(true);
        panelSkins.SetActive(false);
    }
    public void onReturnMenu()
    {
        // return menu
        
    }
}
