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
    public LevelColor[] level_color;

    public float[] lambda_common;
    public float[] lambda_high_priority;
    public float[] lambda_boss;

}