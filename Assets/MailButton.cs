using System.Collections;
using System.Collections.Generic;
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

    public Animator MissionSelector;
    // Start is called before the first frame update
    void Start()
    {
        
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

    public void CreateMission()
    {
        Mission CurrentMission = GameManagerScript.Instance.Missions[GameManagerScript.Instance.CurrentCountry][GameManagerScript.Instance.CurrentMission];
        Name.text = "Mission " + (GameManagerScript.Instance.CurrentMission + 1).ToString() + "/4:" + CurrentMission.Name;
        Description.text = CurrentMission.Description;
        Reputation_Choice.text = CurrentMission.RepChoice;
        Influence_Choice.text = CurrentMission.InfChoice;
        Influece_Reward.text = "REWARD: " + CurrentMission.InfReward.ToString();
        Reputation_Reward.text = "REWARD: " + CurrentMission.RepReward.ToString();
        DataCost.text = "Earn " + CurrentMission.Cost.ToString() + " DATA to complete";
        InfluenceCost.text = "LOSS: " + CurrentMission.CostPercent.ToString()+"% OF INFLUENCE";
        ReputationCost.text =  "LOSS: " + CurrentMission.CostPercent.ToString() + "% OF Reputation";
    }

    public void CreateFinale()
    {
        Mission CurrentMission = GameManagerScript.Instance.Missions[GameManagerScript.Instance.CurrentCountry][GameManagerScript.Instance.CurrentMission];
        Name.text = "Network Established -- Choose Their Fate";
        Description.text = CurrentMission.Description;
        Reputation_Choice.text = CurrentMission.RepChoice;
        Influence_Choice.text = CurrentMission.InfChoice;
        Influece_Reward.text = "COST: " + CurrentMission.InfReward.ToString() + " Inf";
        Reputation_Reward.text = "COST: " + CurrentMission.RepReward.ToString() + " Rep";

        DataCost.text = "";
        InfluenceCost.text = "";
        ReputationCost.text = "";
    }
}
