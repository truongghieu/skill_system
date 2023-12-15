using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;
public class Game : MonoBehaviour
{

    [SerializeField] private SkillUIDrawer _skillButtonsDrawer;
    private void Start() {
        SkillManager.Ins.AddSkill(SkillType.FireBall, 1);
        SkillManager.Ins.AddSkill(SkillType.IceBall, 1);
        _skillButtonsDrawer.DrawSkillButtons();
    }
}
