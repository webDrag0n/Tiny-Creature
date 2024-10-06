using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static PlayerObj;
using static UnityEngine.CullingGroup;

public class PeopleEvent : UnityEvent<BasePeople.PeopleState>
{

}

public class BasePeople: MonoBehaviour
{
    public enum PeopleState
    {
        idle,
        run,
        attack,
        death,
    }
    private PeopleState _currentState;
    private PeopleEvent _stateChanged = new PeopleEvent();

    public PeopleState CurrentState
    {
        get => _currentState;
        set
        {
            _stateChanged.Invoke(value);
            _currentState = value;
        }
    }

    public int apperance;
    public Vector2 target_pos;
    protected SPUM_Prefabs[] prefab_list;
    public SPUM_Prefabs _prefabs;


    public virtual void Start()
    {
        target_pos = transform.position;
        
    }

    // Update is called once per frame
    public virtual void LateUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, target_pos, 10 * Time.deltaTime);
    }

    public virtual void UpdateModel()
    {
        prefab_list = Resources.LoadAll<SPUM_Prefabs>("SPUM/Prefabs");

    }
}

public class People : BasePeople
{
    enum PeopleAppearance
    {
        Male1,
        Male2,
        Male3,
        Female1,
        Female2,
        Female3,
    }

    public override void Start()
    {
        base.Start();
        apperance = UnityEngine.Random.Range(0, Enum.GetValues(typeof(PeopleAppearance)).Length);
        UpdateModel();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void UpdateModel()
    {
        base.UpdateModel();
        _prefabs = prefab_list[apperance];
        Debug.Log("prefab_list length: " + prefab_list.Length);
        Debug.Log("apperance: " + apperance);
    }
}

public class Boss : BasePeople
{
    enum BossAppearance
    {
        Male1,
        Male2,
        Female1,
        Female2,
    }

    public override void Start()
    {
        base.Start();
        apperance = UnityEngine.Random.Range(0, Enum.GetValues(typeof(BossAppearance)).Length);
        UpdateModel();
    }
    public override void LateUpdate()
    {
        base.LateUpdate();
    }
    public override void UpdateModel()
    {
        base.UpdateModel();
        _prefabs = prefab_list[apperance];
        Debug.Log("prefab_list length: " + prefab_list.Length);
        Debug.Log("apperance: " + apperance);
    }
}