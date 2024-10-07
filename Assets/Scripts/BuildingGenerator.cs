using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    public GameObject building_prefab;
    public GameObject elevator_prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject Generate(GameSettings game_settings, GameStates game_states)
    {
        Vector2 ground_base = new Vector2(0, -4);

        // Generate building
        GameObject building_gameObject = Instantiate(building_prefab, ground_base, Quaternion.identity);
        Building building = building_gameObject.GetComponent<Building>();

        // Generate floors inside building
        building.Init(game_settings.init_building_levels, ground_base);


        // Generate elevator
        GameObject elevator = Instantiate(elevator_prefab, ground_base + new Vector2(-3.5f, 0), Quaternion.identity, building_gameObject.transform);
        elevator.GetComponent<Elevator>().building = building;
        elevator.GetComponent<Elevator>().move_speed = game_settings.init_elevator_speed;
        elevator.GetComponent<Elevator>().max_capacity = game_settings.init_elevator_max_capacity;
        elevator.GetComponent<ElevatorController>().control_up = game_settings.control_elevator_up;
        elevator.GetComponent<ElevatorController>().control_down = game_settings.control_elevator_down;
        elevator.GetComponent<ElevatorController>().control_door = game_settings.control_elevator_door;

        if (game_states.is_second_elevator_activated)
        {
            // Generate second elevator
            GameObject elevator2 = Instantiate(elevator_prefab, ground_base + new Vector2(-4.5f, 0), Quaternion.identity, building_gameObject.transform);
            elevator2.GetComponent<Elevator>().building = building;
            elevator2.GetComponent<Elevator>().move_speed = game_settings.init_elevator_speed;
            elevator2.GetComponent<Elevator>().max_capacity = game_settings.init_elevator_max_capacity;
            elevator2.GetComponent<ElevatorController>().control_up = game_settings.control_elevator2_up;
            elevator2.GetComponent<ElevatorController>().control_down = game_settings.control_elevator2_down;
            elevator2.GetComponent<ElevatorController>().control_door = game_settings.control_elevator2_door;
        }

        return building_gameObject;
    }
}
