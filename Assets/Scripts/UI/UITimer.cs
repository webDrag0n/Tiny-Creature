using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    public TMPro.TMP_Text timer_text;
    public GameSettings game_settings;
    public GameStates game_states;
    // Start is called before the first frame update
    void Start()
    {
        timer_text = GetComponent<TMPro.TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer_text.text = (((int)((game_settings.game_time_limit - game_states.timer) * 1000))/1000.0f).ToString();
    }
}
