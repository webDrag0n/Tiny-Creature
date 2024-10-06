using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Vector2 ground_base;
    public int levels;
    public int ground_floor_level = 0;
    public Color ground_floor_color = Color.green;
    public List<Floor> floors;
    public GameObject floor_prefab;
    public GameObject roof_prefab;
    public float floor_interval = 1.05f;

    public void Init(int _levels, Vector2 _ground_base)
    {
        levels = _levels;
        ground_base = _ground_base;
        floor_interval = 1.05f;

        if (levels <= 8)
        {
            ground_floor_level = 0;
        }
        else if (levels > 8)
        {
            ground_floor_level = levels - 8;
        }

        for (int i = 0; i < _levels; i++)
        {
            GameObject new_floor = Instantiate(floor_prefab, ground_base + new Vector2(0, 1.05f * i + 0.09f), Quaternion.identity, transform);
            new_floor.GetComponent<Floor>().floor_index = floors.Count;

            if (i == ground_floor_level)
            {
                // Set ground floor checkbox property and color
                new_floor.GetComponent<Floor>().is_ground_floor = true;
                new_floor.GetComponentInChildren<SpriteRenderer>().color = ground_floor_color;
            }
            floors.Add(new_floor.GetComponent<Floor>());
        }
        GameObject roof = Instantiate(roof_prefab, ground_base + new Vector2(-1, 1.05f * _levels - 0.3f), Quaternion.identity, transform);

    }

    public int GetAmountOfPeopleInFloor(int _floor)
    {
        return floors[_floor].GetComponent<Floor>().GetAmountOfPeople();
    }

}
