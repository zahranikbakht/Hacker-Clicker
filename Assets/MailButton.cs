using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MailButton : MonoBehaviour
{
    public Text Name;
    public Text Description;
    public Text Reputation_Choice;
    public Text Influence_Choice;
    public Text Influece_Reward;
    public Text Reputation_Reward;
    public Text DataCost;
    public Text InfluenceCost;
    public Text ReputationCost;
    public GameObject RepLock;
    public GameObject InfLock;
    public Button RepButton;
    public Button InfButton;

    public Animator MissionSelector;
    public TextMeshProUGUI AnnouncementText;
    AudioSource sound;
    public AudioClip clip1;
    // Start is called before the first frame update
    void Start()
    {
        sound = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TaskOnClick()
    {

        if (GameManagerScript.Instance.NewMissionInMailBox == true)
        {
            MissionSelector.SetBool("Opening", true);
            MissionSelector.SetBool("closing", false);
            if (GameManagerScript.Instance.CurrentMission == 4)
            {
                CreateFinale();
            }
            else
            {
                CreateMission();
            }
            
        }
    }

    public void TaskOnHover()
    {
        sound.PlayOneShot(clip1);
        if (GameManagerScript.Instance.NewMissionInMailBox == true)
        {
            AnnouncementText.text = "You have 1 new message!";
        }
        else
        {
            AnnouncementText.text = "You have no new messages.";
        }
    }
    public void OnHoverExit()
    {
        AnnouncementText.text = "";
    }

    public void CreateMission()
    {
        string country = GameManagerScript.Instance.Countries[GameManagerScript.Instance.CurrentCountry];
        Mission CurrentMission = GameManagerScript.Instance.Missions[country][GameManagerScript.Instance.CurrentMission];
        Name.text = "Mission " + (GameManagerScript.Instance.CurrentMission + 1).ToString() + "/4:" + CurrentMission.Name;
        Description.text = CurrentMission.Description;
        Reputation_Choice.text = CurrentMission.RepChoice;
        Influence_Choice.text = CurrentMission.InfChoice;
        Influece_Reward.text = "REWARD: " + GameManagerScript.BigIntegerDisplay(CurrentMission.InfReward) + " Influence";
        Reputation_Reward.text = "REWARD: " + GameManagerScript.BigIntegerDisplay(CurrentMission.RepReward) + " Reputation";
        DataCost.text = "Earn " + GameManagerScript.BigIntegerDisplay(CurrentMission.Cost) + " DATA to complete";
        InfluenceCost.text = "LOSS: " + CurrentMission.CostPercent.ToString()+"% OF INFLUENCE";
        ReputationCost.text =  "LOSS: " + CurrentMission.CostPercent.ToString() + "% OF Reputation";
        RepLock.SetActive(false);
        InfLock.SetActive(false);
        RepButton.interactable = true;
        InfButton.interactable = true;
    }

    public void CreateFinale()
    {
        string country = GameManagerScript.Instance.Countries[GameManagerScript.Instance.CurrentCountry];
        Mission CurrentMission = GameManagerScript.Instance.Missions[country][GameManagerScript.Instance.CurrentMission];
        Name.text = "Network Established -- Choose Their Fate";
        Description.text = CurrentMission.Description;
        Reputation_Choice.text = CurrentMission.RepChoice;
        Influence_Choice.text = CurrentMission.InfChoice;
        Influece_Reward.text = "COST: " + GameManagerScript.BigIntegerDisplay(CurrentMission.InfReward) + " Influence";
        Reputation_Reward.text = "COST: " + GameManagerScript.BigIntegerDisplay(CurrentMission.RepReward) + " Reputation";
        if (GameManagerScript.Instance.Missions[country][GameManagerScript.Instance.CurrentMission].RepReward > GameManagerScript.Instance.CurrentReputation)
        {
            RepLock.SetActive(true);
            RepButton.interactable = false;
        }
        else
        {
            RepLock.SetActive(false);
            RepButton.interactable = true;
        }
        if (GameManagerScript.Instance.Missions[country][GameManagerScript.Instance.CurrentMission].InfReward > GameManagerScript.Instance.CurrentInfluence)
        {
            InfLock.SetActive(true);
            InfButton.interactable = false;
        }
        else
        {
            InfLock.SetActive(false);
            InfButton.interactable = true;
        }
        DataCost.text = "";
        InfluenceCost.text = "";
        ReputationCost.text = "";
    }
}
