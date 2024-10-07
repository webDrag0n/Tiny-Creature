using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    public GameSettings game_settings;
    public GameStates game_states;
    private GameObject building;
    public BuildingGenerator bg;
    private float money_timer;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        game_settings.init_building_levels = game_states.building_levels;
        building = bg.Generate(game_settings, game_states);
        game_states.timer = 0;
        game_states.player_life = game_settings.init_player_life;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Camera.main.orthographicSize = 4 + game_states.building_levels * 0.5f;

        game_states.timer += Time.deltaTime;
        if (game_states.game_status == GameStatus.in_game_playing && game_states.timer >= game_settings.game_time_limit)
        {
            // If total money reached required amount (100) when time went out, level won
            if (game_states.player_money_total + game_states.player_money_ingame >= 100)
            {
                // Round won;
                game_states.player_money_total -= 100;
                game_states.player_money_total += game_states.player_money_ingame;
                game_states.player_money_ingame = 0;
                game_states.game_status = GameStatus.in_game_won_paused;
            }
            else
            {
                // Round lost;
                game_states.game_status = GameStatus.in_game_lost_paused;
            }
        }

        foreach (Floor floor in building.GetComponent<Building>().floors)
        {
            //if (floor == null)
            //{
            //    continue;
            //}
            // Floor full
            if (floor.GetAmountOfPeople() >= floor.max_people_allowed)
            {
                // Penalty on money
                money_timer += Time.deltaTime;
                // Each second the floor is full will deduct 10
                if (money_timer >= 1)
                {
                    money_timer = 0;
                    game_states.player_money_ingame -= 10;

                    if (game_states.player_money_ingame + game_states.player_money_total < 0){
                        game_states.game_status = GameStatus.in_game_lost_paused;
                    }
                    // Flash red background on corresponding floor
                    // floor.GetComponentInChildren...
                }
            }
        }
    }
}
