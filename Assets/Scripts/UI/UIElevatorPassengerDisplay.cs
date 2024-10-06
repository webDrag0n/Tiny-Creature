using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElevatorPassengerDisplay : MonoBehaviour
{
    public GameStates game_states;

    // Update is called once per frame
    void Update()
    {
        RenderGrids(game_states);
    }

    public void RenderGrids(GameStates _game_states)
    {

    }
}
