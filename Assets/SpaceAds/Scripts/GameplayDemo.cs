﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;

public class GameplayDemo : MonoBehaviour 
{
	public enum Stage { StartMenu, PrevLevel, NextLevel, }

	public Button leftButton;
	public Button rightButton;
	public Button startButton;
	public Animator animator;
	public bool unlockAll;
	public bool showInterstitialAds;

	[System.Serializable]
	public class Level
	{
		public bool isUnlocked;
		public Stage nextStage;
		public Stage prevStage;
		public string nextState;
		public string prevState;
		public string startState;
		public string resetState;
		public string continueState;
	}
	public Level[] levels;

	private int _levelIndex = 0;

	void Start ()
	{
		UnlockLevel (1, false);

		if (Debug.isDebugBuild)
		{
			UnlockAllLevels(unlockAll);
		}
		else RefreshLevelUnlockStates();

	}

	public void StartGameplay ()
	{
		LevelLoader.Instance.LoadLevel ("Gameplay");
		Debug.Log("Starting gameplay...");
		if (animator != null)
		{
			string animatorState = levels[_levelIndex].startState;

			if (animatorState != null) 
			{
				animator.Play(animatorState);

			}
		}
		else Debug.LogWarning("Unable to start gameplay.");
	}

	public void EndGameplay ()
	{
		Debug.Log("End of gameplay.");
	}

	public void Continue ()
	{
		string zoneID = null;

		if (showInterstitialAds && Advertisement.IsReady(zoneID))
		{
			Advertisement.Show(zoneID);
		}
		else DoContinue();
	}

	private void DoContinue ()
	{
		if (_levelIndex < levels.Length - 1)
		{
			animator.Play(levels[_levelIndex].continueState);
			UnlockLevel(++_levelIndex);
		}
		else
		{
			LevelLoader.Instance.LoadLevel(0);
		}
	}

	public void GoToNextStage () 
	{
		if (_levelIndex == levels.Length - 1)
		{
			GoToStage(Stage.StartMenu);
		}
		else GoToStage(Stage.NextLevel);
	}

	public void GoToPrevStage () 
	{
		if (_levelIndex == 0)
		{
			GoToStage(Stage.StartMenu);
		}
		else GoToStage(Stage.PrevLevel);
	}

	public void GoToStage (Stage stage)
	{
		switch (stage)
		{
		case Stage.StartMenu:
			LevelLoader.Instance.LoadLevel(0);
			break;
		case Stage.PrevLevel:
			if (animator != null) 
			{
				string state = null;

				state = levels[_levelIndex].prevState;

				if (!string.IsNullOrEmpty(state)) 
				{
					animator.Play(state);
					_levelIndex--;
				}
			}
			break;
		case Stage.NextLevel:
			if (animator != null) 
			{
				string state = null;
				
				state = levels[_levelIndex].nextState;

				if (!string.IsNullOrEmpty(state)) 
				{
					animator.Play(state);
					_levelIndex++;
				}
			}
			break;
		}
	}
	
	public void UnlockLevel (int i, bool isUnlocked = true)
	{
		if (i >= 0)
		{
			string prefsKey = string.Format(Constants.KEYFORMAT_LEVEL_UNLOCKED,i);

			PlayerPrefs.SetInt(prefsKey, isUnlocked ? 1 : 0);

			levels[i].isUnlocked = isUnlocked;

			RefreshButtonStates();
		}
		else Debug.LogError("Failed to unlock. Level index must be greater than or equal to zero.");
	}

	public void UnlockAllLevels (bool isUnlocked = true)
	{
		if (levels.Length > 0)
		{
			UnlockLevel(0);
			
			for (int i = 1; i < levels.Length; i++)
			{
				UnlockLevel(i,isUnlocked);
			}
			
			RefreshButtonStates();
		}
	}

	private void RefreshLevelUnlockStates ()
	{
		if (levels.Length > 0)
		{
			UnlockLevel(0);
			
			for (int i = 1; i < levels.Length; i++)
			{
				string prefsKey = string.Format(Constants.KEYFORMAT_LEVEL_UNLOCKED,i);
				
				if (PlayerPrefs.HasKey(prefsKey))
				{
					int perfsValue = PlayerPrefs.GetInt(prefsKey);
					levels[i].isUnlocked = perfsValue > 0;
				}
			}
			
			RefreshButtonStates();
		}
	}

	private void RefreshButtonStates ()
	{
		int i = _levelIndex + 1;
		bool isInteractable = levels.Length > i && levels[i].isUnlocked;

		rightButton.interactable = isInteractable;
	}
}
