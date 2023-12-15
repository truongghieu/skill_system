using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pattern;

namespace SkillSystem
{
public class SkillUIDrawer : MonoBehaviour
{
    [SerializeField] private Transform _grid;
    [SerializeField] private SkillButton _skillButtonPrefab;
    private Dictionary<SkillType, int> _skillCollecteds;


public void DrawSkillButtons(){
    Helper.ClearChilds(_grid);
    _skillCollecteds = SkillManager.Ins.SkillCollected;
    if (_skillCollecteds == null)
    {
        Debug.LogError("SkillCollected is null");
        return;
    }
    foreach (var item in _skillCollecteds)
    {
        var skillButton = Instantiate(_skillButtonPrefab);
        skillButton.Initialize(item.Key);
        Helper.AssignToRoot(_grid, skillButton.transform, Vector3.zero, Vector3.one, false);
    }
}

}

}