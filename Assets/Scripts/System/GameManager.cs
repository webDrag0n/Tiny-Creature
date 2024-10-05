using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameSettings settings;
    public GameObject menu_panel;
    public GameObject game_panel;
    public GameObject game_pause_panel;
    public Slider slider;
    public GameStatus game_status;
    public GameStates game_states;
    // Start is called before the first frame update
    void Start()
    {
        game_status = GameStatus.in_menu;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && game_status == GameStatus.in_game_playing)
        {
            game_pause_panel.SetActive(true);
            PauseGame();
        }
    }

    public void LoadMenu()
    {
        game_status = GameStatus.in_menu;
        game_panel.SetActive(false);
        OffLoadGame();
        menu_panel.SetActive(true);
    }

    public void OffLoadMenu()
    {
        menu_panel.SetActive(false);
    }

    public void LoadGame()
    {
        game_status = GameStatus.in_game_paused;
        Time.timeScale = 0;
        OffLoadMenu();
        game_panel.SetActive(true);
        // Load game state from saves to scriptable object

        // Load game scene
        SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);

        if (game_states.is_intro_passed)
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        game_status = GameStatus.in_game_playing;
        game_states.is_intro_passed = true;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        game_status = GameStatus.in_game_paused;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
    }

    public void OffLoadGame()
    {
        game_panel.SetActive(false);
        game_pause_panel.SetActive(false);
        SceneManager.UnloadSceneAsync("Game");
    }

    public void SaveSettings()
    {
        settings.background_music_volume = (int)(slider.value * 10000);
    }
}
