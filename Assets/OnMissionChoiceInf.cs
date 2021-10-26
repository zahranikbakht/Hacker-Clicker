using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMissionChoiceInf : MonoBehaviour
{
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
        if (GameManagerScript.Instance.CurrentMission == 4)
        {
            if (GameManagerScript.Instance.Missions[GameManagerScript.Instance.CurrentCountry][GameManagerScript.Instance.CurrentMission].InfReward <= GameManagerScript.Instance.CurrentInfluence)
            {
                MissionSelector.SetBool("closing", true);
                GameManagerScript.Instance.EmptyMailbox();
            }
        }
        else
        {
            GameManagerScript.Instance.Missions[GameManagerScript.Instance.CurrentCountry][GameManagerScript.Instance.CurrentMission].RepSelected = false;
            GameManagerScript.Instance.MissionName.text = GameManagerScript.Instance.Missions[GameManagerScript.Instance.CurrentCountry][GameManagerScript.Instance.CurrentMission].Name;
            GameManagerScript.Instance.MissionReward.text = "REWARD: " + GameManagerScript.Instance.Missions[GameManagerScript.Instance.CurrentCountry][GameManagerScript.Instance.CurrentMission].InfReward.ToString() + " Inf";
            MissionSelector.SetBool("closing", true);
            GameManagerScript.Instance.EmptyMailbox();
        }
    }
}
