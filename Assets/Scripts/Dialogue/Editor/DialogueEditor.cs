using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;

namespace RPG.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        Dialogue selectedDialogue = null;
        [NonSerialized] GUIStyle[] nodeStyle = new GUIStyle[4];
        [NonSerialized] DialogueNode draggingNode = null;
        [NonSerialized] DialogueNode creatingNode = null;
        [NonSerialized] DialogueNode deletingNode = null;
        [NonSerialized] DialogueNode linkingParentNode = null;
        [NonSerialized] Vector2 draggingOffset;
        [NonSerialized] bool draggingCanvas = false;
        [NonSerialized] Vector2 draggingCanvasOffset;
        Vector2 scrollPosition;

        const float backgroundSize = 50f;

        [MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }

        [OnOpenAssetAttribute(1)]
        public static bool OnOpenAsset(int instID, int line)
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instID) as Dialogue;
            if(dialogue != null)
            {
                ShowEditorWindow();
                return true;
            }
            
            return false;
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;
            StyleNodes();
        }

        private void OnDisable()
        {
            Selection.selectionChanged -= OnSelectionChanged;
        }

        private void StyleNodes()
        {
            for (int i = 0; i < Enum.GetNames(typeof(DialogueSpeaker)).Length; i++)
            {
                nodeStyle[i] = new GUIStyle();
                nodeStyle[i].normal.background = EditorGUIUtility.Load("node" + i.ToString()) as Texture2D;
                nodeStyle[i].padding = new RectOffset(16, 16, 16, 16);
                nodeStyle[i].border = new RectOffset(12, 12, 12, 12);
                nodeStyle[i].normal.textColor = Color.white;
            }
        }

        private void OnSelectionChanged()
        {
            Dialogue dialogue = Selection.activeObject as Dialogue;
            if(dialogue != null)
            {
                selectedDialogue = dialogue;
            }
            Repaint();
        }

        private void OnGUI()
        {
            if(selectedDialogue == null)
            {
                EditorGUILayout.LabelField("No dialogue selected.");
            }
            else
            {
                ProcessEvents();

                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                Rect canvas = GUILayoutUtility.GetRect(4000, 4000);
                Texture2D backgroundTex = Resources.Load("background") as Texture2D;
                Rect texCoords = new Rect(0, 0, 4000 / backgroundSize, 4000 / backgroundSize);
                GUI.DrawTextureWithTexCoords(canvas, backgroundTex, texCoords);

                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawConnections(node);
                }
                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawNode(node);
                }

                EditorGUILayout.EndScrollView();

                if(creatingNode != null)
                {
                    selectedDialogue.CreateNode(creatingNode);
                    creatingNode = null;
                }
                if(deletingNode != null)
                {
                    selectedDialogue.DeleteNode(deletingNode);
                    deletingNode = null;
                }
            }
        }

        private void ProcessEvents()
        {
            if(Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                draggingNode = GetNodeAtPoint(Event.current.mousePosition + scrollPosition);
                if(draggingNode != null)
                {
                    draggingOffset = draggingNode.GetRect().position - Event.current.mousePosition;
                    Selection.activeObject = draggingNode;
                }
                else
                {
                    draggingCanvas = true;
                    draggingCanvasOffset = Event.current.mousePosition + scrollPosition;
                    Selection.activeObject = selectedDialogue;
                }
            }
            else if(Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode.SetPosition(Event.current.mousePosition + draggingOffset);
                draggingNode = null;
            }
            else if(Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                draggingNode.SetPosition(Event.current.mousePosition + draggingOffset);
                GUI.changed = true;
            }
            else if(Event.current.type == EventType.MouseDrag && draggingCanvas)
            {
                scrollPosition = draggingCanvasOffset - Event.current.mousePosition;
                GUI.changed = true;
            }
            else if(Event.current.type == EventType.MouseUp && draggingCanvas)
            {
                draggingCanvas = false;
            }
        }

        private void DrawNode(DialogueNode node)
        {
            GUIStyle style = GetNodeStyle(node.GetSpeaker());

            GUILayout.BeginArea(node.GetRect(), style);

            node.SetText(EditorGUILayout.TextField(node.GetText()));

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("+"))
            {
                creatingNode = node;
            }

            DrawLinkButtons(node);

            if (selectedDialogue.GetNodeCount() > 1)
            {
                if (GUILayout.Button("x"))
                {
                    deletingNode = node;
                }
            }

            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        private GUIStyle GetNodeStyle(DialogueSpeaker speaker)
        {
            GUIStyle style = nodeStyle[0];
            if (speaker == DialogueSpeaker.NPC1)
                style = nodeStyle[1];
            else if (speaker == DialogueSpeaker.NPC2)
                style = nodeStyle[2];
            else if (speaker == DialogueSpeaker.NPC3)
                style = nodeStyle[3];

            return style;
        }

        private void DrawLinkButtons(DialogueNode node)
        {
            if (linkingParentNode == null)
            {
                if (GUILayout.Button("link"))
                {
                    linkingParentNode = node;
                }
            }
            else
            {
                if (linkingParentNode == node)
                {
                    if (GUILayout.Button("cancel"))
                    {
                        linkingParentNode = null;
                    }
                }
                else
                {
                    bool isChild = linkingParentNode.GetChildren().Contains(node.name);
                    bool isParent = node.GetChildren().Contains(linkingParentNode.name);
                    if (isChild || isParent)
                    {
                        if (GUILayout.Button("unlink"))
                        {
                            if(isChild)
                            {
                                linkingParentNode.RemoveChild(node.name);
                            }
                            if(isParent)
                            {
                                node.RemoveChild(linkingParentNode.name);
                            }
                            linkingParentNode = null;
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("Child"))
                        {
                            linkingParentNode.AddChild(node.name);
                            linkingParentNode = null;
                        }
                    }
                }
            }
        }

        private void DrawConnections(DialogueNode node)
        {
            Vector2 startPosition = new Vector2(node.GetRect().xMax, node.GetRect().center.y);
            foreach (DialogueNode childNode in selectedDialogue.GetAllChildren(node))
            {
                Vector2 endPosition = new Vector2(childNode.GetRect().xMin, childNode.GetRect().center.y);
                Vector2 controlPointOffset = endPosition - startPosition;
                controlPointOffset.y = 0f;
                controlPointOffset.x *= 0.8f;
                Handles.DrawBezier(
                    startPosition, endPosition, 
                    startPosition + controlPointOffset, 
                    endPosition - controlPointOffset, 
                    Color.white, null, 4f);
            }
        }

        private DialogueNode GetNodeAtPoint(Vector2 point)
        {
            DialogueNode foundNode = null;
            foreach(DialogueNode node in selectedDialogue.GetAllNodes())
            {
                if(node.GetRect().Contains(point))
                {
                    foundNode = node;
                }
            }
            return foundNode;
        }
    }
}
