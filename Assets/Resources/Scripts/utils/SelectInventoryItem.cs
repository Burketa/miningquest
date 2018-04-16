﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectInventoryItem : MonoBehaviour
{
    private GameObject selected;
    private InventoryScript inventoryScript;

    public void Start()
    {
        inventoryScript = FindObjectOfType<InventoryScript>();
        selected = inventoryScript.selectedButton;
    }

    public void OnClick()
    {
        inventoryScript.ChangeSelectedTo(gameObject);
        inventoryScript.UpdateDescription(gameObject);
    }
}
