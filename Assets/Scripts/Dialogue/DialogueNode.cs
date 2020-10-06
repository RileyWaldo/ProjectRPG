using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RPG.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        [SerializeField] string text = "";
        [SerializeField] List<string> children = new List<string>();
        [SerializeField] Rect rect = new Rect(0, 0, 200, 100);

        public string GetText()
        {
            return text;
        }

        public List<string> GetChildren()
        {
            return children;
        }

        public Rect GetRect()
        {
            return rect;
        }

#if UNITY_EDITOR
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
