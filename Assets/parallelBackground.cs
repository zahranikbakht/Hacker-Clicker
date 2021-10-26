using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallelBackground : MonoBehaviour
{
    public float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(0, -1*Time.deltaTime * speed);
        this.GetComponent<RectTransform>().Translate(offset);
        if (this.GetComponent<RectTransform>().position.y < -1477)
        {
            this.GetComponent<RectTransform>().position = new Vector3(this.GetComponent<RectTransform>().position.x, 38,0);

        }
    }
}
