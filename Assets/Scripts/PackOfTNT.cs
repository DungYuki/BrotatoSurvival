using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PackOfTNT : MonoBehaviour
{
    public GameObject EploseEffect;
    public GameObject ExplosionSound;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ExplosionSound, transform.position, Quaternion.identity);
        Invoke("Explose",4f);
    }

    void Explose()
    {
        var effect = Instantiate(EploseEffect, transform.position, Quaternion.identity);
        effect.transform.parent = gameObject.transform.transform.parent;
        Destroy(gameObject);
    }
}
