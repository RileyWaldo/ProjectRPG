using UnityEngine;
using UnityEngine.UI;

namespace RPG.Dialogue
{
    public class DialogueController : MonoBehaviour
    {
        [SerializeField] GameObject dialogBox = null;
        [SerializeField] Text text = null;
        [SerializeField] float typeSpeed = 1f;

        Dialogue currentDialogue;
        string printText;
        int dialogueIndex;
        int typeOut;
        float typeTime = 0f;
        bool showDialogue = false;

        private void Awake()
        {
            ShowDialogue(false);
        }

        private void Update()
        {
            if(showDialogue)
            {
                string textToDisplay = printText.Remove(0, typeOut);
                text.text = textToDisplay;
                typeTime += Time.deltaTime;
                if(typeTime > typeSpeed)
                {
                    typeTime = 0f;
                    typeOut -= 1;
                }
            }
        }

        public void StartDialogue(Dialogue dialogue)
        {
            
        }

        public void ShowDialogue(bool show)
        {
            showDialogue = show;
            dialogBox.SetActive(showDialogue);
        }
    }
}
