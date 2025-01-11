using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ItemDescriptionUI : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI descriptionTextName;

    public GameObject panel;
    
    [TextArea]
    public string itemName;
    [TextArea]
    public string itemDescription;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (descriptionText != null)
            {
                descriptionTextName.text = itemName;
                descriptionText.text = itemDescription;
                descriptionText.enabled = true;
                panel.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (descriptionText != null)
            {
                descriptionText.enabled = false;
                descriptionText.text = "";
                panel.SetActive(false);
            }
        }
    }
}
