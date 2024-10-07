using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PeopleGenerator : MonoBehaviour
{


    public string prefab_path;

    //public GameObject people_prefab;
    public GameObject[] people_prefab_list;

    public float game_time_limit;
    public int generate_people_limit;
    protected int generate_people_count;

    private float _UniformSampler(float lambda)
    {
        return Random.Range(0, 1 / lambda);
    }

    private float _ExponentialSampler(float lambda)
    {
        return -Mathf.Log(1 - Random.Range(0, 1)) / lambda;
    }

    private float _AfterHalfSampler(float lambda)
    {
        return Random.Range(game_time_limit / 2, 3 * game_time_limit / 4);
    }

    public float RandomGenerateTime(LevelSetting.GenerateMethod method, float lambda)
    {
        float next_time = 0;
        switch (method)
        {
            case LevelSetting.GenerateMethod.UNIFORM:
                next_time = _UniformSampler(lambda);
                break;

            case LevelSetting.GenerateMethod.EXPONENTIAL:
                next_time = _ExponentialSampler(lambda);
                break;

            case LevelSetting.GenerateMethod.AFTER_HALF:
                next_time = _AfterHalfSampler(lambda);
                break;
        }
        return next_time;
    }

    internal void Start()
    {
        people_prefab_list = Resources.LoadAll<GameObject>(prefab_path);
        Debug.Log("people_prefab_list length: " + people_prefab_list.Length);
        return;
    }

    // Update is called once per frame
    internal void FixedUpdate()
    {
        return;
    }

    public GameObject Generate(Vector2 position, Quaternion quad, UnityEngine.Transform transform, Floor.ColorFlag color_flag)
    {
        if (generate_people_limit != 0)
            if (generate_people_count >= generate_people_limit)
                return null;
        generate_people_count++;
        int apperance = Random.Range(0, people_prefab_list.Length);
        GameObject genrated_gameobj = Instantiate(
            people_prefab_list[apperance],
            position,
            quad,
            transform
        );
        return genrated_gameobj;
    }
}

public class Floor : MonoBehaviour
{
    public enum ColorFlag
    {
        NORMAL,
        YELLOW,
        BLUE,
    }

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
                          new Vector2(-2f + queue.Count * people_interval + Random.Range(-0.1f, 0.1f), -0.4f + Random.Range(-0.1f, 0.1f));                             // y
        GameObject new_people = Instantiate(
            people_prefab,
            new_pos,
            Quaternion.identity,
            transform
        );

        queue.Add(new_people);
    }
}