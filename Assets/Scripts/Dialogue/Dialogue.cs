using UnityEngine;

namespace RPG.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "RPG/Create New Dialogue", order = 2)]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] Phase[] phases = default;

        public string GetText(int index)
        {
            return phases[index].text;
        }

        public AudioClip GetAudioClip(int index)
        {
            return phases[index].audioClip;
        }

        [System.Serializable]
        private class Phase
        {
            [TextArea(3, 4)]
            public string text = "";
            public AudioClip audioClip = null;
            public Choice[] choices = null;
        }

        private struct Choice
        {
            string text;
            Dialogue dialogue;
        }
    }
}
