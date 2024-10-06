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
    public bool is_auto_return;

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

    public void Load(GameStates _game_state)
    {
        is_intro_passed = _game_state.is_intro_passed;
        difficulty = _game_state.difficulty;
        lost_counts = _game_state.lost_counts;

        move_speed = _game_state.move_speed;
        capacity = _game_state.capacity;
        boss_value = _game_state.boss_value;
        booster_speed = _game_state.booster_speed;
        booster_duration = _game_state.booster_duration;
        is_auto_return = _game_state.is_auto_return;

        building_levels = _game_state.building_levels;
        player_life = _game_state.player_life;
        player_money_total = _game_state.player_money_total;
        player_money_ingame = _game_state.player_money_ingame;
        timer = _game_state.timer;
        game_status = _game_state.game_status;
    }
}
