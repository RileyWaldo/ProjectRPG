using UnityEngine;
using UnityEngine.UI;
using RPG.Dialogue;
using TMPro;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameText = default;
        [SerializeField] TextMeshProUGUI AIText = default;
        [SerializeField] Button nextButton = default;
        [SerializeField] Button quitButton = default;
        [SerializeField] GameObject AIResponse = default;
        [SerializeField] Transform choiceRoot = default;
        [SerializeField] GameObject choicePrefab = default;

        PlayerConversant playerConversant;

        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            playerConversant.onConversationUpdated += UpdateUI;
            nextButton.onClick.AddListener(() => playerConversant.Next());
            quitButton.onClick.AddListener(() => playerConversant.QuitDialogue());

            UpdateUI();
        }

        void UpdateUI()
        {
            gameObject.SetActive(playerConversant.IsActive());
            if(!playerConversant.IsActive())
            {
                return;
            }

            bool isChoosing = playerConversant.IsChoosing();

            nameText.text = playerConversant.GetName();
            AIResponse.SetActive(!isChoosing);
            choiceRoot.gameObject.SetActive(isChoosing);

            if (isChoosing)
            {
                BuildChoiceList();
            }
            else
            {
                AIText.text = playerConversant.GetText();
                nextButton.gameObject.SetActive(playerConversant.HasNext());
            }
        }

        private void BuildChoiceList()
        {
            foreach (Transform child in choiceRoot)
            {
                Destroy(child.gameObject);
            }

            foreach (DialogueNode choice in playerConversant.GetChoices())
            {
                GameObject choiceButton = Instantiate(choicePrefab, choiceRoot);
                choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = choice.GetText();

                Button button = choiceButton.GetComponentInChildren<Button>();
                button.onClick.AddListener(() => 
                {
                    playerConversant.SelectChoice(choice);
                });
            }
        }
    }
}
