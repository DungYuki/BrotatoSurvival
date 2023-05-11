using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitArea : MonoBehaviour
{
    private GameMode _game_mode;
    // Start is called before the first frame update
    private void Start()
    {
        _game_mode = FindObjectOfType<GameMode>();  
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if(player != null)
        {
            _game_mode.PlayerEscape();
        }
    }
}
