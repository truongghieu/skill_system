using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pattern;
using System.Linq;

namespace SkillSystem
{
public class SkillManager : Singleton<SkillManager>
{

    [SerializeField] SkillController[] _skillControllers;
    private Dictionary<SkillType, int> _skillCollected;

    public Dictionary<SkillType, int> SkillCollected
    {
        get => _skillCollected;
    }
    protected override void Awake(){
        base.Awake();
        Initialize();
    }

    private void Initialize(){
        _skillCollected = new Dictionary<SkillType, int>();
        if(_skillControllers == null){
            Debug.LogError("SkillControllers is null");
            return;
        }
        for (int i = 0; i < _skillControllers.Length; i++)
        {
            _skillControllers[i].LoadStat();
            // add event
            _skillControllers[i].OnStopWithType.AddListener(RemoveSkill);
            _skillCollected.Add(_skillControllers[i].type, 0);
        }
    }

    public SkillController GetSkillController(SkillType type){
        var finded = _skillControllers.Where(x => x.type == type).FirstOrDefault();
        if(finded == null){
            Debug.LogError("SkillType not found");
            return null;
        }
        return finded;
        
    }

    public int GetSkillAmount(SkillType type){
        return _skillCollected[type];
    }

    public bool IsSkillExist(SkillType type){
        return _skillCollected.ContainsKey(type);
    }
    public void AddSkill(SkillType type,int amount = 1){
        if(IsSkillExist(type)){
            var currentAmount = _skillCollected[type];
            _skillCollected[type] = currentAmount + amount;
        }
        else{
            _skillCollected.Add(type, amount);
        }
        
    }
    public void RemoveSkill(SkillType type,int amount = 1){
        if(!IsSkillExist(type)){
            Debug.LogError("SkillType not found");
            return;
        }
        var currentAmount = _skillCollected[type];
        if(currentAmount < amount){
            Debug.LogError("Skill amount not enough");
            return;
        }
        _skillCollected[type] = currentAmount - amount;

    }

    public void StopSkill(SkillType type){
        var finded = GetSkillController(type);
        if(finded == null){
            Debug.LogError("SkillType not found");
            return;
        }
        finded.ForceStop();
    }
    public void StopAllSkill(){
        for (int i = 0; i < _skillControllers.Length; i++)
        {
            _skillControllers[i].ForceStop();
        }
    }
}
}