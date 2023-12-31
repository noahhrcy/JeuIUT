﻿using UnityEngine;
using System.Collections;

public class RandomItem : MonoBehaviour
{
	private ElementalInventory inventory;
	private bool isInventoryOpen = false;

	void Update()
	{
		if (inventory == null)
		{
			inventory = FindObjectOfType<ElementalInventory>();
			if (inventory == null)
			{
				Debug.LogError("ElementalInventory non trouvé dans la scène.");
				return;
			}
		}

		if (Input.GetKeyDown(KeyCode.G))
		{
			inventory.addItem(SimpleMethods.randomElement(), Random.Range(1, inventory.maxStack), new Color(Random.value / 2f, Random.value / 2f, Random.value / 2f, 1f));
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			inventory.clear();
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			PlayerPrefs.SetString("EInventory", inventory.convertToString());
		}
		if (Input.GetKeyDown(KeyCode.L))
		{
			inventory.loadFromString(PlayerPrefs.GetString("EInventory"));
		}

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (inventory != null)
			{
				// Inverser l'état de l'inventaire (ouvrir si fermé, fermer si ouvert)
				isInventoryOpen = !isInventoryOpen;

				// Appliquer l'état à l'objet inventory
				if (inventory.gameObject != null)
				{
					inventory.ToggleInventoryRenderer(isInventoryOpen);
				}
				else
				{
					Debug.LogError("L'objet GameObject de l'inventaire est null.");
				}
				for (int i = 0; i < inventory.Cells.Length; i++)
				{
					if (inventory.Cells[i].elementCount > 0)
					{
						Debug.Log($"Cellule {i}: {inventory.Cells[i].elementName}, Count: {inventory.Cells[i].elementCount}, Color: {inventory.Cells[i].elementColor}");
					}
				}

				InventoryRenderer inventoryRenderer = inventory.GetComponentInChildren<InventoryRenderer>();
				if (inventoryRenderer != null)
				{
					inventoryRenderer.ToggleInventoryUI(isInventoryOpen);
				}
				else
				{
					Debug.LogError("Le composant InventoryRenderer n'est pas attaché à ElementalInventory.");
				}
			}
		}
	}
}

