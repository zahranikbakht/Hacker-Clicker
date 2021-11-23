using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMissionChoiceInf : MonoBehaviour
{
    public Animator MissionSelector;
    public Sprite InfIcon;
    GameObject LocationLock;
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
        string country = GameManagerScript.Instance.Countries[GameManagerScript.Instance.CurrentCountry];
        sound.PlayOneShot(clip1);
        if (GameManagerScript.Instance.CurrentMission == 4)
        {
            if (GameManagerScript.Instance.Missions[country][GameManagerScript.Instance.CurrentMission].InfReward <= GameManagerScript.Instance.CurrentInfluence)
            {
                LocationLock = GameManagerScript.Instance.CountriesLocks[GameManagerScript.Instance.CurrentCountry];
                MissionSelector.SetBool("closing", true);
                GameManagerScript.Instance.EmptyMailbox();
                LocationLock.GetComponent<Image>().sprite = InfIcon;
                GameManagerScript.Instance.ReduceInfluence(GameManagerScript.Instance.Missions[country][GameManagerScript.Instance.CurrentMission].InfReward);
                LocationLock.GetComponent<WorldLocations>().WorldState += 2;
                GameManagerScript.Instance.CurrentCountry += 1;
            }
        }
        else
        {
            GameManagerScript.Instance.Missions[country][GameManagerScript.Instance.CurrentMission].RepSelected = false;
            GameManagerScript.Instance.MissionName.text = GameManagerScript.Instance.Missions[country][GameManagerScript.Instance.CurrentMission].Name;
            GameManagerScript.Instance.MissionReward.text = "REWARD: " + GameManagerScript.BigIntegerDisplay(GameManagerScript.Instance.Missions[country][GameManagerScript.Instance.CurrentMission].InfReward) + " Inf";
            GameManagerScript.Instance.MissionProgressDisplay.text = "Progress: " + GameManagerScript.BigIntegerDisplay(GameManagerScript.Instance.CumMissionProgress) + "/" + GameManagerScript.BigIntegerDisplay(GameManagerScript.Instance.Missions[country][GameManagerScript.Instance.CurrentMission].Cost);
            MissionSelector.SetBool("closing", true);
            GameManagerScript.Instance.EmptyMailbox();
        }
    }
}
