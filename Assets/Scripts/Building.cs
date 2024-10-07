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
    public Sprite[] floor_textures;
    public float floor_interval = 1.05f;


    public void Init(int _levels, Vector2 _ground_base, int level_id)
    {
        levels = _levels;
        ground_base = _ground_base;
        floor_interval = 1.05f;

        if (levels <= 7)
        {
            // above ground floors
            ground_floor_level = 0;
        }
        else if (levels > 7)
        {
            // underground floors
            ground_floor_level = levels - 7;
        }

        for (int i = 0; i < _levels; i++)
        {
            GameObject new_floor = Instantiate(floor_prefab, ground_base + new Vector2(0, 1.05f * i + 0.09f), Quaternion.identity, transform);
            new_floor.GetComponent<Floor>().floor_index = floors.Count;
            new_floor.GetComponent<Floor>().level_setting = Resources.Load<LevelSetting>("LevelSettings/Level" + level_id);

            if (levels <= 7)
            {
                // Above ground floors level 0-8 -> texture 5-13
                new_floor.GetComponent<Floor>().SetSprite(floor_textures[i + 5]);
            }
            else if (levels > 7)
            {

                // underground floors level 8-12 -> texture 0-5
                new_floor.GetComponent<Floor>().SetSprite(floor_textures[i + 5 - ground_floor_level]);
                // Activate building above background on -1 (i=8) floor

            }

            if (i == ground_floor_level)
            {
                // Set ground floor checkbox property and color
                new_floor.GetComponent<Floor>().is_ground_floor = true;

                new_floor.GetComponent<Floor>().scene_above_bg.SetActive(true);
                if (i > 0)
                {
                    new_floor.GetComponent<Floor>().scene_below_bg.SetActive(true);
                }
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
