using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/LevelSetting")]
public class LevelSetting : ScriptableObject
{
    public enum LevelColor
    {
        NORMAL,
        BLUE,
        YELLOW,
    }

    public int level_index;

    public int num_level;
    public float time_limit;
    public float generate_speed = 1;

    public LevelColor[] level_color;

    public int num_of_high_priority;    // -1 represent inf
    public int num_of_boss;             // -1 represent inf

    public float lambda_common;
    public float lambda_high_priority;
    public float lambda_boss;

    public enum GenerateMethod
    {
        UNIFORM,
        EXPONENTIAL,
        AFTER_HALF,
    }

    public GenerateMethod generate_method_common = GenerateMethod.UNIFORM;
    public GenerateMethod generate_method_high_priority;
    public GenerateMethod generate_method_boss;
}