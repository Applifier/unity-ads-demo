using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RewardNotice : MonoBehaviour 
{
	public Text text;
	public Animator animator;

	public void SetAmount (int amount)
	{
		string notice = text.text;
		text.text = string.Format(notice,amount);
	}

	public void Show ()
	{
		animator.Play("RewardNoticeVisible");
	}
}
