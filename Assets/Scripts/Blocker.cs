using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    public GameObject ExitArea;

    public void SpawnExit()
    {
        Instantiate(ExitArea, transform.position + new Vector3(0,1,0), Quaternion.identity);
        Destroy(this);
    }
}
