using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

namespace DemoApplication
{
	public class LevelTouchesController : MonoBehaviour {
		bool ufoAnimationStarted;
		bool completeAnimationStarted;
		bool continueAnimationStarted;
		int currentLevel = 0;
		SpriteRenderer[] objects;
		bool showAnimationStarted;
		Vector3 continueButtonTransform;
		bool adsShowing = false;
		bool hideAnimationStarted;
		SpriteRenderer gameOverTitleRenderer;
		GUIText coinsCounter;
		void Start () {
			gameOverTitleRenderer = ((GameObject) GameObject.Find("background/gameOverTitle")).GetComponent<SpriteRenderer>();
			ufoAnimationStarted = false;
			completeAnimationStarted = false;
			showAnimationStarted = false;
			objects = GameObject.FindObjectsOfType<SpriteRenderer>();
			foreach (SpriteRenderer renderer in objects) {
					renderer.color = new Color(renderer.color.r,renderer.color.g,renderer.color.b, 0);
			}
			showAnimationStarted = true;
			continueButtonTransform = GameObject.Find(@"continue").transform.localScale;
			coinsCounter = GameObject.Find(@"coinsCounter").GetComponent<GUIText>();
			GameObject.Find(@"continue").transform.localScale = new Vector3(0,0,1);
			GameObject.Find(@"planet1").animation.Play();
			GameObject.Find(@"planet2").animation.Play();
			GameObject.Find(@"planet3").animation.Play();
		}

		void Update () {
			if (showAnimationStarted) {
				coinsCounter.color = new Color(coinsCounter.color.r,coinsCounter.color.g,coinsCounter.color.b, coinsCounter.color.a + 0.5f * Time.deltaTime);
				foreach (SpriteRenderer renderer in objects) {
					if (renderer.name != @"continue" && renderer.name != @"complete" && renderer.name != @"level2") {
						renderer.color = new Color(renderer.color.r,renderer.color.g,renderer.color.b, renderer.color.a + 0.5f * Time.deltaTime);
						if (renderer.color.a == 1) {
							showAnimationStarted = false;
						}
					}
				}
			}

			if (hideAnimationStarted) {
				foreach (SpriteRenderer renderer in objects) {
					if (renderer.name != @"gameOverTitle") {
						renderer.color = new Color(renderer.color.r,renderer.color.g,renderer.color.b, renderer.color.a - 0.5f * Time.deltaTime);
					}
				}
			}

			if (Input.GetMouseButtonDown (0) && !Advertisement.isShowing) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit)) {
					if (hit.collider.name == @"playButton") {
						ufoAnimationStarted = true;
						GameObject.Find(@"ufo").animation.Play();
						GameObject.Find(@"playButton").transform.localScale = new Vector3(0,0,0);
					}
					else if (hit.collider.name == @"continue") {

						GameObject.Find(@"Planet1PlaceHolder").animation.Play(@"MoveLeft");
						GameObject.Find(@"Planet2PlaceHolder").animation.Play(@"MoveLeftPlanet2");
						GameObject.Find(@"Planet3PlaceHolder").animation.Play(@"MoveLeftPlanet3");

						GameObject.Find(@"playButton").transform.localScale = new Vector3(1,1,0);
						GameObject.Find(@"playButton").animation.Play();
						GameObject.Find(@"continue").transform.localScale = new Vector3(0,0,0);
						GameObject.Find(@"complete").transform.localScale = new Vector3(0,0,0);
					
						if (currentLevel == 1) {
							GameObject.Find(@"level1").transform.localScale = new Vector3(0,0,0);
							GameObject.Find(@"level2").animation.Play();
						}
					}
					else if (hit.collider.name == @"gameOverTitle")
					{
						if(gameOverTitleRenderer.color.a > 0.9f) {
							Application.LoadLevel("DemoAppLoader");
						}
					}		
				}
			}

			if (!GameObject.Find(@"ufo").animation.isPlaying && ufoAnimationStarted) {
				ufoAnimationStarted = false;
				if (currentLevel < 1)  {
					GameObject.Find(@"complete").transform.localScale = new Vector3(1,1,1);
					GameObject.Find(@"complete").animation.Play();
					completeAnimationStarted = true;
				} else {
					hideAnimationStarted = true;
					GameObject.Find(@"gameOverTitle").transform.localScale = new Vector3(1,1,1);
					GameObject.Find(@"gameOverTitle").animation.Play();
				}
				currentLevel++;
			}

			if (!GameObject.Find(@"complete").animation.isPlaying && completeAnimationStarted) {
				if (!adsShowing) {
					adsShowing = true;
					if (Advertisement.isReady(@"defaultVideoAndPictureZone")) {
						Advertisement.Show(@"defaultVideoAndPictureZone", new ShowOptions {pause = true, resultCallback = result => {adsShowing = false;}});
					}
				}
				GameObject.Find(@"continue").animation.Play();
				GameObject.Find(@"continue").transform.localScale = continueButtonTransform;
				completeAnimationStarted = false;
				continueAnimationStarted  = true;

			}

			if (!GameObject.Find(@"continue").animation.isPlaying && continueAnimationStarted) {
				GameObject.Find(@"ufoScaleObject").transform.localScale = new Vector3 (-GameObject.Find(@"ufoScaleObject").transform.localScale.x,
				                                                                       GameObject.Find(@"ufoScaleObject").transform.localScale.y,
				                                                                       GameObject.Find(@"ufoScaleObject").transform.localScale.z);
				continueAnimationStarted = false;
			}

		}
	}
}
