using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    public GameSettings game_settings;
    private GameObject building;
    public BuildingGenerator bg;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        building = bg.Generate(game_settings.init_building_levels, game_settings.init_elevator_speed, game_settings.init_elevator_max_capacity);
        Camera.main.orthographicSize = game_settings.init_building_levels;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
