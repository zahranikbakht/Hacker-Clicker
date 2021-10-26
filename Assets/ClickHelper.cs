using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickHelper : MonoBehaviour
{
    public long InitialCost = 0;
    public long NumClicked = 0;
    public float Multiplier = 1.5f;
    public GameObject CostLabel;
    public GameObject DisabledShade;
    public GameObject Flash;
    TextMeshProUGUI CostText;
    long CurrentCost = 0;
    Button ButtonComponent;
    string ItemName;
    public TextMeshProUGUI AnnouncementText;
    // Start is called before the first frame update
    void Start()
    {
        CostText = CostLabel.GetComponent<TextMeshProUGUI>();
        ButtonComponent = this.GetComponent<Button>();
        CurrentCost = InitialCost;
        ItemName = this.gameObject.name;
        CostText.text = (InitialCost).ToString() + " Data";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.Instance.CurrentData >= CurrentCost)
        {
            //enable the button
            ButtonComponent.interactable = true;
            DisabledShade.SetActive(false);
            Flash.SetActive(true);
        }
        else if (ButtonComponent.interactable == true)
        {
            //set the button to disabled
            Flash.SetActive(false);
            ButtonComponent.interactable = false;
            DisabledShade.SetActive(true);

        }
    }
    public void TaskOnClick()
    {
        if (GameManagerScript.Instance.CurrentData >= CurrentCost)
        {
            GameManagerScript.Instance.ReduceData(CurrentCost);
            GameManagerScript.Instance.UpgradeNumber[ItemName] += 1;
            this.NumClicked += 1;
            CurrentCost = (int)(CurrentCost * Multiplier);
            CostText.text = (CurrentCost).ToString() + " Data";
        }
    }

    public void TaskOnHover()
    {
        AnnouncementText.text = "Earn " + GameManagerScript.Instance.UpgradePower[ItemName].ToString() + "x more data per click with this upgrade.";
    }
    public void TaskOnExitHover()
    {
        AnnouncementText.text = "";
    }
}
