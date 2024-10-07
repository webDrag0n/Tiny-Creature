using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
