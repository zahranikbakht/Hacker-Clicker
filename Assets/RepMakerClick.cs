using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;

public class RepMakerClick : MonoBehaviour
{
    public int InitialCost;
    public int InitialRepCost;
    public BigInteger NumClicked = 0;
    public double Multiplier = 1.5f;
    public GameObject CostLabel;
    public GameObject DisabledShade;
    public GameObject Flash;
    TextMeshProUGUI CostText;
    public TextMeshProUGUI RepCostText;
    public BigInteger CurrentCost;
    public BigInteger CurrentRepCost;
    Button ButtonComponent;
    string ItemName;
    public TextMeshProUGUI AnnouncementText;
    public TextMeshProUGUI Amount;
    AudioSource sound;
    public AudioClip clip1;
    // Start is called before the first frame update
    void Start()
    {
        sound = this.GetComponent<AudioSource>();
        CostText = CostLabel.GetComponent<TextMeshProUGUI>();
        ButtonComponent = this.GetComponent<Button>();
        CurrentCost = InitialCost;
        CurrentRepCost = InitialRepCost;
        ItemName = this.gameObject.name;
        Load();
        CostText.text = GameManagerScript.BigIntegerDisplay(CurrentCost) + " Data";
        RepCostText.text = GameManagerScript.BigIntegerDisplay(CurrentRepCost) + " REP";
        Amount.text = GameManagerScript.BigIntegerDisplay(NumClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.Instance.CurrentData >= CurrentCost && GameManagerScript.Instance.CurrentReputation >= CurrentRepCost)
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
        if (GameManagerScript.Instance.CurrentData >= CurrentCost && GameManagerScript.Instance.CurrentReputation >= CurrentRepCost)
        {
            sound.PlayOneShot(clip1);
            GameManagerScript.Instance.ReduceData(CurrentCost);
            this.NumClicked += 1;
            CurrentCost = new BigInteger((((double)CurrentCost) * (Multiplier)));
            CostText.text = GameManagerScript.BigIntegerDisplay(CurrentCost) + " Data";

            GameManagerScript.Instance.ReduceReputation(CurrentRepCost);
            GameManagerScript.Instance.GovNumber[ItemName] += 1;
            CurrentRepCost = new BigInteger(((double)CurrentRepCost * (Multiplier)));
            RepCostText.text = GameManagerScript.BigIntegerDisplay(CurrentRepCost) + " Rep";
            Amount.text = GameManagerScript.BigIntegerDisplay(NumClicked);
        }
    }

    public void TaskOnHover()
    {
        AnnouncementText.text = "1 "+ ItemName + " obtains "+ GameManagerScript.BigIntegerDisplay(GameManagerScript.Instance.GovPower[ItemName]) + " reputation every 2 seconds.";
    }
    public void TaskOnExitHover()
    {
        AnnouncementText.text = "";
    }
    public void Load()
    {
        string loaded;
        loaded = PlayerPrefs.GetString(this.gameObject.name + "DataCost", "N/A");
        if (loaded.Equals("N/A"))
            return;
        if (BigInteger.Parse(loaded) == 0)
        {
            this.CurrentCost = InitialCost;
        }
        else
        {
            this.CurrentCost = BigInteger.Parse(loaded);
        }
        loaded = PlayerPrefs.GetString(this.gameObject.name + "RepCost");
        if (BigInteger.Parse(loaded) == 0)
        {
            this.CurrentRepCost = InitialRepCost;
        }
        else
        {
            this.CurrentRepCost = BigInteger.Parse(loaded);
        }
        loaded = PlayerPrefs.GetString(this.gameObject.name);
        this.NumClicked = BigInteger.Parse(loaded.Split(',')[0]);
    }
}
