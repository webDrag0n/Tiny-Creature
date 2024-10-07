using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameStates game_states;
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
    public bool is_auto_return;

    public float timer;
    private Vector2 init_pos;
    public Vector2 target_pos;

    // 0 or 1
    private int elevator_id;

    // Start is called before the first frame update
    private void Start()
    {
        capacity_display = GetComponentInChildren<TMPro.TMP_Text>();

        move_speed = game_states.move_speed;
        board_speed = 1;
        direction = 1;
        max_capacity = game_states.capacity;
        people_amount_inside = 0;
        current_level = 0;

        is_auto_return = game_states.is_auto_return;

        timer = 0;
        init_pos = transform.position;
        target_pos = init_pos;
    }

    private void Update()
    {
        // Move smoothly and shake a bit
        float distance = Vector2.Distance((Vector2)transform.position, target_pos);
        transform.position = Vector2.Lerp(transform.position, target_pos, Time.deltaTime * 10);
        transform.position = (Vector2)transform.position + new Vector2(0.01f * Random.Range(-distance, distance), 0);

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
    private void FixedUpdate()
    {
        target_pos = init_pos + new Vector2(0, 1.05f * current_level);

        // Sync door open status to game_states
        if (elevator_id == 0)
        {
            game_states.elevator1_door_opened = is_door_opened;
        }
        else if (elevator_id == 1)
        {
            game_states.elevator2_door_opened = is_door_opened;
        }

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
            if (timer > 1 / (move_speed / 50))
            {
                timer = 0;
                Move(direction);
            }
        }
    }

    private void Move(int _delta_levels)
    {
        // delta_levels = 1: move up 1 level, -1: move down 1 level
        current_level += _delta_levels;
        // Reached bottom
        if (current_level < 0)
        {
            current_level = 0;
            if (is_auto_return)
            {
                // Auto change direction to return
                direction = -direction;
            }
        }

        // Reached Top
        if (current_level >= building.levels)
        {
            current_level = building.levels - 1;
            if (is_auto_return)
            {
                // Auto change direction to return
                direction = -direction;
            }
        }
    }

    private void PushInPeopleFromFloor(int _floor)
    {
        if (building.GetAmountOfPeopleInFloor(_floor) > 0)
        {
            int[][] elevator_id_passengers = new int[2][];
            // Shallow copy: reference to original array stored in elevator_id_passengers
            elevator_id_passengers[0] = game_states.elevator1_passengers;
            elevator_id_passengers[1] = game_states.elevator2_passengers;

            int new_passenger_type = Random.Range(1, 8);

            // Check if there is yellow people if the current one is blue
            if (new_passenger_type == 2 || new_passenger_type == 5)
            {
                foreach(int passenger_type in elevator_id_passengers[elevator_id])
                {
                    if (passenger_type == 3 || passenger_type == 6)
                    {
                        // Lost directly if different colors were put together
                        game_states.game_status = GameStatus.in_game_lost_paused;
                    }
                }
            }

            // Check if there is blue people if the current one is yellow
            if (new_passenger_type == 3 || new_passenger_type == 6)
            {
                foreach (int passenger_type in elevator_id_passengers[elevator_id])
                {
                    if (passenger_type == 2 || passenger_type == 5)
                    {
                        // Lost directly if different colors were put together
                        game_states.game_status = GameStatus.in_game_lost_paused;
                    }
                }
            }

            for (int i = 0; i < elevator_id_passengers[elevator_id].Length; i++)
            {
                if (elevator_id_passengers[elevator_id][i] == 0)
                {

                    // Different types of people has different values
                    // 0: empty, 1: normal, 2: blue people, 3: yellow people
                    //           4: N High, 5: B High value,6: Y High value
                    //           7: Boss

                    // Should check people type here and assign to elevator
                    // Currently at random [1-7]
                    elevator_id_passengers[elevator_id][i] = new_passenger_type;
                    break;
                }
            }

            building.floors[_floor].Dequeue();
            people_amount_inside += 1;
        }
    }

    private void ReleasePeople()
    {
        //game_states.player_money_ingame += people_amount_inside * 100;
        int[][] elevator_id_passengers = new int[2][];
        elevator_id_passengers[0] = game_states.elevator1_passengers;
        elevator_id_passengers[1] = game_states.elevator2_passengers;

        for(int i = 0; i < elevator_id_passengers[elevator_id].Length; i++)
        {
            // Different types of people has different values
            // 0: empty, 1: normal, 2: blue people, 3: yellow people
            //           4: N High, 5: B High value,6: Y High value
            //           7: Boss
            int passenger_type = elevator_id_passengers[elevator_id][i];
            if (passenger_type > 0 && passenger_type <= 3)
            {
                // Low value people
                game_states.player_money_ingame += 20;
            }
            else if (passenger_type > 3 && passenger_type <= 6)
            {
                // High value people
                game_states.player_money_ingame += 100;
            }
            else if (passenger_type == 7)
            {
                // Boss
                game_states.player_money_ingame += game_states.boss_value;
            }
            elevator_id_passengers[elevator_id][i] = 0;
        }

        people_amount_inside = 0;
        // play people get out animation
    }
}