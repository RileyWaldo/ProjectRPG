using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "RPG/Create New Dialogue", order = 2)]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] List<DialogueNode> nodes = new List<DialogueNode>();

        Dictionary<string, DialogueNode> nodeLookUp = new Dictionary<string, DialogueNode>();

#if UNITY_EDITOR
        private void Awake()
        {
            if(nodes.Count < 1)
            {
                DialogueNode rootNode = new DialogueNode();
                rootNode.UniqueID = Guid.NewGuid().ToString();
                nodes.Add(rootNode);
            }
        }
#endif
        private void OnValidate()
        {
            nodeLookUp.Clear();
            foreach(DialogueNode node in GetAllNodes())
            {
                nodeLookUp[node.UniqueID] = node;
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
            foreach(string childID in parentNode.children)
            {
                if(nodeLookUp.ContainsKey(childID))
                {
                    yield return nodeLookUp[childID];
                }
            }
        }

        public int GetNodeCount()
        {
            return nodes.Count;
        }

        public void CreateNode(DialogueNode parentNode)
        {
            DialogueNode newNode = new DialogueNode();
            newNode.UniqueID = Guid.NewGuid().ToString();
            newNode.rect = parentNode.rect;
            newNode.rect.position += new Vector2(parentNode.rect.width + 25, 25);
            parentNode.children.Add(newNode.UniqueID);
            nodes.Add(newNode);
            OnValidate();
        }

        public void DeleteNode(DialogueNode nodeToDelete)
        {
            nodes.Remove(nodeToDelete);
            OnValidate();
            RemoveDanglingChildren(nodeToDelete);
        }

        private void RemoveDanglingChildren(DialogueNode nodeToDelete)
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                node.children.Remove(nodeToDelete.UniqueID);
            }
        }
    }
}
