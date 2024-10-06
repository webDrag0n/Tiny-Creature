using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GameStates")]
public class GameStates : ScriptableObject
{
    public bool is_intro_passed;
    public int difficulty;
    public int lost_counts;

    public float move_speed;
    public int capacity;
    public int boss_value;
    public float booster_speed;
    public float booster_duration;

    public int building_levels;
    public int player_life;
    public int player_money_total;
    public int player_money_ingame;
    public float timer;
    public GameStatus game_status;

    public void LevelReset()
    {
        player_life = 3;
        timer = 0;
        game_status = GameStatus.in_game_paused;

    }
}
