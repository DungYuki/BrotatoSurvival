using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public float HideTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, HideTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
