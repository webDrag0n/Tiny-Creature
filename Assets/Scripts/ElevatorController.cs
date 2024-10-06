using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Elevator))]
public class ElevatorController : MonoBehaviour
{
    Elevator elevator;
    AudioSource door_open_sound;
    public KeyCode control_up;
    public KeyCode control_down;
    public KeyCode control_door;

    // Start is called before the first frame update
    void Start()
    {
        elevator = GetComponent<Elevator>();
        door_open_sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(control_up))
        {
            // direction = 1 (up)
            elevator.direction = 1;
        }
        else if (Input.GetKeyDown(control_down))
        {
            // direction = -1 (down)
            elevator.direction = -1;
        }

        if (Input.GetKeyDown(control_door))
        {
            // Reset timer on door open/closed
            elevator.timer = 0;
            elevator.is_door_opened = !elevator.is_door_opened;
            if (elevator.is_door_opened)
            {
                door_open_sound.Play();
            }
        }
    }
}
