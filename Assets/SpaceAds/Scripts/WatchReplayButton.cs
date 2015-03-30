using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WatchReplayButton : MonoBehaviour 
{
	public bool interactable;

	private Button _button;

	void Start ()
	{
		_button = GetComponent<Button>();
	}

	void Update ()
	{
		if (interactable)
		{
			Enable();
		}
		else
		{
			Disable();
		}
	}

	public void Enable ()
	{
		SetInteractable(true);
	}

	public void Disable ()
	{
		SetInteractable(false);
	}

	private void SetInteractable (bool interactable = true)
	{
#if EVERYPLAY_IPHONE || EVERYPLAY_ANDROID
		if (Everyplay.IsRecordingSupported() && _button != null) 
		{
			_button.interactable = interactable;
		}
#else
		_button.interactable = false;
#endif
	}

	public void ShowEveryplayModal ()
	{
#if EVERYPLAY_IPHONE || EVERYPLAY_ANDROID
		Everyplay.ShowSharingModal();
#endif
	}
}
