using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChatBoxLine : MonoBehaviour
{
    public Vector2 target_pos;
    public string chat_name;
    public string message;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        GetComponentInChildren<TMPro.TMP_Text>().text = chat_name + "\n" + message;

        transform.position = Vector2.Lerp(transform.position, target_pos, Time.deltaTime * 3);
    }
}
