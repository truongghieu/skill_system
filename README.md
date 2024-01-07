# skill_system
<!-- read me edit -->
<!-- Introduce repo -->
    This repo is a skill system for unity
    It is a core skill system
    It can be used in any game
    It is easy to use
    It is easy to extend

## 1. To create more skill
    inherit from SkillController.cs 
    Override the function
    ```csharp 
    public override void TriggerHandle()
    ```
## 2. To create Data from ScriptableObject
    <!-- Menu -->
    ScriptableObjects/SkillSystem/Skill
    Property:
    1. timeTrigger
    2. cooldownTime
    3. icon
    4. sound
<!-- show img -->
![image](/img/skilldatat.JPG)
    