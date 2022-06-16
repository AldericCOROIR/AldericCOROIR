using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using LookupDictionary = System.Collections.Generic.Dictionary<string, int>;
using CallBackEffect = System.Func<UnityEngine.GameObject, UnityEngine.GameObject, UnityEngine.GameObject, bool>;


public class EffectFactory 
{
    static public Dictionary<string, Queue<Effect>> effectMap = new Dictionary<string, Queue<Effect>>
    {
        { "backToOriginalPos", 
                new Queue<Effect>( new[] {new Effect("BackToOriginalPos"),  })
        },
        { "sword",
                new Queue<Effect>( new[] { new Effect("MoveToMonster"), new Effect("Sword")  })
        },
        { "dagger",
                new Queue<Effect>( new[] { new Effect("MoveToMonster"), new Effect("Dagger")  })
        },
    };
}
