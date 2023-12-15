using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/SkillSystem/Skill", order = 1)]
public class SkillSO : ScriptableObject
{
    public float timeTrigger;
    public float cooldownTime;
    public Sprite icon;
    public AudioClip sound;
    
}
