using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.UI;

public class OnClickButton : MonoBehaviour
{
    private BigInteger Powerups;
    public GameObject feedback;
    public GameObject feedbackPostion;

    public GameObject TargetObject;
    public List<GameObject> PoolOfObjects;
    public int NumberInPool;
    AudioSource sound;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    // Start is called before the first frame update
    void Start()
    {
        sound = this.GetComponent<AudioSource>();
        PoolOfObjects = new List<GameObject>();
        GameObject tmp;
        for (int i =0;i<NumberInPool;i++)
        {
            tmp = Instantiate(TargetObject);
            tmp.SetActive(false);
            PoolOfObjects.Add(tmp);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TaskOnClick()
    {
        int SE = Random.Range(0, 100);
        if (SE < 40)
        {
            sound.PlayOneShot(clip1);
        }
        else if (SE < 60)
        {
            sound.PlayOneShot(clip2);
        }
        else if (SE < 90) 
        {
            sound.PlayOneShot(clip3);
        }
        else
        {
            sound.PlayOneShot(clip4);
        }
        GameManagerScript.Instance.NumClicked += 1;
        Powerups = 1;
        foreach (string key in GameManagerScript.Instance.UpgradeNumber.Keys)
        {
            Powerups += (GameManagerScript.Instance.UpgradePower[key] * GameManagerScript.Instance.UpgradeNumber[key]);

        }

        GameObject newText = GetPooledObject();
        newText.transform.position = feedbackPostion.GetComponent<RectTransform>().transform.position;
        newText.transform.SetParent(feedbackPostion.transform, false);
        newText.GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-20, 20), 0, 0);
        newText.GetComponent<Text>().text = "+" + GameManagerScript.BigIntegerDisplay(Powerups);
        newText.SetActive(true);
        StartCoroutine(KillNumber(newText));
        GameManagerScript.Instance.AddData(Powerups);
    
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < PoolOfObjects.Count; i++)
        {
            if(!PoolOfObjects[i].activeInHierarchy)
            {
                return PoolOfObjects[i];
            }
        }
        GameObject tmp = Instantiate(TargetObject);
        tmp.SetActive(false);
        PoolOfObjects.Add(tmp);
        return PoolOfObjects[PoolOfObjects.Count-1];
    }
    IEnumerator KillNumber(GameObject text)
    {
        Animator anim = text.GetComponent<Animator>();
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return null;

        text.SetActive(false);
    }

}
