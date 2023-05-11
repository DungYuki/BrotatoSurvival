using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EploseSound : MonoBehaviour
{
    void Start()
    {
        Invoke("Explose",GetComponent<AudioSource>().clip.length);
    }

    // Update is called once per frame
    void Explose()
    {
        Destroy(gameObject);
    }
}
