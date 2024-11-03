using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Add this for TextMeshPro

public class ItemTriggerZone : MonoBehaviour
{
    public string collectibleTag = "CollectibleItem";
    
    // Dictionary to store the names of items and their corresponding UI Images
    public Dictionary<string, Image> itemImages = new Dictionary<string, Image>();
    // Dictionary to store item names and their corresponding UI TextMeshPro components
    public Dictionary<string, TextMeshProUGUI> itemTexts = new Dictionary<string, TextMeshProUGUI>();

    private void Start()
    {
        // Manually add each item and its corresponding UI image and text (TextMeshPro)
        itemImages.Add("Apple_LP", GameObject.Find("Image_apple").GetComponent<Image>());
        itemTexts.Add("Apple_LP", GameObject.Find("Text_apple").GetComponent<TextMeshProUGUI>());

        itemImages.Add("Orange_LP", GameObject.Find("Image_orange").GetComponent<Image>());
        itemTexts.Add("Orange_LP", GameObject.Find("Text_orange").GetComponent<TextMeshProUGUI>());

        itemImages.Add("Pineapple_LP", GameObject.Find("Image_pineapple").GetComponent<Image>());
        itemTexts.Add("Pineapple_LP", GameObject.Find("Text_pineapple").GetComponent<TextMeshProUGUI>());

        // Repeat for each item/image/text pair as necessary
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(collectibleTag))
        {
            string itemName = other.gameObject.name;

            // Check if dictionaries contain the item before accessing
            if (itemImages.ContainsKey(itemName) && itemTexts.ContainsKey(itemName))
            {
                // Deactivate the image and text in the UI
                itemImages[itemName].gameObject.SetActive(false);
                itemTexts[itemName].gameObject.SetActive(false);

                // Optionally, remove the item references from dictionaries to keep them clean
                itemImages.Remove(itemName);
                itemTexts.Remove(itemName);
            }

            // Destroy the item in the game world
            Destroy(other.gameObject);
        }
    }
}
