using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoneyInGame : MonoBehaviour
{
    public TMPro.TMP_Text money_text;
    public GameStates game_states;
    // Start is called before the first frame update
    void Start()
    {
        money_text = GetComponent<TMPro.TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        money_text.text = game_states.player_money_ingame.ToString();
    }
}
