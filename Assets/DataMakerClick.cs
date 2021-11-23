using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;

public class DataMakerClick : MonoBehaviour
{
    public long InitialCost = 0;
    public BigInteger NumClicked = 0;
    public float Multiplier = 1.5f;
    public GameObject CostLabel;
    public GameObject DisabledShade;
    public GameObject Flash;
    TextMeshProUGUI CostText;
    public BigInteger CurrentCost = 0;
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
        ItemName = this.gameObject.name;
        Load();
        CostText.text = GameManagerScript.BigIntegerDisplay(CurrentCost) + " Data";
        Amount.text = GameManagerScript.BigIntegerDisplay(NumClicked);
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
            sound.PlayOneShot(clip1);
            GameManagerScript.Instance.ReduceData(CurrentCost);
            GameManagerScript.Instance.MalwareNumber[ItemName] += 1;
            this.NumClicked += 1;
            CurrentCost = new BigInteger(((double)CurrentCost * Multiplier));
            CostText.text = GameManagerScript.BigIntegerDisplay(CurrentCost) + " Data";
            Amount.text = GameManagerScript.BigIntegerDisplay(NumClicked);
        }
    }

    public void TaskOnHover()
    {
        AnnouncementText.text = "This "+ ItemName + " obtains "+ GameManagerScript.BigIntegerDisplay(GameManagerScript.Instance.MalwarePower[ItemName]) + " data from victims every 2 seconds.";
    }
    public void TaskOnExitHover()
    {
        AnnouncementText.text = "";
    }
    public void Load()
    {
        string loaded;
        loaded = PlayerPrefs.GetString(this.gameObject.name + "Cost", "N/A");
        if (loaded.Equals("N/A"))
            return;
        this.CurrentCost = BigInteger.Parse(loaded);
        loaded = PlayerPrefs.GetString(this.gameObject.name);
        this.NumClicked = BigInteger.Parse(loaded.Split(',')[0]);
    }
}
