using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Villager", menuName = "Villager")]
public class npcdata : ScriptableObject
{
        public string villagerName;
        public Color villagerColor;
        public Color villagerNameColor;
        public dialoguedata dialogue;
    }
