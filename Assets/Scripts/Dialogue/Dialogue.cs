using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RPG.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "RPG/Create New Dialogue", order = 2)]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] Vector2 newNodeOffSet = new Vector2(250, 25);
        [SerializeField] List<DialogueNode> nodes = new List<DialogueNode>();

        Dictionary<string, DialogueNode> nodeLookUp = new Dictionary<string, DialogueNode>();

        private void OnValidate()
        {
            nodeLookUp.Clear();
            foreach(DialogueNode node in GetAllNodes())
            {
                nodeLookUp[node.name] = node;
            }
        }

        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        public DialogueNode GetRootNode()
        {
            return nodes[0];
        }

        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            foreach(string childID in parentNode.GetChildren())
            {
                if(nodeLookUp.ContainsKey(childID))
                {
                    yield return nodeLookUp[childID];
                }
            }
        }

        public IEnumerable<DialogueNode> GetPlayerChildren(DialogueNode currentNode)
        {
            foreach(DialogueNode node in GetAllChildren(currentNode))
            {
                if(node.GetSpeaker() == DialogueSpeaker.Player)
                {
                    yield return node;
                }
            }
        }

        public IEnumerable<DialogueNode> GetAIChildren(DialogueNode currentNode)
        {
            foreach (DialogueNode node in GetAllChildren(currentNode))
            {
                if (node.GetSpeaker() != DialogueSpeaker.Player)
                {
                    yield return node;
                }
            }
        }

        public int GetNodeCount()
        {
            return nodes.Count;
        }

#if UNITY_EDITOR
        public void CreateNode(DialogueNode parentNode)
        {
            DialogueNode newNode = MakeNode(parentNode);
            Undo.RegisterCreatedObjectUndo(newNode, "Created Dialogue Node");
            Undo.RecordObject(this, "Added Dialouge Node");
            AddNode(newNode);
        }

        private DialogueNode MakeNode(DialogueNode parentNode)
        {
            DialogueNode newNode = CreateInstance<DialogueNode>();
            newNode.name = Guid.NewGuid().ToString();

            if (parentNode != null)
            {
                parentNode.AddChild(newNode.name);
                newNode.SetPosition(parentNode.GetRect().position + newNodeOffSet);
                if (parentNode.GetSpeaker() == DialogueSpeaker.Player)
                    newNode.SetSpeaker(DialogueSpeaker.NPC1);
                else
                    newNode.SetSpeaker(DialogueSpeaker.Player);
            }

            return newNode;
        }

        private void AddNode(DialogueNode newNode)
        {
            nodes.Add(newNode);
            OnValidate();
        }

        public void DeleteNode(DialogueNode nodeToDelete)
        {
            Undo.RecordObject(this, "Deleted Dialogue Node");
            nodes.Remove(nodeToDelete);
            OnValidate();
            RemoveDanglingChildren(nodeToDelete);
            Undo.DestroyObjectImmediate(nodeToDelete);
        }

        private void RemoveDanglingChildren(DialogueNode nodeToDelete)
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                node.RemoveChild(nodeToDelete.name);
            }
        }
#endif

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (nodes.Count == 0)
            {
                DialogueNode newNode = MakeNode(null);
                AddNode(newNode);
            }

            if (AssetDatabase.GetAssetPath(this) != "")
            {
                foreach(DialogueNode node in GetAllNodes())
                {
                    if(AssetDatabase.GetAssetPath(node) == "")
                    {
                        AssetDatabase.AddObjectToAsset(node, this);
                    }
                }
            }
#endif
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
}
