using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAbilityText : MonoBehaviour
{
    public TMPro.TMP_Text ability_text;
    public GameStates game_states;
    public Ability target_ability;
    // Start is called before the first frame update
    void Start()
    {
        ability_text = GetComponent<TMPro.TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (target_ability)
        {
            case Ability.move_speed:
                ability_text.text = ((int)game_states.move_speed).ToString();
                break;
            case Ability.capacity:
                ability_text.text = game_states.capacity.ToString();
                break;
            case Ability.boss_value:
                ability_text.text = game_states.boss_value.ToString();
                break;
            case Ability.booster_speed:
                ability_text.text = game_states.booster_speed.ToString();
                break;
            case Ability.booster_duration:
                ability_text.text = game_states.booster_duration.ToString();
                break;
        }
    }
}
