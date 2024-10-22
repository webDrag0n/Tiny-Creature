using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Floor : MonoBehaviour
{
    public People.PeopleColor floor_color = People.PeopleColor.NORMAL;

    public bool is_ground_floor;
    public GameObject people_prefab;
    public SpriteRenderer floor_sprite_renderer;
    public List<GameObject> queue;
    public int max_people_allowed;
    public float people_interval;
    public float speed_generate_people;

    public float timer_normal;
    protected float _set_timer_normal;
    public float timer_high_priority;
    protected float _set_timer_high_priority;

    public GameObject floor_full_warning;
    public GameObject scene_above_bg;
    public GameObject scene_below_bg;

    public struct PeopleGenerators
    {
        public PeopleGenerator NormalGenerator;
        public PeopleGenerator HighPriorityGenerator;
        public PeopleGenerator BossGenerator;
    }

    public PeopleGenerators people_generators;

    public int floor_index;
    public LevelSetting level_setting;

    // Start is called before the first frame update
    internal void Start()
    {
        people_interval = 0.8f;
        speed_generate_people = Random.Range(0.1f, 0.3f);
        max_people_allowed = 6;
        people_generators.NormalGenerator = GameObject.Find("NormalGenerator").GetComponent<PeopleGenerator>();
        people_generators.HighPriorityGenerator = GameObject.Find("HighPriorityGenerator").GetComponent<PeopleGenerator>();
        people_generators.BossGenerator = GameObject.Find("BossGenerator").GetComponent<PeopleGenerator>();

    }

    // Update is called once per frame
    internal void FixedUpdate()
    {
        if (!is_ground_floor)
        {
            // Not Ground floor

            //timer += Time.fixedDeltaTime;
            //if (timer > 1 / speed_generate_people)
            //{
            //    timer = 0;
            //    EnQueue();
            //}
            float delta_time = Time.fixedDeltaTime;
            timer_normal += delta_time;
            timer_high_priority += delta_time;

            // Set timer
            if (_set_timer_normal <= 0)
                _set_timer_normal = people_generators.NormalGenerator.RandomGenerateTime(LevelSetting.GenerateMethod.EXPONENTIAL, level_setting.lambda_common);

            if (timer_normal >  _set_timer_normal && GetAmountOfPeople() < max_people_allowed)
            {
                timer_normal = 0;
                EnQueue(People.PeopleLevel.NORMAL);
                _set_timer_normal = people_generators.NormalGenerator.RandomGenerateTime(LevelSetting.GenerateMethod.EXPONENTIAL, level_setting.lambda_common);
            }


            if (_set_timer_high_priority <= 0)
                _set_timer_high_priority = people_generators.HighPriorityGenerator.RandomGenerateTime(LevelSetting.GenerateMethod.EXPONENTIAL, level_setting.lambda_high_priority);

            if (timer_high_priority > _set_timer_high_priority && GetAmountOfPeople() < max_people_allowed)
            {
                timer_high_priority = 0;
                EnQueue(People.PeopleLevel.HIGH_PRIORITY);
                _set_timer_high_priority = people_generators.HighPriorityGenerator.RandomGenerateTime(LevelSetting.GenerateMethod.EXPONENTIAL, level_setting.lambda_high_priority);
            }
        }
        else
        {
            // Ground floor
        }
    }

    public void SetSprite(Sprite floor_sprite)
    {
        // Set floor texture based on floor level
        floor_sprite_renderer.sprite = floor_sprite;
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

    public People Dequeue()
    {
        People people_copy = queue[0].GetComponent<People>();
        Destroy(queue[0]);
        queue.RemoveAt(0);
        QueueMoveForward();
        return people_copy;
    }

    public void EnQueue(People.PeopleLevel people_level)
    {
        Vector2 new_pos = (Vector2)transform.position +
                          new Vector2(-2f + queue.Count * people_interval + Random.Range(-0.1f, 0.1f), -0.4f + Random.Range(-0.1f, 0.1f));                             // y

        //GameObject new_people = Instantiate(
        //    people_prefab,
        //    new_pos,
        //    Quaternion.identity,
        //    transform
        //);
        
        GameObject new_people = null;
        if (people_level == People.PeopleLevel.NORMAL)
            new_people = people_generators.NormalGenerator.Generate(new_pos, Quaternion.identity, transform, floor_color, people_level);
        else if (people_level == People.PeopleLevel.HIGH_PRIORITY)
            new_people = people_generators.HighPriorityGenerator.Generate(new_pos, Quaternion.identity, transform, floor_color, people_level);
        else
            new_people = people_generators.BossGenerator.Generate(new_pos, Quaternion.identity, transform, floor_color, people_level);
        
        //Debug.Log("EnQueue --- new_people: " + new_people.IsUnityNull());
        queue.Add(new_people);
    }
}