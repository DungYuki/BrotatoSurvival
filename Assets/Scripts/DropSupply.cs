using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSupply : MonoBehaviour
{
    public Supply[] SupplyPrefab;
    public void DropASupply()
    {
        for(int i = 0; i < GameState.DropRate.Length; i++)
        {
            float _drop_rate = GameState.DropRate[i] / 100.0f;
            if (Random.value > (1 - _drop_rate))
            {
                Instantiate(SupplyPrefab[i], transform.position, Quaternion.identity);
                break;
            }
        }
    }

}
