using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace SkillSystem
{
public class SkillButton : MonoBehaviour
{
    [SerializeField] Image _skillIcon;
    [SerializeField] Image _skillCooldownOverlay;
    [SerializeField] Image _skillTriggerOverlay;
    [SerializeField] Text _amountText;
    [SerializeField] Text _cooldownText;
    [SerializeField] Text _triggerText;
    [SerializeField] Button BtnCpnt;
    private SkillController _skillController;
    private SkillType _skillType;
    private int _currentAmount;
    #region EVENTS
    private void RegisterEvent(){
        if(_skillController == null){
            Debug.LogError("SkillController is null");
            return;
        }
        _skillController.OnSkillCooldown.AddListener(UpdateCooldown);
        _skillController.OnSkillUpdate.AddListener(UpdateTimerTrigger);
        _skillController.OnCooldownStop.AddListener(UpdateUI);
    }


        private void UnregisterEvent(){
        if(_skillController == null){
            Debug.LogError("SkillController is null");
            return;
        }
        _skillController.OnSkillCooldown.RemoveListener(UpdateCooldown);
        _skillController.OnSkillUpdate.RemoveListener(UpdateTimerTrigger);
        _skillController.OnCooldownStop.RemoveListener(UpdateUI);

    }
    
    #endregion

    public void Initialize(SkillType type){
        _skillType = type;
        _skillController = SkillManager.Ins.GetSkillController(type);
        _currentAmount = SkillManager.Ins.GetSkillAmount(type);
        _skillTriggerOverlay.transform.parent.gameObject.SetActive(false);
        UpdateUI();
        BtnCpnt.onClick.RemoveAllListeners();
        BtnCpnt.onClick.AddListener(TriggerSkill);
        RegisterEvent();
    }

        private void UpdateUI()
        {
            if(_skillController == null){
                Debug.LogError("SkillController is null");
                return;
            }
            _skillIcon.sprite = _skillController.skillStats.icon;
            UpdateAmountTxt();
            UpdateCooldownTxt();
            UpdateCooldownOverlay();
            UpdateTriggerOverlay();
            UpdateTriggerTxt();
            _currentAmount = SkillManager.Ins.GetSkillAmount(_skillType);
            bool CanActive = _currentAmount > 0;
            gameObject.SetActive(CanActive);
        }

        
        private void UpdateTimerTrigger()
        {
            UpdateTriggerOverlay();
            UpdateTriggerTxt();
        }
        private void UpdateCooldown()
        {
            UpdateCooldownOverlay();
            UpdateCooldownTxt();
        }

        private void UpdateTriggerTxt()
        {
            _triggerText.text = _skillController.triggerProgress.ToString("f1");
            _triggerText.transform.parent.gameObject.SetActive(_skillController.IsTriggered);   
        }

        private void UpdateTriggerOverlay()
        {
            _skillTriggerOverlay.fillAmount = _skillController.triggerProgress;
        }

        private void TriggerSkill()
        {
            if(_skillController == null){
                Debug.LogError("SkillController is null");
                return;
            }
            _skillController.Trigger();
            
        }

        private void UpdateCooldownOverlay()
        {
            _skillCooldownOverlay.fillAmount = _skillController.cooldownProgress;
            _skillCooldownOverlay.transform.gameObject.SetActive(_skillController.IsCooldown);
        }

        private void UpdateCooldownTxt()
        {
            _cooldownText.text = _skillController.CooldownTime.ToString("f1");
        }

        private void UpdateAmountTxt()
        {
            _amountText.text = $"x{SkillManager.Ins.GetSkillAmount(_skillType).ToString()}";
        }

        private void OnDestroy() {
        UnregisterEvent();
    }
    }

}