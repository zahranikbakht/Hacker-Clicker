using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMissionChoiceRep : MonoBehaviour
{
    public Animator MissionSelector;
    public Sprite RepIcon;
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
            if (GameManagerScript.Instance.Missions[country][GameManagerScript.Instance.CurrentMission].RepReward <= GameManagerScript.Instance.CurrentReputation)
            {
                LocationLock = GameManagerScript.Instance.CountriesLocks[GameManagerScript.Instance.CurrentCountry];
                MissionSelector.SetBool("closing", true);
                GameManagerScript.Instance.EmptyMailbox();
                LocationLock.GetComponent<Image>().sprite = RepIcon;
                GameManagerScript.Instance.ReduceReputation(GameManagerScript.Instance.Missions[country][GameManagerScript.Instance.CurrentMission].RepReward);
                LocationLock.GetComponent<WorldLocations>().WorldState += 1;
                GameManagerScript.Instance.CurrentCountry += 1;

            }
        }
        else
        {
            GameManagerScript.Instance.Missions[GameManagerScript.Instance.Countries[GameManagerScript.Instance.CurrentCountry]][GameManagerScript.Instance.CurrentMission].RepSelected = true;
            GameManagerScript.Instance.MissionName.text = GameManagerScript.Instance.Missions[country][GameManagerScript.Instance.CurrentMission].Name;
            GameManagerScript.Instance.MissionReward.text = "REWARD: " +GameManagerScript.BigIntegerDisplay(GameManagerScript.Instance.Missions[country][GameManagerScript.Instance.CurrentMission].RepReward) + " Rep";
            GameManagerScript.Instance.MissionProgressDisplay.text = "Progress: " + GameManagerScript.BigIntegerDisplay(GameManagerScript.Instance.CumMissionProgress) + "/" + GameManagerScript.BigIntegerDisplay(GameManagerScript.Instance.Missions[country][GameManagerScript.Instance.CurrentMission].Cost);
            MissionSelector.SetBool("closing", true);
            GameManagerScript.Instance.EmptyMailbox();
        }
    }
}
