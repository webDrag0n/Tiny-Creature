using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILife : MonoBehaviour
{
    public int total_lifes = 3;
    public RectTransform mask;
    public GameStates game_state;

    private void FixedUpdate()
    {
        SetLife(game_state.player_life);
    }

    public void SetLife(int _life)
    {
        mask.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (total_lifes - _life) * 80);
    }
}
