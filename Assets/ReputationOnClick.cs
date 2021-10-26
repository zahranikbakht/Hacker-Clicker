using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReputationOnClick : MonoBehaviour
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
            GameManagerScript.Instance.ReduceInfluence(1);
            GameManagerScript.Instance.ReduceData(10);
            GameManagerScript.Instance.AddReputation(2);
        }
    }
}
