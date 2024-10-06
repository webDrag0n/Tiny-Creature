using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class UIChatBox : MonoBehaviour
{
    public GameObject chat_box_line;
    public float message_time_interval;
    public TextAsset textFile;
    private Queue<string> script_names;
    private Queue<string> script_lines;
    private float timer;
    private int counter;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        counter = 0;
        script_names = new Queue<string>();
        script_lines = new Queue<string>();
        ReadScript("");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > message_time_interval)
        {
            timer = 0;
            DisplayNextMessage();
        }
    }

    void DisplayNextMessage()
    {
        try
        {
            string name = script_names.Dequeue();
            string message = script_lines.Dequeue();
            GameObject new_chat_box_line = Instantiate(chat_box_line, new Vector2(0, 0), Quaternion.identity, transform);
            new_chat_box_line.transform.localPosition = new Vector2(40, 440 - counter * 170);
            new_chat_box_line.GetComponentInChildren<TMPro.TMP_Text>().text = name + "\n" + message;
            counter++;
        }
        catch
        {
            return;
        }
    }

    void ReadScript(string path)
    {
        string[] data = textFile.text.Split('\n');
        foreach (string line in data)
        {
            // Do Something with the input.
            string[] name_text_pair = line.Split("|");
            script_names.Enqueue(name_text_pair[0]);
            script_lines.Enqueue(name_text_pair[1]);
        }

    }
}
