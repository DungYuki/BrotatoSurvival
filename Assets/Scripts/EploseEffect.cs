using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EploseEffect : MonoBehaviour
{
    public AnimationClip effect;
    private CameraShake cameraShake;

    // Start is called before the first frame update
    void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
        StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
        Invoke("Explose", effect.length);
    }

    void Explose()
    {
        gameObject.transform.parent.gameObject.GetComponent<Blocker>().SpawnExit();
        Destroy(gameObject.transform.parent.gameObject);
    }
}