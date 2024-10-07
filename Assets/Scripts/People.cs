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

    public GameObject high_value_sign;
    public GameObject boss_sign;

    public virtual void Start()
    {
        target_pos = transform.position;
        high_value_sign = Instantiate(
            Resources.Load<GameObject>("Icon/HighValueSign"),
            (Vector2)transform.position + new Vector2(0, 1),
            Quaternion.identity,
            transform);

        boss_sign = Instantiate(
            Resources.Load<GameObject>("Icon/BossSign"),
            (Vector2)transform.position + new Vector2(0, 1),
            Quaternion.identity,
            transform);
    }

    // Update is called once per frame
    public virtual void LateUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, target_pos, 10 * Time.deltaTime);

        // High priority people show ! sign on head
        high_value_sign.SetActive(level == PeopleLevel.HIGH_PRIORITY);
        boss_sign.SetActive(level == PeopleLevel.BOSS);
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
