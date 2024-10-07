using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElevatorPassengerDisplay : MonoBehaviour
{
    public GameStates game_states;

    public GameObject passenger_display1;
    public GameObject passenger_display2;

    public GameObject elevator1_door_open_bg;
    public GameObject elevator2_door_open_bg;

    // Need to be length of 10
    public Image[] passenger_grids1;
    public Image[] passenger_grids2;

    private void Start()
    {
        passenger_grids1 = passenger_display1.GetComponentsInChildren<Image>();
        passenger_grids2 = passenger_display2.GetComponentsInChildren<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        RenderGrids(game_states);

    }

    private void FixedUpdate()
    {
        elevator1_door_open_bg.SetActive(game_states.elevator1_door_opened);
        elevator2_door_open_bg.SetActive(game_states.elevator2_door_opened);
    }

    public void RenderGrids(GameStates _game_states)
    {
        for (int i = 0; i < _game_states.elevator1_passengers.Length; i ++)
        {
            switch (_game_states.elevator1_passengers[i])
            {

                // Different types of people has different values
                // 0: empty, 1: normal, 2: blue people, 3: yellow people
                //           4: N High, 3: B High value,6: Y High value
                //           7: Boss
                case 0:
                    // empty
                    passenger_grids1[i].color = Color.black;
                    break;
                case 1:
                    // normal
                    passenger_grids1[i].color = Color.white;
                    break;
                case 2:
                    // blue people
                    passenger_grids1[i].color = Color.blue;
                    break;
                case 3:
                    // yellow people
                    passenger_grids1[i].color = Color.yellow;
                    break;
                case 4:
                    passenger_grids1[i].color = Color.magenta;
                    break;
                case 5:
                    passenger_grids1[i].color = Color.cyan;
                    break;
                case 6:
                    passenger_grids1[i].color = Color.red;
                    break;
                case 7:
                    passenger_grids1[i].color = Color.green;
                    break;
            }
        }
        for (int i = 0; i < _game_states.elevator2_passengers.Length; i++)
        {
            switch (_game_states.elevator2_passengers[i])
            {
                case 0:
                    // empty
                    passenger_grids2[i].color = Color.black;
                    break;
                case 1:
                    // normal
                    passenger_grids2[i].color = Color.white;
                    break;
                case 2:
                    // blue people
                    passenger_grids2[i].color = Color.blue;
                    break;
                case 3:
                    // yellow people
                    passenger_grids2[i].color = Color.yellow;
                    break;
                case 4:
                    passenger_grids2[i].color = Color.magenta;
                    break;
                case 5:
                    passenger_grids2[i].color = Color.cyan;
                    break;
                case 6:
                    passenger_grids2[i].color = Color.red;
                    break;
                case 7:
                    passenger_grids2[i].color = Color.green;
                    break;
            }
        }
    }
}
