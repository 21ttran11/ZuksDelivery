using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        [SerializeField] GameObject dialogBox;
        [SerializeField] TextMeshProUGUI dialogueText;
        [SerializeField] int lettersPerSeconds;

        public void ShowDialogue()
        {
            dialogBox.SetActive(true);
        }

        public IEnumerator TypeDialogue(string line)
        {
            dialogueText.text = "";
            foreach(var letter in line.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(1f / lettersPerSeconds);
            }

        }
    }
}
