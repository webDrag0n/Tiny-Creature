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
}
