using System;
using UnityEngine;

namespace QuestPage.Quest
{
    [Serializable]
    public struct EnemyQuestData
    {
        public EnemyType EnemyType;
        public Sprite View;

        [Range(100, 100000)] 
        public int MaxHealth;

        [Range(100, 100000)] 
        public int Damage;        
    }
}