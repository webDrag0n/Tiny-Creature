using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static PlayerObj;
using static UnityEngine.CullingGroup;

public class BasePeople: MonoBehaviour
{
    public enum PeopleLevel
    {
        NORMAL,
        HIGH_PRIORITY,
        BOSS,
    }

    public enum PeopleColor
    {
        NORMAL,
        BLUE,
        YELLOW,
    }

    public PeopleLevel level;
    public PeopleColor color;
    public Vector2 target_pos;

    public virtual void Start()
    {
        target_pos = transform.position;
    }

    // Update is called once per frame
    public virtual void LateUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, target_pos, 10 * Time.deltaTime);
    }
}

public class People : BasePeople
{
    public override void Start()
    {
        base.Start();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }
}
