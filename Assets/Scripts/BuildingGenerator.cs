using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    public GameObject building_prefab;
    public GameObject floor_prefab;
    public GameObject elevator_prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject Generate(int _levels, float _elevator_speed, int _elevator_max_capacity)
    {
        Vector2 ground_base = new Vector2(0, -2);

        // Generate building
        GameObject building_gameObject = Instantiate(building_prefab, ground_base, Quaternion.identity);
        Building building = building_gameObject.GetComponent<Building>();

        // Generate floors inside building
        building.Init(_levels, ground_base);


        // Generate elevator
        GameObject elevator = Instantiate(elevator_prefab, ground_base + new Vector2(-2.5f, 0), Quaternion.identity, building_gameObject.transform);
        elevator.GetComponent<Elevator>().building = building;
        elevator.GetComponent<Elevator>().move_speed = _elevator_speed;
        elevator.GetComponent<Elevator>().max_capacity = _elevator_max_capacity;

        return building_gameObject;
    }
}
