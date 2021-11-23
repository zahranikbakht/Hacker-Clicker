using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
public class SaveMaster : MonoBehaviour
{
    public GameObject[] Malwares = new GameObject[4];
    public GameObject[] Upgrades = new GameObject[4];
    public GameObject[] DarkWeb = new GameObject[3];
    public GameObject[] Government = new GameObject[3];
    public static SaveMaster SaveInstance { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        SaveInstance = this;
    }

    public void Save()
    {
        PlayerPrefs.SetString("NumClicked",  GameManagerScript.Instance.NumClicked.ToString());
        PlayerPrefs.SetString("Data",  GameManagerScript.Instance.CurrentData.ToString());
        PlayerPrefs.SetString("Influence",  GameManagerScript.Instance.CurrentInfluence.ToString());
        PlayerPrefs.SetString("Reputation",  GameManagerScript.Instance.CurrentReputation.ToString());
        foreach (KeyValuePair<string, BigInteger> entry in  GameManagerScript.Instance.MalwareNumber)
        {
            PlayerPrefs.SetString(entry.Key, entry.Value.ToString() + "," +  GameManagerScript.Instance.MalwarePower[entry.Key].ToString());
        }
        foreach (KeyValuePair<string, int> entry in  GameManagerScript.Instance.UpgradeNumber)
        {
            PlayerPrefs.SetString(entry.Key, entry.Value.ToString() + "," +  GameManagerScript.Instance.UpgradePower[entry.Key].ToString());
        }
        foreach (KeyValuePair<string, BigInteger> entry in  GameManagerScript.Instance.DarkWebNumber)
        {
            PlayerPrefs.SetString(entry.Key, entry.Value.ToString() + "," +  GameManagerScript.Instance.DarkWebPower[entry.Key].ToString());
        }
        foreach (KeyValuePair<string, BigInteger> entry in  GameManagerScript.Instance.GovNumber)
        {
            PlayerPrefs.SetString(entry.Key, entry.Value.ToString() + "," +  GameManagerScript.Instance.GovPower[entry.Key].ToString());
        }

        PlayerPrefs.SetString("CurrentMission",  GameManagerScript.Instance.CurrentMission.ToString());
        PlayerPrefs.SetString("CurrentCountry",  GameManagerScript.Instance.CurrentCountry.ToString());
        PlayerPrefs.SetString("MissionProgress",  GameManagerScript.Instance.MissionProgress.ToString());

        PlayerPrefs.SetString("CumMissionProgress",  GameManagerScript.Instance.CumMissionProgress.ToString());
        PlayerPrefs.SetString("NewMissionInMailBox",  GameManagerScript.Instance.NewMissionInMailBox.ToString());
        PlayerPrefs.SetString("MissionBuffer",  GameManagerScript.Instance.MissionBuffer.ToString());

        for (int i = 0; i <  GameManagerScript.Instance.CountriesLocks.Length; i++)
        {
            PlayerPrefs.SetString( GameManagerScript.Instance.Countries[i],  GameManagerScript.Instance.CountriesLocks[i].GetComponent<WorldLocations>().WorldState.ToString());
        }

        PlayerPrefs.SetString("ScrollbarSize",  GameManagerScript.Instance.scrollbar.size.ToString());

        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetString(Malwares[i].name + "Cost", Malwares[i].GetComponent<DataMakerClick>().CurrentCost.ToString());

            PlayerPrefs.SetString(Upgrades[i].name + "Cost", Upgrades[i].GetComponent<ClickHelper>().CurrentCost.ToString());
        }
        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.SetString(DarkWeb[i].name + "DataCost", DarkWeb[i].GetComponent<InfMakerClick>().CurrentCost.ToString());
            PlayerPrefs.SetString(DarkWeb[i].name + "InfCost", DarkWeb[i].GetComponent<InfMakerClick>().CurrentInfCost.ToString());

            PlayerPrefs.SetString(Government[i].name + "DataCost", Government[i].GetComponent<RepMakerClick>().CurrentCost.ToString());
            PlayerPrefs.SetString(Government[i].name + "RepCost", Government[i].GetComponent<RepMakerClick>().CurrentRepCost.ToString());
        }
        PlayerPrefs.SetString("MissionName", GameManagerScript.Instance.MissionName.text);
        string country = GameManagerScript.Instance.Countries[GameManagerScript.Instance.CurrentCountry];
        PlayerPrefs.SetString("RepSelected", GameManagerScript.Instance.Missions[country][GameManagerScript.Instance.CurrentMission].RepSelected.ToString());
        PlayerPrefs.SetString("CostPercent", GameManagerScript.Instance.Missions[country][GameManagerScript.Instance.CurrentMission].CostPercent.ToString());
    }
}
