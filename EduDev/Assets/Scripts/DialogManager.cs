using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private Image mcImage; // Image karakter utama
    [SerializeField] private Image npcImage; // Image karakter NPC
    [SerializeField] private Text mcText; // Teks karakter utama
    [SerializeField] private Text npcText; // Teks NPC
    [SerializeField] private Button nextButton; // Tombol next untuk melanjutkan dialog
    [SerializeField] private Animator dialogAnimator; // Animator untuk dialog box

    private Queue<string> mcDialogues; // Antrian dialog MC
    private Queue<string> npcDialogues; // Antrian dialog NPC
    private bool isMcTurn = true; // Menandakan giliran MC atau NPC
    public bool isInDialogue = false;
    private bool isTyping = false; // Menandakan apakah sedang mengetik teks

    [System.Serializable]
    public struct Dialogue
    {
        public Sprite mcSprite;
        public Sprite npcSprite;
        public string[] mcSentences;
        public string[] npcSentences;
    }

    public Dialogue[] npcDialoguesData; // Data dialog untuk setiap NPC
    private Dialogue currentDialogue;

    void Start()
    {
        mcDialogues = new Queue<string>();
        npcDialogues = new Queue<string>();
    }

    public void StartDialogue(int npcIndex)
    {
        if (npcIndex >= npcDialoguesData.Length) return;

        currentDialogue = npcDialoguesData[npcIndex];

        mcDialogues.Clear();
        npcDialogues.Clear();

        foreach (string sentence in currentDialogue.mcSentences)
        {
            mcDialogues.Enqueue(sentence);
        }

        foreach (string sentence in currentDialogue.npcSentences)
        {
            npcDialogues.Enqueue(sentence);
        }

        mcImage.sprite = currentDialogue.mcSprite;
        npcImage.sprite = currentDialogue.npcSprite;

        mcText.text = "";
        npcText.text = "";

        dialogAnimator.SetTrigger("In");
        isMcTurn = true;
        isInDialogue = true;

        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(DisplayNextSentence);
        
        DisplayNextSentence();
    }

    void DisplayNextSentence()
    {
        if (isTyping) return; // Jika sedang mengetik, abaikan klik

        if (mcDialogues.Count == 0 && npcDialogues.Count == 0)
        {
            EndDialogue();
            return;
        }

        nextButton.interactable = false; // Nonaktifkan tombol selama mengetik

        if (isMcTurn && mcDialogues.Count > 0)
        {
            string sentence = mcDialogues.Dequeue();
            StartCoroutine(TypeSentence(mcText, sentence));
            isMcTurn = false;
        }
        else if (!isMcTurn && npcDialogues.Count > 0)
        {
            string sentence = npcDialogues.Dequeue();
            StartCoroutine(TypeSentence(npcText, sentence));
            isMcTurn = true;
        }
    }

    private IEnumerator TypeSentence(Text targetText, string sentence)
    {
        isTyping = true;
        targetText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            targetText.text += letter;
            yield return null;
        }
        isTyping = false;
        nextButton.interactable = true; // Aktifkan kembali tombol setelah selesai mengetik
    }

    private void EndDialogue()
    {
        dialogAnimator.SetTrigger("Out");
        nextButton.onClick.RemoveAllListeners();
        isInDialogue = false;
    }
}