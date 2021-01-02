using UnityEngine;
using RPG.Control;

namespace RPG.Dialogue
{
    public class AIConversant : MonoBehaviour, IRaycastable
    {
        [SerializeField] Dialogue dialogue = null;
        //[SerializeField] GameObject[] otherNPC;
        [SerializeField] string[] NPCNames;

        public string GetNPCName(DialogueSpeaker speaker)
        {
            string nameToReturn = "NamelessOne";
            if (speaker == DialogueSpeaker.NPC1)
            {
                if (NPCNames[0] != null)
                    nameToReturn = NPCNames[0];
            }
            else if (speaker == DialogueSpeaker.NPC2)
            {
                if (NPCNames[1] != null)
                    nameToReturn = NPCNames[1];
            }
            else if(speaker == DialogueSpeaker.NPC3)
            {
                if (NPCNames[2] != null)
                    nameToReturn = NPCNames[2];
            }
            return nameToReturn;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Dialogue;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if(dialogue == null)
            {
                return false;
            }

            if(Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
            }
            return true;
        }
    }
}
