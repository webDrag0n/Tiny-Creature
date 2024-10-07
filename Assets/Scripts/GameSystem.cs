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
        Camera.main.orthographicSize = 6 + game_settings.init_building_levels / 2;
        game_states.timer = 0;
        game_states.player_life = game_settings.init_player_life;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        game_states.timer += Time.deltaTime;
        if (game_states.game_status == GameStatus.in_game_playing && game_states.timer >= game_settings.game_time_limit)
        {
            // If total money reached required amount (300) when time went out, level won
            if (game_states.player_money_total + game_states.player_money_ingame >= 300)
            {
                // Round won;
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
            // Floor full
            if (floor.GetAmountOfPeople() >= floor.max_people_allowed)
            {
                // Penalty on money
                money_timer += Time.deltaTime;
                Debug.Log(money_timer);
                // Each second the floor is full will deduct 10
                if (money_timer >= 1)
                {
                    money_timer = 0;
                    game_states.player_money_ingame -= 10;
                    // Flash red background on corresponding floor
                    // floor.GetComponentInChildren...
                }
            }
        }
    }
}
