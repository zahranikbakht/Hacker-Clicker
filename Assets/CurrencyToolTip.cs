using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyToolTip : MonoBehaviour
{
    public TextMeshProUGUI AnnouncementText;
    public string CurrencyDescription;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void OnHover()
    {
        if (this.gameObject.name.Equals("Data"))
        {
            AnnouncementText.text = CurrencyDescription;
        }
        
    }
    public void OnHoverExit()
    {
        if (this.gameObject.name.Equals("Data"))
        {
            AnnouncementText.text = "";
        }
    }
}
