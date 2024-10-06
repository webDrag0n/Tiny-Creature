using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public bool is_ground_floor;
    public GameObject people_prefab;
    public List<GameObject> queue;
    public int max_people_allowed;
    public float people_interval;
    public float speed_generate_people;
    public float timer;
    public int floor_index;
    // Start is called before the first frame update
    void Start()
    {
        people_interval = 0.4f;
        speed_generate_people = Random.Range(0.1f, 0.3f);
        max_people_allowed = 8;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!is_ground_floor && GetAmountOfPeople() < max_people_allowed)
        {
            timer += Time.fixedDeltaTime;
            if (timer > 1 / speed_generate_people)
            {
                timer = 0;
                EnQueue();
            }
        }
        
    }

    public int GetAmountOfPeople()
    {
        return queue.Count;
    }

    public void QueueMoveForward()
    {
        for (int i = 0; i < queue.Count; i++)
        {
            queue[i].GetComponent<People>().target_pos = queue[i].GetComponent<People>().target_pos - new Vector2(people_interval, 0);
        }
    }

    public void Dequeue()
    {
        Destroy(queue[0]);
        queue.RemoveAt(0);
        QueueMoveForward();
    }

    public void EnQueue()
    {
        GameObject new_people = Instantiate(people_prefab, (Vector2)transform.position + new Vector2(-1.3f + queue.Count * people_interval, 0), Quaternion.identity, transform);
        queue.Add(new_people);
    }
}
