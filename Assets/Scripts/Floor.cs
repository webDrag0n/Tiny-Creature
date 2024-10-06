using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PeopleGenerator: MonoBehaviour
{
    private enum GeneratorFlag
    {
        NORMAL,
        YELLOW,
        BLUE,
    }

    public GameObject people_prefab;

    public float lambda_common;
    public float lambda_high_priority;
    public float lambda_boss;

    internal void Start()
    {
        return;
    }

    // Update is called once per frame
    internal void FixedUpdate()
    {
        return;
    }

    public GameObject Generate(Vector2 position, Quaternion quad, UnityEngine.Transform transform)
    {
        GameObject genrated_gameobj = Instantiate(
            people_prefab,
            position,
            quad,
            transform
        );
        return genrated_gameobj;
    }
}

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
    internal void Start()
    {
        people_interval = 0.8f;
        speed_generate_people = Random.Range(0.1f, 0.3f);
        max_people_allowed = 6;
    }

    // Update is called once per frame
    internal void FixedUpdate()
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
            queue[i].GetComponent<People>().target_pos = queue[i].GetComponent<People>().target_pos - 
                                                         new Vector2(people_interval - Random.Range(-0.05f, 0.05f), 0);
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
        Vector2 new_pos = (Vector2)transform.position +
                          new Vector2(-2f + queue.Count * people_interval + Random.Range(-0.1f, 0.1f),-0.4f + Random.Range(-0.1f, 0.1f));                             // y
        GameObject new_people = Instantiate(
            people_prefab,
            new_pos,
            Quaternion.identity,
            transform
        );

        queue.Add(new_people);
    }
}