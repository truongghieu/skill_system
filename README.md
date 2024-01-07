# skill_system
<!-- read me edit -->
<!-- Key future -->
<!-- Heading -->
# KEY FUTURE
<!-- Bullet point -->
* Inherit from SkillController to easily create new skills
* Define skills via ScriptableObjects for easy editing
* Trigger skills from code or via input events
* Configure properties like cooldowns, effects, sounds
* Extendable framework that can be customized as needed

![image](/img/skillExample.JPG)

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
## 3. Note
    1. Implement skill sound in SkillController.cs
