using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMissionChoiceRep : MonoBehaviour
{
    public Animator MissionSelector;
    public Sprite RepIcon;
    public GameObject LocationLock;
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
        if (GameManagerScript.Instance.CurrentMission == 4)
        {
            if (GameManagerScript.Instance.Missions[GameManagerScript.Instance.CurrentCountry][GameManagerScript.Instance.CurrentMission].RepReward <= GameManagerScript.Instance.CurrentReputation)
            {
                MissionSelector.SetBool("closing", true);
                GameManagerScript.Instance.EmptyMailbox();
                LocationLock.GetComponent<Image>().sprite = RepIcon;
                GameManagerScript.Instance.ReduceReputation(GameManagerScript.Instance.Missions[GameManagerScript.Instance.CurrentCountry][GameManagerScript.Instance.CurrentMission].RepReward);
                LocationLock.GetComponent<WorldLocations>().WorldState += 1;
                GameManagerScript.Instance.CurrentCountry = "USA";

            }
        }
        else
        {
            GameManagerScript.Instance.Missions[GameManagerScript.Instance.CurrentCountry][GameManagerScript.Instance.CurrentMission].RepSelected = true;
            GameManagerScript.Instance.MissionName.text = GameManagerScript.Instance.Missions[GameManagerScript.Instance.CurrentCountry][GameManagerScript.Instance.CurrentMission].Name;
            GameManagerScript.Instance.MissionReward.text = "REWARD: " + GameManagerScript.Instance.Missions[GameManagerScript.Instance.CurrentCountry][GameManagerScript.Instance.CurrentMission].RepReward.ToString() + " Rep";
            MissionSelector.SetBool("closing", true);
            GameManagerScript.Instance.EmptyMailbox();
        }
    }
}
