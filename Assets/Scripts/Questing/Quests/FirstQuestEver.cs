using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstQuestEver : Quest
{
    private void Start()
    {
        QuestName = "FirstQuestEver";
        Description = "Chop down all the trees";


        Goals.Add(new KillGoal(this,0,"Destroy 5 trees", false,0,5));

        Goals.ForEach(g => g.Init());
    }
}
