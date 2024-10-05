using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Elevator))]
public class ElevatorController : MonoBehaviour
{
    Elevator elevator;
    // Start is called before the first frame update
    void Start()
    {
        elevator = GetComponent<Elevator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            // direction = 1 (up)
            elevator.direction = 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            // direction = -1 (down)
            elevator.direction = -1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Reset timer on door open/closed
            elevator.timer = 0;
            elevator.is_door_opened = !elevator.is_door_opened;
        }
    }
}
