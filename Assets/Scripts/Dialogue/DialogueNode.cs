using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using RPG.Core;

namespace RPG.Dialogue
{
    public enum DialogueSpeaker
    {
        Player,
        NPC1,
        NPC2,
        NPC3
    }

    public class DialogueNode : ScriptableObject
    {
        [SerializeField] DialogueSpeaker speaker = DialogueSpeaker.NPC1;
        [TextArea(5, 5)]
        [SerializeField] string text = "";
        [SerializeField] AudioClip voiceAudio = default;
        [SerializeField] string onEnterAction = "";
        [SerializeField] string onExitAction = "";
        [SerializeField] Condition condition = default;
        [SerializeField] List<string> children = new List<string>();
        [SerializeField] Rect rect = new Rect(0, 0, 200, 100);

        public DialogueSpeaker GetSpeaker()
        {
            return speaker;
        }

        public string GetText()
        {
            return text;
        }

        public AudioClip GetVoiceClip()
        {
            return voiceAudio;
        }

        public List<string> GetChildren()
        {
            return children;
        }

        public Rect GetRect()
        {
            return rect;
        }

        public string GetOnEnterAction()
        {
            return onEnterAction;
        }

        public string GetOnExitAction()
        {
            return onExitAction;
        }

        public bool CheckCondition(IEnumerable<IPredicateEvaluator> evaluators)
        {
            return condition.Check(evaluators);
        }

#if UNITY_EDITOR
        public void SetSpeaker(DialogueSpeaker newSpeaker)
        {
            Undo.RecordObject(this, "Updated Dialogue Node Speaker");
            speaker = newSpeaker;
            EditorUtility.SetDirty(this);
        }

        public void SetText(string newText)
        {
            if(newText != text)
            {
                Undo.RecordObject(this, "Updated Dialogue Node Text");
                text = newText;
                EditorUtility.SetDirty(this);
            }
        }

        public void SetRect(Rect newRect)
        {
            Undo.RecordObject(this, "Updated Dialogue Node Rect");
            rect = newRect;
            EditorUtility.SetDirty(this);
        }

        public void SetPosition(Vector2 pos)
        {
            Undo.RecordObject(this, "Moved Dialogue Node");
            rect.position = pos;
            EditorUtility.SetDirty(this);
        }

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Added Dialogue Link");
            children.Add(childID);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Removed Dialogue Link");
            children.Remove(childID);
            EditorUtility.SetDirty(this);
        }
#endif
    }
}
