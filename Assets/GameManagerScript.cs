using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;
public class Mission
{
    public string Description;
    public string Name;
    public string RepChoice;
    public string InfChoice;
    public string country;
    public BigInteger RepReward;
    public BigInteger InfReward;
    public BigInteger Cost;
    public bool RepSelected = true;
    public int CostPercent;
    public Mission (string desc, string name,string repC,string infC, string countryI, BigInteger InfR, BigInteger RepR, BigInteger costData)
    {
        Description = desc;
        Name = name;
        RepChoice = repC;
        InfChoice = infC;
        RepReward = RepR;
        InfReward = InfR;
        Cost = costData;
        country = countryI;
        CostPercent = Random.Range(35, 55);
    }
}
public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }

    public long NumClicked = 0;
    public BigInteger CurrentData = 0;
    public BigInteger CurrentInfluence = 0;
    public BigInteger CurrentReputation = 0;

    public Dictionary<string, int> MalwareNumber = new Dictionary<string, int>();
    public Dictionary<string, int> MalwarePower = new Dictionary<string, int>();

    public Dictionary<string, int> UpgradeNumber = new Dictionary<string, int>();
    public Dictionary<string, int> UpgradePower = new Dictionary<string, int>();

    public Dictionary<string, int> DarkWebNumber = new Dictionary<string, int>();
    public Dictionary<string, int> DarkWebPower = new Dictionary<string, int>();

    public Dictionary<string, int> GovNumber = new Dictionary<string, int>();
    public Dictionary<string, int> GovPower = new Dictionary<string, int>();

    public Dictionary<string, Mission[]> Missions = new Dictionary<string, Mission[]>();
    public int CurrentMission = 0;
    public string CurrentCountry = "Canada";
    public BigInteger MissionProgress = 0;
    public TextMeshProUGUI MissionName;
    public TextMeshProUGUI MissionReward;
    public GameObject MissionCompleteScreen;
    private Animator MissionCompleteScreenAnimator;
    public bool NewMissionInMailBox = true;

    float MissionBuffer = 0;
    public GameObject Notification;

    public GameObject Lock;

    float Timer = 0;
    public Text Data_UIText;
    public Text Influence_UIText;
    public Text Reputation_UIText;

    public Scrollbar scrollbar;
    
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        MissionCompleteScreenAnimator = MissionCompleteScreen.GetComponent<Animator>();
        MalwareNumber.Add("Virus", 0);
        MalwareNumber.Add("Trojan", 0);
        MalwareNumber.Add("Adware", 0);
        MalwareNumber.Add("Worm", 0);

        MalwarePower.Add("Virus", 1);
        MalwarePower.Add("Worm", 3);
        MalwarePower.Add("Trojan", 20);
        MalwarePower.Add("Adware", 7);

        UpgradeNumber.Add("CPU", 0);
        UpgradeNumber.Add("ssd", 0);
        UpgradeNumber.Add("power", 0);
        UpgradeNumber.Add("ubertooth", 0);

        UpgradePower.Add("CPU", 2);
        UpgradePower.Add("ssd", 3);
        UpgradePower.Add("power", 4);
        UpgradePower.Add("ubertooth", 5);

        DarkWebPower.Add("Bitcoin", 1);
        DarkWebPower.Add("Firearm", 5);
        DarkWebPower.Add("Spy", 20);

        DarkWebNumber.Add("Bitcoin", 0);
        DarkWebNumber.Add("Firearm", 0);
        DarkWebNumber.Add("Spy", 0);

        GovPower.Add("Survillance", 1);
        GovPower.Add("Assistant", 5);
        GovPower.Add("Private Jet", 20);

        GovNumber.Add("Survillance", 0);
        GovNumber.Add("Assistant", 0);
        GovNumber.Add("Private Jet", 0);

        Mission[] CanadaMissions = new Mission[5];
        CanadaMissions[0] = new Mission("Government of Quebec has recently launched an application to generate QR codes for vaccinated individuals. This app is highly vunerable and easy to exploit.", 
                                    "VaxiCode App", 
                                    "- Identify the weaknesses and inform the government of Quebec.",
                                    "- Generate false QR codes by request.", 
                                    "Canada", 
                                    500, 
                                    500, 
                                    500);

        CanadaMissions[1] = new Mission("Canada's electrity sector has recently been the target of many cyberattacks. You have found a loophole that lets you disrupt the supply of electricity throughout the country.",
                                    "BULK POWER SYSTEM",
                                    "- Send the details of the loophole to the IT anonymously",
                                    "- Cause a massive blackout in the country and demand ransom.",
                                    "Canada",
                                    2000,
                                    2000,
                                    2000);
        CanadaMissions[2] = new Mission("You have been informed that there is a high chance Minister of Foreign Affairs is involved with several criminal offenses. You decide to break into his personal computer to get solid proof.",
                            "Expose Scandal",
                            "- Assist the government to cover his corruption.",
                            "- Blackmail him for some influence over his political decisions.",
                            "Canada",
                            9000,
                            9000,
                            9000);
        CanadaMissions[3] = new Mission("You have hijacked thousands of devices across the country to provide enough computing power for a Brute Force Attack against The Bank of Canada. This will allow you to 'guess' passwords and get unauthorized access to their data.",
                            "Brute Force",
                            "- Contact the Governing Council and offer security advice.",
                            "- Breach the data on dark web.",
                            "Canada",
                            40000,
                            40000,
                            40000);

        CanadaMissions[4] = new Mission("You have established a vast network in Canada. Now you have enough power to determine the fate of this country.",
                    "Brute Force",
                    "- Make peace with the government and safegaurd the data.",
                    "- Publish all the data online and expose the government.",
                    "Canada",
                    50000,
                    50000,
                    0);
        Missions.Add("Canada", CanadaMissions);

    }

    // Update is called once per frame
    void Update()
    {
        // Data Auto-Increment
        Timer += Time.deltaTime;
        if (Timer > 3)
        {
            Timer = 0;
            foreach (string key in MalwareNumber.Keys)
            {
                AddData(MalwareNumber[key] * MalwarePower[key]);
            }
            foreach (string key in DarkWebNumber.Keys)
            {
                AddInfluence(DarkWebNumber[key] * DarkWebPower[key]);
            }
            foreach (string key in GovNumber.Keys)
            {
                AddReputation(GovNumber[key] * GovPower[key]);
            }

        }
        Data_UIText.text = CurrentData.ToString();
        Influence_UIText.text = CurrentInfluence.ToString();
        Reputation_UIText.text = CurrentReputation.ToString();

        //Mission Buffer

        if (!Notification.activeSelf && MissionName.text == "NO ACTIVE MISSION" && CurrentCountry != "USA")
        {
            if (MissionBuffer <= 0)
            {
                MissionBuffer = Random.Range(30, 90);
            }
            else
            {
                MissionBuffer -= Time.deltaTime;
                if (MissionBuffer <= 0)
                {
                    Notification.SetActive(true);
                    NewMissionInMailBox = true;
                }
            }
        }

        //Mission Progress
        if (MissionName.text != "NO ACTIVE MISSION" && !MissionCompleteScreen.activeSelf)
        {
            float progress = (float)(((double)MissionProgress) / (double)Missions[CurrentCountry][CurrentMission].Cost);
            if (progress > Mathf.Epsilon)
            {
                scrollbar.size -= progress;
                MissionProgress = 0;
                Debug.Log(MissionProgress);
            }
            if (scrollbar.size <= 0)
            {
                if (Missions[CurrentCountry][CurrentMission].RepSelected)
                {
                    AddReputation(Missions[CurrentCountry][CurrentMission].RepReward);
                    ReduceInfluence(((Missions[CurrentCountry][CurrentMission].CostPercent * CurrentInfluence)/100));
                }
                else
                {
                    AddInfluence(Missions[CurrentCountry][CurrentMission].InfReward);
                    ReduceReputation(((Missions[CurrentCountry][CurrentMission].CostPercent * CurrentReputation) / 100));
                }
                //if (CurrentMission == 3)
                //{
                //    CurrentMission = 0;
                //    CurrentCountry = "USA";
                //}
                //else
                //{
                CurrentMission += 1;
                //}
                scrollbar.size = 1;
                MissionCompleteScreen.SetActive(true);
                StartCoroutine("OnCompleteVictoryAnimation");

                //GameManagerScript.Instance.MissionName.text = GameManagerScript.Instance.Missions[GameManagerScript.Instance.CurrentCountry][GameManagerScript.Instance.CurrentMission].Name;
            }
        }

    }

    public void ReduceData(long cost)
    {
        CurrentData -= cost;
    }
    public void AddData(BigInteger cost)
    {
        CurrentData += cost;
        if (MissionName.text != "NO ACTIVE MISSION")
        {
            MissionProgress += cost;
        }
    }
    public void ReduceInfluence(BigInteger cost)
    {
        if (CurrentInfluence > cost)
        {
            CurrentInfluence -= cost;
        }
        else
        {
            CurrentInfluence = 0;
        }
    }
    public void AddInfluence(BigInteger cost)
    {
        CurrentInfluence += cost;
    }
    public void ReduceReputation(BigInteger cost)
    {
        if (CurrentReputation > cost)
        {
            CurrentReputation -= cost;
        }
        else
        {
            CurrentReputation  = 0;
        }
    }
    public void AddReputation(BigInteger cost)
    {
        CurrentReputation += cost;
    }

    public void EmptyMailbox()
    {
        NewMissionInMailBox = false;
        Notification.SetActive(false);
    }

    IEnumerator OnCompleteVictoryAnimation()
    {
        while (MissionCompleteScreenAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return null;

        MissionCompleteScreen.SetActive(false);
        MissionName.text = "NO ACTIVE MISSION";
        MissionReward.text = "";
        MissionProgress = 0;

    }

    IEnumerator OnCompleteLocationAnimation()
    {
        while (MissionCompleteScreenAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return null;

        MissionName.text = "NO ACTIVE MISSION";
        MissionCompleteScreen.SetActive(false);
        MissionReward.text = "";
        MissionProgress = 0;
        CurrentMission = 0;
        CurrentCountry = "USA";

    }
}
