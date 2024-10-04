using System.Collections;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    public GameObject jumpObject;
    public GameObject choiceObject;

    private Animator jumpAnimator;
    private Animator choiceAnimator;

    // Nama trigger animasi (bukan state)
    private string jumpTriggerName = "jumpTrigger";  
    private string choiceTriggerName = "choiceTrigger";  

    void Start()
    {
        if (jumpObject != null)
        {
            jumpAnimator = jumpObject.GetComponent<Animator>();
            if (jumpAnimator == null)
            {
                Debug.LogError("Animator tidak ditemukan di objek jump!");
            }
        }
        if (choiceObject != null)
        {
            choiceAnimator = choiceObject.GetComponent<Animator>();
            if (choiceAnimator == null)
            {
                Debug.LogError("Animator tidak ditemukan di objek choice!");
            }
        }
    }

    public void ClickButtonJump()
    {
        if (jumpAnimator != null)
        {
            jumpAnimator.SetTrigger(jumpTriggerName);
            StartCoroutine(ResetTriggerWhenAnimationJump());
        }
    }

    public void ClickButtonChoice()
    {
        if (choiceAnimator != null)
        {
            choiceAnimator.SetTrigger("choice");
            StartCoroutine(ResetTriggerWhenAnimationChoice());
        }
    }

    private IEnumerator ResetTriggerWhenAnimationJump()
    {
        while (jumpAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            yield return null;
        }
        jumpAnimator.ResetTrigger(jumpTriggerName);
    }

    private IEnumerator ResetTriggerWhenAnimationChoice()
    {
        while (choiceAnimator.GetCurrentAnimatorStateInfo(0).IsName("choice"))
        {
            yield return null;
        }
        choiceAnimator.ResetTrigger(choiceTriggerName);
    }
}
