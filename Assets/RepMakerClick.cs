using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RepMakerClick : MonoBehaviour
{
    public long InitialCost = 0;
    public long InitialRepCost = 0;
    public long NumClicked = 0;
    public float Multiplier = 1.5f;
    public GameObject CostLabel;
    public GameObject DisabledShade;
    public GameObject Flash;
    TextMeshProUGUI CostText;
    public TextMeshProUGUI RepCostText;
    long CurrentCost = 0;
    long CurrentRepCost = 0;
    Button ButtonComponent;
    string ItemName;
    public TextMeshProUGUI AnnouncementText;
    // Start is called before the first frame update
    void Start()
    {
        CostText = CostLabel.GetComponent<TextMeshProUGUI>();
        ButtonComponent = this.GetComponent<Button>();
        CurrentCost = InitialCost;
        CurrentRepCost = InitialRepCost;
        ItemName = this.gameObject.name;
        CostText.text = (InitialCost).ToString() + " Data";
        RepCostText.text = (InitialRepCost).ToString() + " REP";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.Instance.CurrentData >= CurrentCost && GameManagerScript.Instance.CurrentInfluence >= CurrentRepCost)
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
        if (GameManagerScript.Instance.CurrentData >= CurrentCost && GameManagerScript.Instance.CurrentInfluence >= CurrentRepCost)
        {
            GameManagerScript.Instance.ReduceData(CurrentCost);
            this.NumClicked += 1;
            CurrentCost = (int)(CurrentCost * Multiplier);
            CostText.text = (CurrentCost).ToString() + " Data";

            GameManagerScript.Instance.ReduceReputation(CurrentRepCost);
            GameManagerScript.Instance.GovNumber[ItemName] += 1;
            CurrentRepCost = (int)(CurrentRepCost * (Multiplier));
            RepCostText.text = (CurrentRepCost).ToString() + " Rep";
        }
    }

    public void TaskOnHover()
    {
        AnnouncementText.text = "1 "+ ItemName + " obtains "+ GameManagerScript.Instance.GovPower[ItemName].ToString() + " reputation every 3 seconds.";
    }
    public void TaskOnExitHover()
    {
        AnnouncementText.text = "";
    }
}
