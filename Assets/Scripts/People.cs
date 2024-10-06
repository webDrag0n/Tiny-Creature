using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BasePeople: MonoBehaviour
{
    public int apperance;
    public Mesh[] modelMeshes;
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

    public virtual void UpdateModel()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = modelMeshes[apperance];
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
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void UpdateModel()
    {
        base.UpdateModel();
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
    }
    public override void LateUpdate()
    {
        base.LateUpdate();
    }
    public override void UpdateModel()
    {
        base.UpdateModel();
    }
}