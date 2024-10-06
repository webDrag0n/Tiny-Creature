using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : MonoBehaviour
{
    public Vector2 target_pos;

    // Start is called before the first frame update
    void Start()
    {
        target_pos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, target_pos, 10 * Time.deltaTime);
    }
}
