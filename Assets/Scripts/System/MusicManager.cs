using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public GameStates game_states;

    public AudioSource main_menu_bgm;
    public AudioSource game_day_bgm;
    public AudioSource game_night_bgm;
    // Update is called once per frame
    void Update()
    {
        if (game_states.game_status == GameStatus.in_menu)
        {
            main_menu_bgm.volume = 1;
            game_day_bgm.volume = 0;
            game_night_bgm.volume = 0;
        }
        if (game_states.game_status == GameStatus.in_game_playing)
        {
            main_menu_bgm.volume = 0;
            game_day_bgm.volume = 1;
            game_night_bgm.volume = 0;
        }
        if (game_states.game_status == GameStatus.in_game_won_paused
            || game_states.game_status == GameStatus.in_game_lost_paused
            || game_states.game_status == GameStatus.in_game_paused)
        {
            main_menu_bgm.volume = 0;
            game_day_bgm.volume = 0;
            game_night_bgm.volume = 1;
        }
    }

}
