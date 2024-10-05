using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GameStates")]
public class GameStates : ScriptableObject
{
    public bool is_intro_passed;
    public int difficulty;
    public int lost_counts;
}
