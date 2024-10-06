using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GameSettings")]
public class GameSettings : ScriptableObject
{
    public int background_music_volume = 50;
    public int sound_fx_volume = 50;

    public int init_building_levels;

    public float init_elevator_speed;
    public int init_elevator_max_capacity;
    public int init_player_life;

    public float game_time_limit;
    public bool is_two_player;

    public KeyCode control_elevator_up = KeyCode.W;
    public KeyCode control_elevator_down = KeyCode.S;
    public KeyCode control_elevator_door = KeyCode.E;

    public KeyCode control_elevator2_up = KeyCode.O;
    public KeyCode control_elevator2_down = KeyCode.K;
    public KeyCode control_elevator2_door = KeyCode.I;
}
