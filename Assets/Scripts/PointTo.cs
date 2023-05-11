using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PointTo : MonoBehaviour
{
    private Blocker _blocker;
    // Start is called before the first frame update
    void Start()
    {
        _blocker = FindObjectOfType<Blocker>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_blocker != null)
        {
            Vector2 direction = _blocker.transform.position - transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        }
    }
}
