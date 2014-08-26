using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace DemoApplication
{
  public class LevelSceneController : MonoBehaviour
	{
		GUIText coinsCounter;
    SpriteRenderer background;
		SpriteRenderer coins;
		SpriteRenderer complete;
		SpriteRenderer unityadslogo;
		SpriteRenderer level1;
		SpriteRenderer level2;
		SpriteRenderer playButton;

    float worldScreenHeight;
    float worldScreenWidth;

    void Start () {
			coins = GameObject.Find(@"coins").GetComponent<SpriteRenderer>();
			background = GameObject.Find(@"background").GetComponent<SpriteRenderer>();
			complete = GameObject.Find(@"complete").GetComponent<SpriteRenderer>();
			coinsCounter = GameObject.Find(@"coinsCounter").GetComponent<GUIText>();
			level1 = GameObject.Find(@"level1").GetComponent<SpriteRenderer>();
			level2 = GameObject.Find(@"level2").GetComponent<SpriteRenderer>();
			unityadslogo = GameObject.Find(@"unityads-logo").GetComponent<SpriteRenderer>();
			playButton = GameObject.Find(@"playButton").GetComponent<SpriteRenderer>();

			worldScreenHeight = Camera.main.orthographicSize * 2;
			worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

			float x = background.sprite.bounds.size.x;
			float y = background.sprite.bounds.size.y;
			float aspect = (float)x/(float)y;
			float finalHeight = (float)worldScreenWidth/(float)aspect;

			coins.transform.position = new Vector3 (worldScreenWidth/2 - coins.bounds.size.x, 
			                                        worldScreenHeight/2 - coins.bounds.size.y, 
			                                        coins.transform.position.z);
			background.transform.localScale = new Vector3 (worldScreenWidth/x, finalHeight/y, 1);
			unityadslogo.transform.position = new Vector3(0, worldScreenHeight/2 - unityadslogo.bounds.size.y*2, unityadslogo.transform.position.z);
			level1.transform.position = new Vector3(0, unityadslogo.transform.position.y - unityadslogo.bounds.size.y, level1.transform.position.z);
			level2.transform.position = new Vector3(0, unityadslogo.transform.position.y - unityadslogo.bounds.size.y, level2.transform.position.z);
			complete.transform.position = new Vector3(0, level1.transform.position.y - level1.bounds.size.y*1.5f, complete.transform.position.z);
			playButton.transform.position = new Vector3(0, -y/2 + playButton.bounds.size.y/2, complete.transform.position.z);

			coinsCounter.text = SharedData.coinsCount.ToString();
			coinsCounter.transform.position = new Vector3(1 - coinsCounter.GetScreenRect().width/Screen.width - 0.06f,
			                                              0.98f, 
			                                              0);
    }
	}
}

