using TMPro;
using System.Linq;
using UnityEngine;
using IPA.Utilities;
using System.Collections;
using BeatSaberMarkupLanguage;
using HarmonyLib;
using System.Reflection;
using HMUI;
using UnityEngine.UI;

namespace PlayFirst
{
    public class CancelScore : MonoBehaviour
    {
        public static float pausetime = 0.1f;
        public static bool paused_yet = false;

        public static AudioTimeSyncController audiocontroller;
        public static SongController songcontroller;

        
        public void Update()
        {
            //Logger.log.Debug("In Update!");

            if (audiocontroller != null && songcontroller != null)
            {
                //Logger.log.Debug("Current:" + audiocontroller.songTime.ToString());

                if (Config.UserConfig.mod_enabled)
                {
                    if (audiocontroller.songTime >= pausetime && !paused_yet)
                    {
                        Logger.log.Debug("#####################");
                        songcontroller.PauseSong();
                        Logger.log.Debug("Song Paused");

                        CancelButtonViewController.Instance.ShowButton();
                        //PauseUI.instance.Setup();

                        paused_yet = true;
                    }

                    // Debug:
                    //else if (audiocontroller.songTime >= pausetime + 0.2f)
                    //{
                    //    Logger.log.Debug("#############################################################");
                    //}
                }
            }
        }
    }
}


//private Canvas _canvas;
//private TextMeshProUGUI _currentTimeText;
//private Button pausebutton;


//public void Awake()
//{
//GameObject canvasGo = new GameObject("Canvas");
//canvasGo.transform.parent = transform;
//_canvas = canvasGo.AddComponent<Canvas>();
//_canvas.renderMode = RenderMode.WorldSpace;

//var canvasTransform = _canvas.transform;
//canvasTransform.position = new Vector3(0f, 3f, 3.8f);
//canvasTransform.localScale = Vector3.one;
//Logger.log.Debug("Created Canvas");

//_currentTimeText = CreateText(_canvas, new Vector2(0f, 0f), "PAUSE BUTTON");
//pausebutton = CreateButton(_canvas, new Vector2(0f, 0f), "DISABLE SCORE");
//Logger.log.Debug("Created Button");

//BS_Utils.Utilities.BSEvents.gameSceneActive += HideSessionTime;
//BS_Utils.Utilities.BSEvents.menuSceneActive += ShowSessionTime;
//}


/*private static TextMeshProUGUI CreateText(Canvas canvas, Vector2 position, string text)
       {
           GameObject gameObject = new GameObject("CustomUIText");
           gameObject.SetActive(false);
           TextMeshProUGUI textMeshProUgui = gameObject.AddComponent<TextMeshProUGUI>();

           textMeshProUgui.rectTransform.SetParent(canvas.transform, false);
           textMeshProUgui.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
           textMeshProUgui.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
           textMeshProUgui.rectTransform.sizeDelta = new Vector2(1f, 1f);
           textMeshProUgui.rectTransform.transform.localPosition = Vector3.zero;
           textMeshProUgui.rectTransform.anchoredPosition = position;

           textMeshProUgui.text = text;
           textMeshProUgui.fontSize = 0.15f;
           textMeshProUgui.color = Color.white;
           textMeshProUgui.alignment = TextAlignmentOptions.Center;
           gameObject.SetActive(true);

           return textMeshProUgui;
       }*/

/*private static Button CreateButton(Canvas canvas, Vector2 position, string text)
{
    Logger.log.Debug("In CreateButton");

    GameObject gameObject = new GameObject("Button");
    gameObject.SetActive(false);

    Button pause_button = gameObject.AddComponent<Button>();
    pause_button.SetButtonText(text);
    Logger.log.Debug("#### 3");

    pause_button.transform.parent = canvas.transform;
    pause_button.SetButtonTextSize(0.15f);
    Logger.log.Debug("#### 4");
    //pause_button.transform.localPosition = Vector3.zero;

    //pause_button.colors = new ColorBlock(Color.red)

    gameObject.SetActive(true);
    Logger.log.Debug("#### 5");

    return pause_button;
}*/