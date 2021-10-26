using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
public class OnClickButton : MonoBehaviour
{
    private BigInteger Powerups;
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
        GameManagerScript.Instance.NumClicked += 1;
        Powerups = 1;
        foreach (string key in GameManagerScript.Instance.UpgradeNumber.Keys)
        {
            Powerups *= BigInteger.Pow(GameManagerScript.Instance.UpgradePower[key], GameManagerScript.Instance.UpgradeNumber[key]);
        }
        GameManagerScript.Instance.AddData(Powerups);
    
    }
}
