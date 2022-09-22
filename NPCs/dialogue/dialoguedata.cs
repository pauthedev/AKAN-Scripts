using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Dialogue Data", menuName = "Dialogue Data")]
public class dialoguedata : ScriptableObject
{
        [TextArea(4, 4)]
        public List<string> conversationBlock;
}
