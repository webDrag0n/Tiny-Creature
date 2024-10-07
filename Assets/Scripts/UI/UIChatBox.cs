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

    private Queue<UIChatBoxLine> chat_box_lines;
    private float timer;

    // Amount of messages currently shown,
    // Need to keep < 4 since the screen height is limited
    private int counter;
    // Start is called before the first frame update
    void Start()
    {
        ResetChatBox();
        ReadScript(0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > message_time_interval)
        {
            timer = 0;
            DisplayNextMessage();

            // Start scrolling if counter > 4
            if (counter > 4)
            {
                // Destroy oldest message
                UIChatBoxLine oldest_line = chat_box_lines.Dequeue();
                Destroy(oldest_line.gameObject);
                // Reduce showed message amount counter
                counter--;
                // Move other message up one by one
                foreach (UIChatBoxLine line in chat_box_lines)
                {
                    line.target_pos = (Vector2)line.transform.position + new Vector2(0, 140);
                }
            }
        }
    }

    void DisplayNextMessage()
    {
        try
        {
            string name = script_names.Dequeue();
            string message = script_lines.Dequeue();
            GameObject new_chat_box_line = Instantiate(chat_box_line, new Vector2(0, 0), Quaternion.identity, transform);
            new_chat_box_line.transform.localPosition = new Vector2(40, 340 - counter * 170);

            new_chat_box_line.GetComponent<UIChatBoxLine>().target_pos = new_chat_box_line.transform.position;
            new_chat_box_line.GetComponent<UIChatBoxLine>().chat_name = name;
            new_chat_box_line.GetComponent<UIChatBoxLine>().message = message;

            chat_box_lines.Enqueue(new_chat_box_line.GetComponent<UIChatBoxLine>());
            counter++;
        }
        catch
        {
            return;
        }
    }

    void ReadScript(int level)
    {
        textFile = Resources.Load<TextAsset>("ChatBoxDialogs/Dialog" + level);
        string[] data = textFile.text.Split('\n');
        foreach (string line in data)
        {
            // Do Something with the input.
            string[] name_text_pair = line.Split("|");
            PushInMessage(name_text_pair[0], name_text_pair[1]);
        }

    }

    void PushInMessage(string name, string message)
    {
        script_names.Enqueue(name);
        script_lines.Enqueue(message);
    }

    void ResetChatBox()
    {
        try
        {
            foreach (UIChatBoxLine line in chat_box_lines)
            {
                Destroy(line.gameObject);
            }
        }
        catch
        {

        }
        
        chat_box_lines = new Queue<UIChatBoxLine>();
        script_names = new Queue<string>();
        script_lines = new Queue<string>();
        timer = 0;
        counter = 0;
    }
}
