using UnityEngine;
using System.Collections;
using UnityEditor;
public class spaceAdsInitScript : MonoBehaviour {
	
	[InitializeOnLoad]
	public class Startup {
		static Startup()
		{
			string [] Scenes = new string [] {
				"Assets/SpaceAds/StartMenu.unity",
				"Assets/SpaceAds/Scenes/GameplayDemo.unity",
				"Assets/SpaceAds/Scenes/Gameplay.unity",
			};

			EditorBuildSettingsScene[] newSettings = new EditorBuildSettingsScene[Scenes.Length];

			for (int i = 0; i < Scenes.Length; i ++)
			{
				Debug.Log ("Added scene #" + i + " ("+Scenes[i]+") to build settings." );
				EditorBuildSettingsScene sceneToAdd = new EditorBuildSettingsScene(Scenes[i], true);
				newSettings[i] = sceneToAdd;
			}  
			EditorBuildSettings.scenes = newSettings;
		}
	} 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
