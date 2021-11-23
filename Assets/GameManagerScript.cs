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
    public static int Counter = 0;
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
        if (costData > 0)
        {
            Counter += 1;
        }
           
    }
}

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }
    public BigInteger NumClicked = 0;
    public BigInteger CurrentData = 0;
    public BigInteger CurrentInfluence = 0;
    public BigInteger CurrentReputation = 0;

    public Dictionary<string, BigInteger> MalwareNumber = new Dictionary<string, BigInteger>();
    public Dictionary<string, int> MalwarePower = new Dictionary<string, int>();

    public Dictionary<string, int> UpgradeNumber = new Dictionary<string, int>();
    public Dictionary<string, int> UpgradePower = new Dictionary<string, int>();

    public Dictionary<string, BigInteger> DarkWebNumber = new Dictionary<string, BigInteger>();
    public Dictionary<string, int> DarkWebPower = new Dictionary<string, int>();

    public Dictionary<string, BigInteger> GovNumber = new Dictionary<string, BigInteger>();
    public Dictionary<string, int> GovPower = new Dictionary<string, int>();

    public Dictionary<string, Mission[]> Missions = new Dictionary<string, Mission[]>();
    public int CurrentMission = 0;
    public int CurrentCountry = 0;
    public BigInteger MissionProgress = 0;
    public TextMeshProUGUI MissionName;
    public TextMeshProUGUI MissionReward;
    public TextMeshProUGUI MissionProgressDisplay;
    public BigInteger CumMissionProgress = 0;
    public GameObject MissionCompleteScreen;
    private Animator MissionCompleteScreenAnimator;
    public bool NewMissionInMailBox = true;
    public string[] Countries = new string[] { "Canada", "South Africa","Mexico", "China", "Brazil", "UK", "India", "Spain", "USA","Ukraine", "Japan","Russia","Iran","Australia"};
    public GameObject[] CountriesLocks = new GameObject[14];
    public float MissionBuffer = 0;
    public GameObject Notification;

    public GameObject Lock;

    float Timer = 0;
    public Text Data_UIText;
    public Text Influence_UIText;
    public Text Reputation_UIText;

    public Text Data_Addon;
    public Text Influence_Addon;
    public Text Reputation_Addon;

    public Scrollbar scrollbar;
    public Sprite OpenIcon;

    public TextMeshProUGUI Announcement;
    public GameObject TutorialPrompt;
    public TextMeshProUGUI CompleteLocationText;
    public GameObject CompleteLocationPrompt;
    Animator CompleteLocationPromptAnimator;
    Animator TutorialPromptAnimator;

    public static bool InWindow = false;
    bool inSuccessScreen = false;
    static string[] units = new string[333];

    public Sprite RepIcon;
    public Sprite InfIcon;
    public Sprite LockIcon;
    public Sprite GPSIcon;

    AudioSource sound;
    public AudioClip clip1;
    public AudioClip clip2;

    bool SaveFound;
    private void Awake()
    {
        units[0] = "K";
        units[1] = "M";
        units[2] = "G";
        units[3] = "T";
        units[4] = "P";
        units[5] = "Z";
        units[6] = "Y";
        units[7] = "X";
        units[8] = "V";
        units[9] = "L";
        units[10] = "A";
        units[11] = "E";
        units[12] = "C";
        units[13] = "S";
        units[13] = "U";
        for (int i = 10; i < 300; i++)
        {
            units[i] = units[i % 10] + System.Convert.ToChar(65 + (i%10)).ToString();
        }
        Instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        SaveFound = true;
        sound = this.GetComponent<AudioSource>();
        CompleteLocationPromptAnimator = CompleteLocationPrompt.GetComponent<Animator>();
        MissionCompleteScreenAnimator = MissionCompleteScreen.GetComponent<Animator>();
        TutorialPromptAnimator = TutorialPrompt.GetComponent<Animator>();

        MalwareNumber.Add("Virus", 0);
        MalwareNumber.Add("Trojan", 0);
        MalwareNumber.Add("Adware", 0);
        MalwareNumber.Add("Worm", 0);

        MalwarePower.Add("Virus", 1);
        MalwarePower.Add("Worm", 8);
        MalwarePower.Add("Trojan", 120);
        MalwarePower.Add("Adware", 40);

        UpgradeNumber.Add("CPU", 0);
        UpgradeNumber.Add("ssd", 0);
        UpgradeNumber.Add("power", 0);
        UpgradeNumber.Add("ubertooth", 0);

        UpgradePower.Add("CPU", 1);
        UpgradePower.Add("ssd", 5);
        UpgradePower.Add("power", 15);
        UpgradePower.Add("ubertooth", 28);

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

        int MissionMultiplier = 5;
        double RepInfMultiplier = 1.5f;
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
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(MissionMultiplier, (double)Mission.Counter));
        CanadaMissions[2] = new Mission("You have been informed that there is a high chance Minister of Economy is involved with several criminal offenses. You decide to break into his personal computer to get solid proof.",
                            "Expose Scandal",
                            "- Assist the government to cover his corruption.",
                            "- Blackmail him for some influence over his political decisions.",
                            "Canada",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(MissionMultiplier, (double)Mission.Counter));
        CanadaMissions[3] = new Mission("You have hijacked thousands of devices across the country to provide enough computing power for a Brute Force Attack against The Bank of Canada. This will allow you to 'guess' passwords and get unauthorized access to their data.",
                            "Brute Force",
                            "- Contact the Governing Council and offer security advice.",
                            "- Sell the data on dark web.",
                            "Canada",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(MissionMultiplier, (double)Mission.Counter));

        CanadaMissions[4] = new Mission("You have established a vast network in Canada. Now you have enough power to determine the fate of this country. Canada currently has a Corruption Perceptions Index of 77 {100 is very clean and 0 is highly corrupt}. ",
                    "Brute Force",
                    "- Make peace with the government and safeguard the data.",
                    "- Publish all the data online and expose the government.",
                    "Canada",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter+1),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter+1),
                                   0);
        Missions.Add("Canada", CanadaMissions);


        Mission[] SAMissions = new Mission[5];
        SAMissions[0] = new Mission("You have developed a Trojan that you are quite proud of. You use it to target several private businesses in South Africa and steal their data. You anticipate to earn a lot of money after selling the data.",
                                    "Starting Out",
                                    "- Attend a VIP event and meet the influencial families of South Africa.",
                                    "- Explore the black market and hire a few hackers and assasins.",
                                    "South Africa",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        SAMissions[1] = new Mission("Transnet is a South African state-owned enterprise that controls rail, port and pipeline infrastructure. A high-ranked official has allegedly taken bribes to conceal a cargo of drugs through a port. You finally find a chance to hack into their system.",
                                    "Transnet",
                                    "- Contact the official and offer to help with the concealment of the cargo.",
                                    "- Cause a major slowdown in the port operation so the cargo can be identified.",
                                    "South Africa",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        SAMissions[2] = new Mission("You have found reports showing that Bank of Baroda has issued unapproved loans to certain individuals and allowed them to move money via suspicious transactions to offshore accounts. You want to break into their system and find the actual records.",
                            "Bank of Baroda",
                            "- Assist the bank to upgrade their system security and hide the records.",
                            "- Press the bank owner to reveal the names of involved individuals.",
                            "South Africa",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        SAMissions[3] = new Mission("Ex-president of South Africa was charged with widespread corruption during his presidency and is now in prison. Following his arrest, there has been a great insurrection in the country.",
                            "Insurrection",
                            "- Ensure the IT security of the prison.",
                            "- Break into the prison security system and help him escape.",
                            "South Africa",
                            40000,
                            40000,
                            500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        SAMissions[4] = new Mission("You have established a vast network in South Africa. Now you have enough power to determine the fate of this country. South Africa currently has a Corruption Perceptions Index of 44 {100 is very clean and 0 is highly corrupt}.",
                    "Brute Force",
                    "- Make peace with the government and safeguard the data.",
                    "- Publish all the data online and expose the government.",
                    "South Africa",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                   0);
        Missions.Add("South Africa", SAMissions);


        Mission[] MexicoMissions = new Mission[5];
        MexicoMissions[0] = new Mission("You have recently taken interest in Cryptojacking: use of someone else’s computer to mine cryptocurrency. You decide to send thousands of emails containing crypto mining code. Your bitcoin wallet will be overflowing!",
                                    "Cryptojacking",
                                    "- Spend it on researches to find cyber vulnerabilities in Mexico.",
                                    "- Buy a fake identity and some tacos.",
                                    "Mexico",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        MexicoMissions[1] = new Mission("PEMEX is a large Mexican oil company. The former boss of PEMEX was recently arrested with bribery charges. He claimed that several politcians were involved in this crime. You decide to attack their system with a ransomware.",
                                    "PEMEX",
                            "- Get documents on the ex-boss's bribery and share them online.",
                            "- Demand ransome and information on the politicians involved.",
                                    "Mexico",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        MexicoMissions[2] = new Mission("You are set to perform a cyber attack on Mexico's Economy Ministry servers. You anticipate to find sensitive information, and releasing them could damage Mexico's economy and the reputation of officials.",
                            "Economy Ministry",
                            "- Agree to erase the data if officials admit to their mistakes.",
                            "- Blackmail the officials for influence over their decisions.",
                            "Mexico",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        MexicoMissions[3] = new Mission("You find a large network of cartels selling drugs on the dark web. You attempt to find their physical location. However, Cartels typically leave no trace and evidence of their work.",
                            "Cyber-Cartel",
                            "- Share the information with the police.",
                            "- Hire assasins to go to their hideout and eliminate them.",
                            "Mexico",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        MexicoMissions[4] = new Mission("You have established a vast network in Mexico. Now you have enough power to determine the fate of this country. Mexico currently has a Corruption Perceptions Index of 31 {100 is very clean and 0 is highly corrupt}.",
                    "Brute Force",
                    "- Make peace with the government and safeguard the data.",
                    "- Publish all the data online and expose the government.",
                    "Mexico",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                   0);

        Missions.Add("Mexico", MexicoMissions);


        Mission[] ChinaMissions = new Mission[5];
        ChinaMissions[0] = new Mission("Chinese New Year is approaching, and it drives up e-commerce sales. E-commerce websites keep the account information of all the users, so you decide to celebrate new year by stealing data from an e-commerce website.",
                                    "Happy New Year",
                                    "- Erase their data as an alert for them to improve their security.",
                                    "- Sell the accounts information on the dark web.",
                                    "China",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        ChinaMissions[1] = new Mission("A new series of cellphones has become popular in China. These cellphones have a hidden feature that sends the location and recorded phone calls of the owner to an unlocatable server.",
                                    "My Cellphone",
                            "- Disable the feature by shutting down the secret module.",
                            "- Leak the recorded phone calls online to tarnish the cellphone brand.",
                                    "China",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        ChinaMissions[2] = new Mission("You have heard that a group of Chinese hackers have formed a campaign against surveillance. You attempt to find their identities and locations.",
                            "Surveillance Campaign",
                            "- Share the details with the government to have them arrested.",
                            "- Request to join their campaign.",
                            "China",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        ChinaMissions[3] = new Mission("You find that the president of China is being blackmailed by someone for violating privacy rights of the citizens. Several officials are trying to this person. ",
                            "Don't Watch Me",
                            "- Cooporate with the authorities to find the blackmailer.",
                            "- Take him down and become the new blackmailer.",
                            "China",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        ChinaMissions[4] = new Mission("You have established a vast network in China. Now you have enough power to determine the fate of this country. China currently has a Corruption Perceptions Index of 42 {100 is very clean and 0 is highly corrupt}.",
                    "Brute Force",
                    "- Make peace with the government and safeguard the data.",
                    "- Publish all the data online and expose the government.",
                    "China",
                                       500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                   0);

        Missions.Add("China", ChinaMissions);


        Mission[] BrazilMissions = new Mission[5];
        BrazilMissions[0] = new Mission("Brazil is one of the most vulnerable countries to cyber attacks due to lack of investment in cyber security. You are targeting one of the largest telecommunications infrastructures in the country.",
                                    "Telecommunications Infra",
                                    "- Offer to help affected businesses improve their security.",
                                    "- Spy on affected businesses.",
                                    "Brazil",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        BrazilMissions[1] = new Mission("A company has recently announced a major security program for all of its clients, and invited hackers globally to a competition to break into its new protection system. There is a high prize for the hacker who can do this. ",
                                    "Cyber Competiton",
                            "- Join the competition and win the prize.",
                            "- Hijack the company's system and leak their source code.",
                                    "Brazil",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        BrazilMissions[2] = new Mission("You are contacted by an anonymous person who offered to give you insider access to The National Treasury systems so that you can steal it and divide the profit.",
                            "National Treasury",
                            "- Find the identity of the person and reveal it to the government.",
                            "- Accept the offer.",
                            "Brazil",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        BrazilMissions[3] = new Mission("Operation Car Wash is a large police investigation on money laundering in Brazil that involves dozens of legislators. You become aware of an upcoming attack that attempts to erase the data of this case.",
                            "Operation Car Wash",
                            "- Get the attack details and contact the police to warn them.",
                            "- Break into their system and save a copy of the data.",
                            "Brazil",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        BrazilMissions[4] = new Mission("You have established a vast network in Brazil. Now you have enough power to determine the fate of this country. Brazil currently has a Corruption Perceptions Index of 38 {100 is very clean and 0 is highly corrupt}.",
                    "Brute Force",
                    "- Make peace with the government and safeguard the data.",
                    "- Publish all the data online and expose the government.",
                    "Brazil",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                   0);

        Missions.Add("Brazil", BrazilMissions);

        Mission[] EnglandMissions = new Mission[5];
        EnglandMissions[0] = new Mission("Homesafe Security is a new company providing home cameras to citizens in order to help prevent theft and future home break-ins. They are still very new...",
             "Homesafe",
             "- Contact the company with information regarding hacker saftey.",
             "- Attach a malware on cameras to allow for remote viewing.",
             "UK",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        EnglandMissions[1] = new Mission("Security in England is very important to its government. With the large surge of immigrants wanting to come into Europe in search of sanctuary, the parliament is attempting extreme caution for who is allowed in the country.",
     "Immigration",
     "- Campaign to oppose letting in people from war-torn countries.",
     "- Campaign to promote the ideas of a larger workforce.",
     "UK",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        EnglandMissions[2] = new Mission("CCTV in London is everywhere, there is 1 camera installed for every 14 people living there. This may be an opportunity to turn a profit.",
        "CCTV",
        "- Develop an AI to auto detect criminal activity on the cameras.",
        "- Cause malfunctions in the camera and send a virus to GCHQ offices.",
        "UK",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        EnglandMissions[3] = new Mission("Bank of England recently announced that a new cryptocurrency called Binance will be used between it and its clients. Lets show them why it's a bad idea.",
        "Binance",
        "- Exploit flaws in transactions to show them how it can be stolen.",
        "- Perform a heist on clients' digital wallets during a transaction.",
        "UK",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        EnglandMissions[4] = new Mission("You have established a vast network in UK. Now you have enough power to determine the fate of this country. UK currently has a Corruption Perceptions Index of 77 {100 is very clean and 0 is highly corrupt}.",
                    "Brute Force",
                    "- Make peace with the government and safeguard the data.",
                    "- Publish all the data online and expose the government.",
                    "UK",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter+ 1),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                   0);
        Missions.Add("UK", EnglandMissions);

        Mission[] IndiaMissions = new Mission[5];
        IndiaMissions[0] = new Mission("You want to create a detailed phishing email, pretending to be from Microsoft Tech-support. You encourage the victims to download a malware and this gives you access to the financial information stored on their PC. ",
                                    "Microsoft Phishing",
                                    "- Donate money to charity from their bank accounts.",
                                    "- Sell the financial information and buy a mansion in India.",
                                    "India",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        IndiaMissions[1] = new Mission("You were recently informed that a criminal who managed to escape prison is trying to leave India by robbing a plane in an hour. Time is short and there is not much information available about the criminal.",
                                    "The Delhi Airport",
                            "- Inform the airport and assist them to find the criminal's identity.",
                            "- Distrupt the airport system to delay the flights until you identify him.",
                                    "India",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        IndiaMissions[2] = new Mission("The government has launched a new National Cybersecurity Strategy to protect the critical infrastructure, starting with the telecommunications sector. You take the challenge to hack into their system after its security has been upgraded.",
                            "National Strategy",
                            "- Help the government revise their strategy plan.",
                            "- Distrupt internet access throughout the country.",
                            "India",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        IndiaMissions[3] = new Mission("You find that the details of an upcoming secret meeting between the Foreign Affairs minister and her counterparts in Mumbai is being sold on dark web. This could be a chance for anti-nationals to plan an attack against her.",
                            "Secret Meeting",
                            "- Expose the seller and inform the minister.",
                            "- Buy the information and plant a listening device in the location.",
                            "India",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        IndiaMissions[4] = new Mission("You have established a vast network in India. Now you have enough power to determine the fate of this country. India currently has a Corruption Perceptions Index of 40 {100 is very clean and 0 is highly corrupt}.",
                    "Brute Force",
                    "- Make peace with the government and safeguard the data.",
                    "- Publish all the data online and expose the government.",
                    "India",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                   0);

        Missions.Add("India", IndiaMissions);

        Mission[] SpainMissions = new Mission[5];
        SpainMissions[0] = new Mission("Reina Sofia is a beautiful museum in Spain that is home to some of Picasso’s masterworks. Each work is worth a lot of money and the museum is protected by an advanced security system",
                                    "Reina Sofia",
                                    "- Help the museum management with proactive cybersecurity.",
                                    "- Hijack the survillance system and steal the artworks.",
                                    "Spain",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        SpainMissions[1] = new Mission("Football is very popular in Spain, and they have two of the best clubs in the world. They engage in multi-million dollar transfer deals. You decide to steal the email credentials of the managing director of one of the clubs.",
                                    "Football Game",
                            "- Get details of players' schedules and release it for the fans.",
                            "- Change his bank details to direct payments to your secret account.",
                                    "Spain",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        SpainMissions[2] = new Mission("Bullfighting is a controversial sport that is banned in many countries but it's still practiced in Spain. The bull is subject to stress and eventually death in a fight. However, some argue that it is a cultural heritage that should be kept alive.",
                            "Save The Bull",
                            "- Peacefully protest against it.",
                            "- Force the prime minister to ban bullfighting by blackmailing.",
                            "Spain",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        SpainMissions[3] = new Mission("A former politican is charged with making profit from a public water company. He has attempted to erase the evidence against him. You decide to hack into his laptop to find more information.",
                            "Water Firm",
                            "- Share your findings with the police.",
                            "- Falsify documents to create fake proof.",
                            "Spain",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        SpainMissions[4] = new Mission("You have established a vast network in Spain. Now you have enough power to determine the fate of this country. Spain currently has a Corruption Perceptions Index of 62 {100 is very clean and 0 is highly corrupt}.",
                    "Brute Force",
                    "- Make peace with the government and safeguard the data.",
                    "- Publish all the data online and expose the government.",
                    "Spain",
                                      500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter+ 1),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                   0);

        Missions.Add("Spain", SpainMissions);

        Mission[] USAMissions = new Mission[5];
        USAMissions[0] = new Mission("You always enjoy playing games on PlayStation. You see a chance to hack into their employees' remote computers using a spyware and get access to their data and projects. ",
                                    "PlayStation",
                                    "- Leak the details of their unannounced project to the players.",
                                    "- Sell their source code and buy more games from Sony.",
                                    "USA",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        USAMissions[1] = new Mission("SolarWind is a software company that developed a system called Orion which is used by over 30,000 companies to manage their IT resources. By hacking into their system, you get access to the data of all of their clients.",
                                    "SolarWind",
                            "- Collect data stealthily without being detected for a long time.",
                            "- Blackmail the powerful companies for influence over their decisions.",
                                    "USA",
                                           500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter)); 
        USAMissions[2] = new Mission("There has always been conspiracies about NASA knowing secrets about space and aliens. You decide to break into their system to find out yourself. It is not going to be an easy task!",
                            "ALIENS.",
                            "- Save a copy of the documents and erase them.",
                            "- Press NASA to publish the secrets.",
                            "USA",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        USAMissions[3] = new Mission("USA has recently held the presidential election. After a large amount of election fraud allegations, the results were disqualified and there is going to be another election next week.",
                            "Presidential Election",
                            "- Help verify the integrity of online votes.",
                            "- Alter the votes to elect your preferred candidate.",
                            "USA",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        USAMissions[4] = new Mission("You have established a vast network in USA. Now you have enough power to determine the fate of this country. USA currently has a Corruption Perceptions Index of 67 {100 is very clean and 0 is highly corrupt}.",
                    "Brute Force",
                    "- Make peace with the government and safeguard the data.",
                    "- Publish all the data online and expose the government.",
                    "USA",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter+ 1),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                   0);

        Missions.Add("USA", USAMissions);


        Mission[] UkraineMissions = new Mission[5];
        UkraineMissions[0] = new Mission("It is Thursday afternoon and you are really bored, so you decide to hire someone to hack a large electronic sign in downtown Kiev and write something on it.",
                                    "Electronic Sign",
                                    "- Write a joke.",
                                    "- Write your alias.",
                                    "Ukraine",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        UkraineMissions[1] = new Mission("Chernobyl is located in the northern Ukraine. Chernobyl Exclusion Zone is protected by an advanced survillance system due to high radiation level. Very few can access the zone after the nuclear disaster.",
                                    "Chernobyl",
                            "- Collaborate with the State Emergency Service to stop intruders.",
                            "- Disable their survillance system and send someone to investigate.",
                                    "Chernobyl",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        UkraineMissions[2] = new Mission("Many organizations and businesses in Ukraine have recently been victims of cyberattacks. Ukraine accuses Russia of committing Cyberwarfare against the nation. You decide to trace these attacks and see where they are coming from.",
                            "Cyberwarfare",
                            "- Help the government protect the country against these attacks.",
                            "- Join the hackers against the government of Ukraine.",
                            "Ukraine",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        UkraineMissions[3] = new Mission("After hacking into the computer systems of Ministry of Defense, you notice the existance of secret malware planted for espionage. You suspect it has had access to classified documents for a while now.",
                            "Cyber Espionage",
                            "- Help the government remove the malware and keep it a secret.",
                            "- Reveal the extent of the attack to the public.",
                            "Ukraine",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        UkraineMissions[4] = new Mission("You have established a vast network in Ukraine. Now you have enough power to determine the fate of this country. Ukraine currently has a Corruption Perceptions Index of 33 {100 is very clean and 0 is highly corrupt}.",
                    "Brute Force",
                    "- Make peace with the government and safeguard the data.",
                    "- Publish all the data online and expose the government.",
                    "Ukraine",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter+ 1),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                   0);

        Missions.Add("Ukraine", UkraineMissions);


        Mission[] JapanMissions = new Mission[5];
        JapanMissions[0] = new Mission("Technology in Japan seems to be a very touchy topic at the moment, it has not developed in any meaningful ways over the years. What is going on?",
             "Tekunoroji",
             "- Offer assistances to government to help cybersecurity in the nation.",
             "- Mine information from the existing companies that handle the internet.",
             "Japan",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        JapanMissions[1] = new Mission("You find hacker by the name of Akai Yurei has control over the government in Japan with blackmail. This could go different ways.",
     "Akai Yurei",
     "- Locate him on the dark web in order to expose him.",
     "- Follow trails to get a lead on Akai's identity.",
     "Japan",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        JapanMissions[2] = new Mission("You found an encrypted address that leads to Akai Yurei among all he's done, a likely mistake from his early career.",
        "Red Ghost",
        "- Share the address with other Japanese hackers to work as a team.",
        "- Use the address to identify him and find relatives and family.",
        "Japan",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        JapanMissions[3] = new Mission("You find Akai Yurei's real name and where he lives with a beacon on his network. You will have to figure out how best to fight him.",
        "Final Fight",
        "- Corrupt Akai's assets and notify the government.",
        "- Assassinate Akai and seize his files for blackmail purposes.",
        "Japan",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        JapanMissions[4] = new Mission("You have established a vast network in Japan. Now you have enough power to determine the fate of this country. Japan currently has a Corruption Perceptions Index of 74 {100 is very clean and 0 is highly corrupt}.",
                    "Brute Force",
                    "- Make peace with the government and safeguard the data.",
                    "- Publish all the data online and expose the government.",
                    "Japan",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                   0);
        Missions.Add("Japan", JapanMissions);


        Mission[] RussiaMission = new Mission[5];
        RussiaMission[0] = new Mission("The world's greatest hackers live in Russia. You are interested to get to know a few of them.",
             "Meet Up",
             "- Meet hackers that are working for the government.",
             "- Meet independent hackers that prioritize self-interest.",
             "Russia",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        RussiaMission[1] = new Mission("It looks like Interpol has got some clues on your identity and is searching for you with cybercrime charges. You are probably in a big trouble and you need to come up with a plan fast.",
     "Interpol",
     "- Ask the government to hide you in exchange for information on other countries.",
     "- Buy a new identity through dark web.",
     "Russia",
     50000,
     50000,
     0);

        RussiaMission[2] = new Mission("Your system has been under a lot of attacks during the past few days, and many of your operations are interrupted. You are very angry and decide to locate the hackers behind these attacks as soon as possible.",
        "Fight Back",
        "- Expose the group of hackers to the government.",
        "- Hire anonymous hackers to destroy their systems.",
        "Russia",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        RussiaMission[3] = new Mission("A Russian official has contacted you with a huge offer. They are interested in the information that you have collected from countries from all around the world, and are willing to buy it for a high price.",
        "Foreign Policy",
        "- Accept the offer and sell all information.",
        "- Release this offer to the International Press.",
        "Russia",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        RussiaMission[4] = new Mission("You have established a vast network in Russia. Now you have enough power to determine the fate of this country. Russia currently has a Corruption Perceptions Index of 30 {100 is very clean and 0 is highly corrupt}.",
                    "Brute Force",
                    "- Make peace with the government and safeguard the data.",
                    "- Publish all the data online and expose the government.",
                    "Russia",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                   0);
        Missions.Add("Russia", RussiaMission);

        Mission[] IranMissions = new Mission[5];
        IranMissions[0] = new Mission("The world's greatest hackers live in Russia. You are interested to get to know a few of them.",
             "Meet Up",
             "- Meet hackers that are working for the government.",
             "- Meet independent hackers that prioritize self-interest.",
             "Iran",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        IranMissions[1] = new Mission("There has recently been a cyber attack that paralyzed all gas stations in Iran. Iranian officials blame US for this disruption. You decide to dig deep and find details of this attack and collect evidence.",
     "Gas Stations",
     "- Help Iran back up its claim with evidence.",
     "- Identify and abuse new weaknesses in their system.",
     "Iran",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        IranMissions[2] = new Mission("Iranian Cyber Army has been working on a new malware to infect Israel's Internet infrastructure. They have allocated great resources for this project.",
        "Cyber Army",
        "- Assist the government in the development of this virus.",
        "- Distrupt their systems and erase their work.",
        "Iran",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        IranMissions[3] = new Mission("You have been informed of a potential attempt on the life of Iran's president by a group of rioters. This does not seem to be a very well planned attack and you don't anticipate a high success rate.",
        "Rioters' Attempt",
        "- Inform the president's office.",
        "- Hire assasins to help rioters take him down.",
        "Iran",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        IranMissions[4] = new Mission("You have established a vast network in Iran. Now you have enough power to determine the fate of this country. Iran currently has a Corruption Perceptions Index of 25 {100 is very clean and 0 is highly corrupt}.",
                    "Brute Force",
                    "- Make peace with the government and safeguard the data.",
                    "- Publish all the data online and expose the government.",
                    "Iran",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter+ 1),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                   0);
        Missions.Add("Iran", IranMissions);


        Mission[] AustraliaMissions = new Mission[5];
        AustraliaMissions[0] = new Mission("Australia is finally coming out of the copper wire ages and is moving to fiber optics. There is an opportunity here.",
             "Copper Wire",
             "- Offer efficient code to help increase internet speed in ISPs.",
             "- Attack the adapting ISPs with spyware while they are occupied.",
             "Australia",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        AustraliaMissions[1] = new Mission("The wealth of the country has been skyrocketing as of late, but banks have been having troubles with their services. Action will be required on all ends.",
     "Bank Services",
     "- Troubleshoot issues causing blackouts on their web services.",
     "- Make a scam link for clients seeking faster service times.",
     "Australia",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        AustraliaMissions[2] = new Mission("A private vault company was recently hit hard with a robbery and the goods are being distributed on the dark web. Would you like to make a purchase?",
        "Private Vault",
        "- Locate the seller and alert proper authorities.",
        "- Locate the seller and seize the goods for yourself",
        "Australia",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));
        AustraliaMissions[3] = new Mission("The fiber optic case ended up being a sham due to corruption in the political office. Time to step in.",
        "No Copper Wire",
        "- Enter the government site and expose those found responsible.",
        "- Trojan the government site, and eliminate those found responsible.",
        "Australia",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter),
                                    500 * BigInteger.Pow(MissionMultiplier, Mission.Counter));

        AustraliaMissions[4] = new Mission("You have established a vast network in Australia. Now you have enough power to determine the fate of this country. Australia currently has a Corruption Perceptions Index of 77 {100 is very clean and 0 is highly corrupt}.",
                    "Brute Force",
                    "- Make peace with the government and safeguard the data.",
                    "- Publish all the data online and expose the government.",
                    "Australia",
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                    500 * (BigInteger)System.Math.Pow(RepInfMultiplier, (double)Mission.Counter + 1),
                                   0);
        Missions.Add("Australia", AustraliaMissions);

        for (int i = 0; i < Lock.transform.childCount ; i++)
        {
            for (int j = 0; j < Countries.Length ; j++)
            {
                if (Lock.transform.GetChild(i).transform.name == Countries[j])
                {
                    CountriesLocks[j] = Lock.transform.GetChild(i).transform.gameObject;
                    break;
                }
            }
        }
        Load();
        if (!SaveFound)
        {
            TutorialPromptAnimator.SetBool("closed", false);
            TutorialPromptAnimator.Play("Open");
            GameManagerScript.InWindow = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        while (InWindow)
        {
            return;
        }
        // Data Auto-Increment
        Timer += Time.deltaTime;
        if (Timer > 2)
        {
            Timer = 0;
            BigInteger AddedNum = 0;
            foreach (string key in MalwareNumber.Keys)
            {
                AddedNum += (MalwareNumber[key] * MalwarePower[key]);
                
            }
            AddData(AddedNum);
            StartCoroutine(TickNumberAnimation(Data_Addon, AddedNum));
            AddedNum = 0;
            foreach (string key in DarkWebNumber.Keys)
            {
                AddedNum += (DarkWebNumber[key] * DarkWebPower[key]);
            }
            AddInfluence(AddedNum);
            StartCoroutine(TickNumberAnimation(Influence_Addon, AddedNum));
            AddedNum = 0;
            foreach (string key in GovNumber.Keys)
            {
                AddedNum+= (GovNumber[key] * GovPower[key]);
            }
            AddReputation(AddedNum);
            StartCoroutine(TickNumberAnimation(Reputation_Addon, AddedNum));
            AddedNum = 0;

        }
        Data_UIText.text = BigIntegerDisplay(CurrentData);
        Influence_UIText.text = BigIntegerDisplay(CurrentInfluence);
        Reputation_UIText.text = BigIntegerDisplay(CurrentReputation);

        //Check Game End
        if (CurrentCountry == Countries.Length - 1)
        {
            return;
        }
        if (!Notification.activeSelf && MissionName.text == "NO ACTIVE MISSION" && CurrentCountry <= Countries.Length-1)
        {
            if (MissionBuffer <= 0)
            {
                MissionBuffer = Random.Range(30, 90);
            }
            else
            {
                MissionBuffer -= Time.deltaTime;
                if (MissionBuffer <= 0 || CurrentMission == 4)
                {
                    Notification.SetActive(true);
                    NewMissionInMailBox = true;
                    MissionBuffer = 0;
                }
            }
        }

        //Mission Progress
        if (MissionName.text != "NO ACTIVE MISSION" && !MissionCompleteScreen.activeSelf)
        {
            string country = Countries[CurrentCountry];
            float progress = (float)(((double)MissionProgress) / (double)Missions[country][CurrentMission].Cost);
            if (progress > Mathf.Epsilon)
            {
                scrollbar.size -= progress;
                MissionProgress = 0;
            }
            if (CumMissionProgress >= Missions[Countries[CurrentCountry]][CurrentMission].Cost)
            {
                if (Missions[country][CurrentMission].RepSelected)
                {
                    AddReputation(Missions[country][CurrentMission].RepReward);
                    ReduceInfluence(((Missions[country][CurrentMission].CostPercent * CurrentInfluence)/100));
                }
                else
                {
                    AddInfluence(Missions[country][CurrentMission].InfReward);
                    ReduceReputation(((Missions[country][CurrentMission].CostPercent * CurrentReputation) / 100));
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
                ImproveUpgrades();
                //GameManagerScript.Instance.MissionName.text = GameManagerScript.Instance.Missions[GameManagerScript.Instance.CurrentCountry][GameManagerScript.Instance.CurrentMission].Name;
            }
        }

        //Country Change
        if (CurrentCountry > 0 && CurrentMission == 4 && (CountriesLocks[CurrentCountry-1].GetComponent<WorldLocations>().WorldState == WorldLocations.State.CLEARED_BY_REPUTATION || CountriesLocks[CurrentCountry - 1].GetComponent<WorldLocations>().WorldState == WorldLocations.State.CLEARED_BY_INFLUENCE))
        {

            OnCompleteLocationAnimation();
        }

    }

    public void ReduceData(BigInteger cost)
    {
        CurrentData -= cost;
    }
    public void AddData(BigInteger cost)
    {
        CurrentData += cost;
        if (MissionName.text != "NO ACTIVE MISSION")
        {
            MissionProgress += cost;
            CumMissionProgress += cost;
            if (!inSuccessScreen)
            {
                MissionProgressDisplay.text = "Progress: " + BigIntegerDisplay(CumMissionProgress) + "/" + BigIntegerDisplay(Missions[Countries[CurrentCountry]][CurrentMission].Cost);
            }
           
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

    public void ImproveUpgrades()
    {
        List<string> keys = new List<string>(MalwarePower.Keys);
        foreach (string key in keys)
        {
            MalwarePower[key] = (int)Mathf.Max((float)MalwarePower[key] + 1, 1.1f * (float)MalwarePower[key]);
        }
        keys = new List<string>(DarkWebPower.Keys);
        foreach (string key in keys)
        {
            DarkWebPower[key] = (int)Mathf.Max((float)DarkWebPower[key] + 1, 1.1f * (float)DarkWebPower[key]);
        }
        keys = new List<string>(GovPower.Keys);
        foreach (string key in keys)
        {
            GovPower[key] = (int)Mathf.Max((float)GovPower[key] + 1, 1.1f * (float)GovPower[key]);
        }
        keys = new List<string>(UpgradePower.Keys);
        foreach (string key in keys)
        {
            UpgradePower[key] = (int)Mathf.Max((float)UpgradePower[key] + 1, 1.1f * (float)UpgradePower[key]);
        }
    }
    IEnumerator OnCompleteVictoryAnimation()
    {
        sound.PlayOneShot(clip1);
        inSuccessScreen = true;
        while (MissionCompleteScreenAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return null;

        MissionCompleteScreen.SetActive(false);
        MissionName.text = "NO ACTIVE MISSION";
        MissionReward.text = "";
        MissionProgressDisplay.text = "";
        inSuccessScreen = false;
        MissionProgress = 0;
        CumMissionProgress = 0;
        Announcement.text = "All upgrades are now 10% more powerful!";

    }

    IEnumerator TickNumberAnimation(Text Addon, BigInteger AddedNum)
    {
        if (AddedNum > 0)
        {
            Addon.gameObject.SetActive(true);
            Animator animator = Addon.GetComponent<Animator>();
            Addon.text = "+" + BigIntegerDisplay(AddedNum);
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                yield return null;
            yield return null;
            Addon.gameObject.SetActive(false);
        }
    }
       public void OnCompleteLocationAnimation()
    {
        sound.PlayOneShot(clip2);
        if (CurrentCountry > 13)
        {
            int NumClearedByInfluence = 0;
            int NumClearedByReputation = 0;
            for (int i =0;i<Countries.Length;i++)
            {
                if(CountriesLocks[i].GetComponent<WorldLocations>().WorldState == WorldLocations.State.CLEARED_BY_REPUTATION)
                {
                    NumClearedByReputation += 1;
                }
                else
                {
                    NumClearedByInfluence += 1;
                }
            }
            if (NumClearedByReputation/14 < 0.33)
            {
                CompleteLocationText.text = "Congratulations! You have cleared all the countries. You have control all over the world with your influence, and the greatest world leaders are your puppets.";
            }
            else if (NumClearedByReputation / 14 < 0.66)
            {
                CompleteLocationText.text = "Congratulations! You have cleared all the countries. You have befriended international politicans while asserting dominance when required. You are a Digital Arbiter!";
            }
            else
            {
                CompleteLocationText.text = "Congratulations! You have cleared all the countries. You have respect all over the world with your reputation, and the greatest world leaders are your allies.";
            }

        }
        else
        {
            CompleteLocationText.text = "Congratulations! You have cleared " + CountriesLocks[CurrentCountry - 1].name + ". Now you can explore " + CountriesLocks[CurrentCountry].name + ".";
        }
        CurrentMission = 0;
        MissionName.text = "NO ACTIVE MISSION";
        MissionCompleteScreen.SetActive(false);
        MissionReward.text = "";
        CumMissionProgress = 0;
        MissionProgress = 0;
        CountriesLocks[CurrentCountry].GetComponent<Image>().sprite = OpenIcon;
        CountriesLocks[CurrentCountry].GetComponent<WorldLocations>().WorldState = WorldLocations.State.In_Progress;
        InWindow = true;
        CompleteLocationPromptAnimator.SetBool("closed", false);
        CompleteLocationPromptAnimator.Play("Open");

    }

    public static string BigIntegerDisplay(BigInteger number)
    {
        
        string ScientificNotion = (number).ToString("E");
        int power = int.Parse(ScientificNotion.Substring(ScientificNotion.Length - 3));
        float rational = float.Parse(ScientificNotion.Substring(0,5));
        string display;
        if (power < 3)
        {
            return number.ToString();
        }

        if (power % 3 == 0)
        {
            rational = float.Parse(ScientificNotion.Substring(0, 4));
        }
        else if (power % 3 == 1)
        {
            rational = float.Parse(ScientificNotion.Substring(0, 5)) * 10;
        }
        else
        {
            rational = float.Parse(ScientificNotion.Substring(0, 6)) * 100;
        }
        display = rational.ToString("F2") + units[(int)(power / 3)-1];
        return display;
    }

    public void Load()
    {
        string loaded;
        loaded = PlayerPrefs.GetString("NumClicked", "N/A");
        if (loaded == "N/A")
        {
            SaveFound = false;
            return;
        }
        NumClicked = BigInteger.Parse(loaded);
        loaded = PlayerPrefs.GetString("Data");
        CurrentData = BigInteger.Parse(loaded);
        loaded = PlayerPrefs.GetString("Influence");
        CurrentInfluence = BigInteger.Parse(loaded);
        loaded = PlayerPrefs.GetString("Reputation");
        CurrentReputation = BigInteger.Parse(loaded);
        List<string> keys = new List<string>(MalwarePower.Keys);
        foreach (string entry in keys)
        {
            loaded = PlayerPrefs.GetString(entry);
            string[] words = loaded.Split(',');
            MalwareNumber[entry] = BigInteger.Parse(words[0]);
            MalwarePower[entry] = int.Parse(words[1]);
        }
        keys = new List<string>(UpgradeNumber.Keys);
        foreach (string entry in keys)
        {
            loaded = PlayerPrefs.GetString(entry);
            string[] words = loaded.Split(',');
            UpgradeNumber[entry] = int.Parse(words[0]);
            UpgradePower[entry] = int.Parse(words[1]);
        }
        keys = new List<string>(DarkWebNumber.Keys);
        foreach (string entry in keys)
        {
            loaded = PlayerPrefs.GetString(entry);
            string[] words = loaded.Split(',');
            DarkWebNumber[entry] = BigInteger.Parse(words[0]);
            DarkWebPower[entry] = int.Parse(words[1]);
        }
        keys = new List<string>(GovNumber.Keys);
        foreach (string entry in keys)
        {
            loaded = PlayerPrefs.GetString(entry);
            string[] words = loaded.Split(',');
            GovNumber[entry] = BigInteger.Parse(words[0]);
            GovPower[entry] = int.Parse(words[1]);
        }

        loaded = PlayerPrefs.GetString("CurrentMission");
        CurrentMission = int.Parse(loaded);
        loaded = PlayerPrefs.GetString("CurrentCountry");
        CurrentCountry = int.Parse(loaded);
        loaded = PlayerPrefs.GetString("MissionProgress");
        MissionProgress = BigInteger.Parse(loaded);
        loaded = PlayerPrefs.GetString("CumMissionProgress");
        CumMissionProgress = BigInteger.Parse(loaded);
        loaded = PlayerPrefs.GetString("NewMissionInMailBox");
        NewMissionInMailBox = bool.Parse(loaded);
        if (NewMissionInMailBox)
        {
            Notification.SetActive(true);
        }
        else
        {
            Notification.SetActive(false);
        }
        loaded = PlayerPrefs.GetString("MissionBuffer");
        MissionBuffer = float.Parse(loaded);
        for (int i = 0; i < GameManagerScript.Instance.CountriesLocks.Length; i++)
        {
            loaded = PlayerPrefs.GetString(GameManagerScript.Instance.Countries[i]);
            if (loaded.Equals((WorldLocations.State.CLEARED_BY_INFLUENCE).ToString()))
            {
                CountriesLocks[i].GetComponent<Image>().sprite = InfIcon;
                CountriesLocks[i].GetComponent<WorldLocations>().WorldState = WorldLocations.State.CLEARED_BY_INFLUENCE;
            }
            else if (loaded.Equals((WorldLocations.State.CLEARED_BY_REPUTATION).ToString()))
            {
                CountriesLocks[i].GetComponent<Image>().sprite = RepIcon;
                CountriesLocks[i].GetComponent<WorldLocations>().WorldState = WorldLocations.State.CLEARED_BY_REPUTATION;
            }
            else if (loaded.Equals((WorldLocations.State.INACCESSIBLE).ToString()))
            {
                CountriesLocks[i].GetComponent<Image>().sprite = LockIcon;
                CountriesLocks[i].GetComponent<WorldLocations>().WorldState = WorldLocations.State.INACCESSIBLE;
            }
            else if (loaded.Equals((WorldLocations.State.In_Progress).ToString()))
            {
                CountriesLocks[i].GetComponent<Image>().sprite = GPSIcon;
                CountriesLocks[i].GetComponent<WorldLocations>().WorldState = WorldLocations.State.In_Progress;
            }
        }

        loaded = PlayerPrefs.GetString("ScrollbarSize");
        scrollbar.size = float.Parse(loaded);
        loaded = PlayerPrefs.GetString("MissionName");
        if (loaded != "NO ACTIVE MISSION")
        {
            string country = Countries[CurrentCountry];
            Missions[country][CurrentMission].RepSelected = false;
            MissionName.text = Missions[country][CurrentMission].Name;

            Missions[country][CurrentMission].RepSelected = bool.Parse(PlayerPrefs.GetString("RepSelected"));

            Missions[country][CurrentMission].CostPercent = PlayerPrefs.GetInt("CostPercent");

            if (Missions[country][CurrentMission].RepSelected)
            {
                MissionReward.text = "REWARD: " + BigIntegerDisplay(Missions[country][CurrentMission].RepReward) + " Rep";
            }
            else
            {
                MissionReward.text = "REWARD: " + BigIntegerDisplay(Missions[country][CurrentMission].InfReward) + " Inf";
            }
            MissionProgressDisplay.text = "Progress: " + BigIntegerDisplay(CumMissionProgress) + "/" + BigIntegerDisplay(Missions[country][CurrentMission].Cost);
        }
    }
}
