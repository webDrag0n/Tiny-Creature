using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIAbilityChange : MonoBehaviour
{
    public GameStates game_state;
    public Ability target_ability;
    public float value_change;
    public int money_cost;

    public void Change()
    {
        if (game_state.player_money_total < money_cost)
        {
            return;
        }

        switch (target_ability)
        {
            case Ability.move_speed:
                game_state.move_speed += value_change;
                if (game_state.move_speed < 10 || game_state.move_speed > 100)
                {
                    // Revert operation if sell ability but value is less than 0
                    game_state.move_speed -= value_change;
                    return;
                }
                game_state.player_money_total -= money_cost;
                break;
            case Ability.capacity:
                game_state.capacity += (int)value_change;
                if (game_state.capacity < 1 || game_state.capacity > 10)
                {
                    // Revert operation if change ability but value is out of range
                    game_state.capacity -= (int)value_change;
                    return;
                }
                game_state.player_money_total -= money_cost;
                break;
            case Ability.boss_value:
                game_state.boss_value += (int)value_change;
                if (game_state.boss_value < 10)
                {
                    // Revert operation if sell ability but value is less than 0
                    game_state.boss_value -= (int)value_change;
                    return;
                }
                game_state.player_money_total -= money_cost;
                break;
            case Ability.booster_speed:
                game_state.booster_speed += value_change;
                if (game_state.booster_speed < 10)
                {
                    // Revert operation if sell ability but value is less than 0
                    game_state.booster_speed -= value_change;
                    return;
                }
                game_state.player_money_total -= money_cost;
                break;
            case Ability.booster_duration:
                game_state.booster_duration += value_change;
                if (game_state.booster_duration < 1)
                {
                    // Revert operation if sell ability but value is less than 0
                    game_state.booster_duration -= value_change;
                    return;
                }
                game_state.player_money_total -= money_cost;
                break;
        }
    }
}
