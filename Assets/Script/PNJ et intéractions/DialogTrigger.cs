using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;


public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    public TextMeshProUGUI interactMessage;
    public bool isInRange = false;
    public bool isBoss = false;
    public bool isDoor = false;

    // Liste des noms des objets à ramasser (dynamique)
    public List<string> requiredItems = new List<string>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            // Vérifier si c'est un boss et si les objets nécessaires sont dans l'inventaire
            if (isBoss && CheckRequiredItems())
            {
                StartBossFight();
            }
            else if (isDoor && !CheckRequiredItems())
            {
                TriggerDialog();
            }
            else
            {
                TriggerDialog();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            interactMessage.gameObject.SetActive(true);
            Debug.Log("Le joueur est dans la zone de dialogue");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactMessage.gameObject.SetActive(false);
            TriggerExit();
        }
    }

    public void TriggerExit()
    {
        isInRange = false;
        Debug.Log("Le joueur a quitté la zone de dialogue");
    }

    public void TriggerDialog()
    {
        // Ajout de la condition pour vérifier si l'objet est actif (visible)
        if (gameObject.activeSelf)
        {
            FindObjectOfType<DialogManager>().StartDialog(dialog);
        }
    }

    private bool CheckRequiredItems()
    {
        // Utiliser la liste de noms d'objets configurables depuis l'Inspector
        foreach (string itemName in requiredItems)
        {
            bool hasItem = ElementalInventory.Instance.contains(itemName, 1);
            Debug.Log(ElementalInventory.Instance.convertToString());
            if (!hasItem)
            {
                return false; 
            }
        }

        return true;
    }

    // Fonction pour déclencher la scène de combat avec le boss
    private void StartBossFight()
    {
        SceneManager.LoadScene(gameObject.name);
        Debug.Log("Lancement de la scène de combat avec le boss!");
    }
}
