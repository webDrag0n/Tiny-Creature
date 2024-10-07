using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameSettings settings;
    public GameStates game_states_default;
    public GameStates game_states;
    public GameObject menu_panel;
    public GameObject game_panel;
    public GameObject game_pause_panel;
    public GameObject game_won_pause_panel;
    public GameObject game_lost_pause_panel;
    public GameObject game_end_panel;
    public Dictionary<string, GameObject> game_panels;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        game_states.game_status = GameStatus.in_menu;
        game_panels = new Dictionary<string, GameObject>();
        game_panels.Add("menu", menu_panel);
        game_panels.Add("game", game_panel);
        game_panels.Add("game_pause", game_pause_panel);
        game_panels.Add("game_won_pause", game_won_pause_panel);
        game_panels.Add("game_lost_pause", game_lost_pause_panel);
        game_panels.Add("game_end", game_end_panel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && game_states.game_status == GameStatus.in_game_playing)
        {
            game_states.game_status = GameStatus.in_game_paused;
            OpenPanel("game_pause");
            PauseGame();
        }

        // On game won
        if (game_states.game_status == GameStatus.in_game_won_paused)
        {
            // Deduct $300 to build a new floor
            game_states.player_money_total += game_states.player_money_ingame;
            game_states.player_money_ingame = 0;
            OpenPanel("game_won_pause");
            PauseGame();
        }

        // On game lose
        if (game_states.game_status == GameStatus.in_game_lost_paused)
        {
            game_states.player_money_total += game_states.player_money_ingame;
            game_states.player_money_total += 100;
            game_states.player_money_ingame = 0;
            if (game_states.player_life == 0)
            {
                // 0 life left -> game end (bad ending)
                OpenPanel("game_end");
            }
            else
            {
                // Still have some life -> allow replay
                OpenPanel("game_lost_pause");
            }
            
            PauseGame();
        }
    }

    public void NextLevel()
    {
        game_states.building_levels++;
        game_states.current_game_level++;
        game_states.timer = 0;
        game_states.player_life = 3;
        RestartGame();
    }

    public void LoadMenu()
    {
        game_states.game_status = GameStatus.in_menu;
        OffLoadGame();
        OpenPanel("menu");
    }

    public void OffLoadMenu()
    {
        OpenPanel("None");
    }

    public void NewGame()
    {
        game_states.Load(game_states_default);
        LoadGame();
    }

    public void NewGame2P()
    {
        game_states.Load(game_states_default);
        // Directly go to last level
        game_states.current_game_level = 12;
        game_states.capacity = 10;
        game_states.move_speed = 400;
        LoadGame();
    }

    public void LoadGame()
    {
        game_states.game_status = GameStatus.in_game_paused;
        Time.timeScale = 0;
        OffLoadMenu();
        OpenPanel("game");
        // Load game state from saves to scriptable object

        // Load game scene
        SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);

        // Activate auto return
        if (game_states.current_game_level >= 5)
        {
            game_states.is_auto_return = true;
        }
        else
        {
            game_states.is_auto_return = false;
        }

        // Activate duo elevator on level 6
        if (game_states.current_game_level >= 6)
        {
            game_states.is_second_elevator_activated = true;
        }
        else
        {
            game_states.is_second_elevator_activated = false;
        }
        // Sync building level with current_game_level +3
        if (game_states.building_levels != game_states.current_game_level + 3)
        {
            Debug.LogWarning("building_levels & current_game_level out of sync!");
            game_states.building_levels = game_states.current_game_level + 3;
        }

        if (game_states.is_intro_passed)
        {
            ResumeGame();
        }
    }


    public void ResumeGame()
    {
        Time.timeScale = 1;
        game_states.game_status = GameStatus.in_game_playing;
        game_states.is_intro_passed = true;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        // Reload game scene
        OffLoadGame();
        // Reset game status
        game_states.LevelReset();
        OffLoadMenu();
        LoadGame();
    }

    public void OffLoadGame()
    {
        game_states.game_status = GameStatus.in_menu;
        SceneManager.UnloadSceneAsync("Game");
    }

    public void SaveSettings()
    {
        settings.background_music_volume = (int)(slider.value * 10000);
    }

    // Open panel with corresponding panel name and close all other panels
    public void OpenPanel(string panel_name)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
        foreach (KeyValuePair<string, GameObject> name_panel_pair in game_panels)
        {
            if (panel_name == name_panel_pair.Key)
            {
                // Open corresponding panel
                name_panel_pair.Value.SetActive(true);
            }
            else
            {
                // Close other panels
                name_panel_pair.Value.SetActive(false);
            }
        }
    }
}
