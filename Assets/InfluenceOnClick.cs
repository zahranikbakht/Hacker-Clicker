using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InfluenceOnClick : MonoBehaviour
{
    Button ButtonComponent;
    // Start is called before the first frame update
    void Start()
    {
        ButtonComponent = this.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.Instance.CurrentData < 10)
        {
            ButtonComponent.interactable = true;
        }
    }
    public void TaskOnClick()
    {
        if (GameManagerScript.Instance.CurrentData >= 10)
        {
            GameManagerScript.Instance.ReduceReputation(1);
            GameManagerScript.Instance.ReduceData(10);
            GameManagerScript.Instance.AddInfluence(2);
        }
    }
}
