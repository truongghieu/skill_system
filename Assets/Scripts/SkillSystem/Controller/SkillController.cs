using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pattern;
using SkillSystem;
using UnityEngine.Events;

public class SkillController : MonoBehaviour
{
    public SkillType type;
    public SkillSO skillStats;

    protected bool _isTriggered;
    protected bool _isCooldown;

    protected float _timeTrigger;
    protected float _cooldownTime;

    public UnityEvent OnTriggerEnter;
    public UnityEvent OnTriggerExit;
    public UnityEvent OnSkillUpdate;
    public UnityEvent OnSkillCooldown;
    public UnityEvent OnStop;
    public UnityEvent<SkillType,int> OnStopWithType;
    public UnityEvent OnCooldownStop;

    public float cooldownProgress
    {
        get => _cooldownTime / skillStats.cooldownTime;
    }   
    public float triggerProgress
    {
        get => _timeTrigger / skillStats.timeTrigger;
    }
    public bool IsTriggered {
        get => _isTriggered;
    }
    public bool IsCooldown {
        get => _isCooldown;
    }
    public float CooldownTime{
        get => _cooldownTime;
    }

    public virtual void LoadStat(){
        if(skillStats == null){
            Debug.LogError("SkillStats is null");
            return;
        }
        _timeTrigger = skillStats.timeTrigger;
        _cooldownTime = skillStats.cooldownTime;

    }
    public virtual void Trigger(){
        if(_isTriggered || _isCooldown){
            return;
        }
        _isTriggered = true;
        _isCooldown = true;
        OnTriggerEnter?.Invoke();
    }

    private void Update(){
        CoreHandle();
    }
    private void CoreHandle(){
        TriggerHandle();
        CooldownHandle();
    }
    private void CooldownHandle(){
        if(!_isCooldown){
            return;
        }
        _cooldownTime -= Time.deltaTime;
        OnSkillCooldown?.Invoke();
        if (_cooldownTime <= 0){
            _isCooldown = false;
            OnCooldownStop?.Invoke();
            _cooldownTime = skillStats.cooldownTime;
        }   
    }

    private void TriggerHandle(){
        if(!_isTriggered){
            return;
        }
        _timeTrigger -= Time.deltaTime;
        OnSkillUpdate?.Invoke();
        if (_timeTrigger <= 0){
            _isTriggered = false;
            OnTriggerExit?.Invoke();
            _timeTrigger = skillStats.timeTrigger;
            OnStopWithType?.Invoke(type,1);
            OnStop?.Invoke();
        }
    }

    public void ForceStop(){
        _isTriggered = false;
        _isCooldown = false;
       LoadStat();
    }


}
