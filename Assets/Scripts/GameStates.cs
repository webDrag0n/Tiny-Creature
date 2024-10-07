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
    //{
    //    get { return capacity; }
    //    set {
    //        if (value > 0 && value <= 10)
    //        {
    //            capacity = value;
    //        }
    //    }
    //}
    public int boss_value;
    public float booster_speed;
    public float booster_duration;
    public bool is_auto_return;
    public bool is_second_elevator_activated;

    public int building_levels;
    public int player_life;
    public int player_money_total;
    public int player_money_ingame;


    // Different types of people has different values
    // 0: empty, 1: normal, 2: blue people, 3: yellow people
    //           4: N High, 3: B High value,6: Y High value
    //           7: Boss
    public int[] elevator1_passengers;
    public int[] elevator2_passengers;

    public bool elevator1_door_opened;
    public bool elevator2_door_opened;

    public float timer;
    public GameStatus game_status;

    public void LevelReset()
    {
        player_life = 3;
        timer = 0;
        game_status = GameStatus.in_game_paused;
        player_money_ingame = 0;
        for (int i = 0; i < elevator1_passengers.Length; i++)
        {
            elevator1_passengers[i] = 0;
        }
        for (int i = 0; i < elevator2_passengers.Length; i++)
        {
            elevator2_passengers[i] = 0;
        }

        elevator1_door_opened = false;
        elevator2_door_opened = false;

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
        is_second_elevator_activated = _game_state.is_second_elevator_activated;

        building_levels = _game_state.building_levels;
        player_life = _game_state.player_life;
        player_money_total = _game_state.player_money_total;
        player_money_ingame = _game_state.player_money_ingame;

        // Deep copy
        for (int i = 0; i < elevator1_passengers.Length; i++)
        {
            elevator1_passengers[i] = _game_state.elevator1_passengers[i];
        }
        for (int i = 0; i < elevator2_passengers.Length; i++)
        {
            elevator2_passengers[i] = _game_state.elevator2_passengers[i];
        }

        elevator1_door_opened = _game_state.elevator1_door_opened;
        elevator2_door_opened = _game_state.elevator2_door_opened;

        timer = _game_state.timer;
        game_status = _game_state.game_status;
    }
}
