using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Building building;

    public TMPro.TMP_Text capacity_display;
    public GameObject door;
    public GameObject upward_arrow;
    public GameObject downward_arrow;

    // floor(s) per second
    public float move_speed;
    // people(s) per second;
    public float board_speed;
    // -1 down/1 up
    public int direction;
    public int max_capacity;
    public int people_amount_inside;
    public int current_level;

    public bool is_door_opened;

    public float timer;
    private Vector2 init_pos;
    public Vector2 target_pos;

    // Start is called before the first frame update
    void Start()
    {
        capacity_display = GetComponentInChildren<TMPro.TMP_Text>();

        move_speed = 1.3f;
        board_speed = 1;
        direction = 1;
        max_capacity = 10;
        people_amount_inside = 0;
        current_level = 0;

        timer = 0;
        init_pos = transform.position;
        target_pos = init_pos;
    }

    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, target_pos, Time.deltaTime * 10);

        // Show capacity in inGame GUI
        capacity_display.text = people_amount_inside + "/" + max_capacity;

        if (direction > 0)
        {
            upward_arrow.SetActive(true);
            downward_arrow.SetActive(false);
        }
        else
        {
            upward_arrow.SetActive(false);
            downward_arrow.SetActive(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target_pos = init_pos + new Vector2(0, 1.05f * current_level);
        if (is_door_opened)
        {
            door.SetActive(true);
            timer += Time.fixedDeltaTime;
            if (building.floors[current_level].is_ground_floor)
            {
                // People get out at ground floor
                ReleasePeople();
            }
            else
            {
                if (timer > 1 / board_speed && people_amount_inside < max_capacity)
                {
                    // People get in at other floors
                    timer = 0;
                    PushInPeopleFromFloor(current_level);
                }
            }
            
        }
        else
        {
            door.SetActive(false);
            timer += Time.fixedDeltaTime;
            if (timer > 1 / move_speed)
            {
                timer = 0;
                Move(direction);
            }
        }
        
    }

    void Move(int _delta_levels)
    {
        // delta_levels = 1: move up 1 level, -1: move down 1 level
        current_level += _delta_levels;
        if (current_level < 0)
        {
            current_level = 0;
        }

        if (current_level >= building.levels)
        {
            current_level = building.levels - 1;
        }
    }

    void PushInPeopleFromFloor(int _floor)
    {
        if (building.GetAmountOfPeopleInFloor(_floor) > 0)
        {
            building.floors[_floor].Dequeue();
            people_amount_inside += 1;
        }
    }

    void ReleasePeople()
    {
        people_amount_inside = 0;
        // play people get out animation
    }
}
