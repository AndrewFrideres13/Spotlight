using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuController : MonoBehaviour {
    public Button strtBtn;
    public Button quitBtn;
    public Image titleFadeImg;
    public Image uiFadeImg;
    public GameManager gManager;
    private bool isFinished;

    void Start () {
        quitBtn.onClick.AddListener (() => buttonCallBack (quitBtn));
        strtBtn.onClick.AddListener (() => buttonCallBack (strtBtn));
        titleFadeImg.CrossFadeAlpha (0.0f, 5.0f, true);
        Invoke ("fadeUIIn", 5);
    }

    private void fadeUIIn () {
        uiFadeImg.CrossFadeAlpha (0.0f, 2.0f, true);
        Invoke ("FadeComplete", 2);
    }

    private void FadeComplete () {
        uiFadeImg.gameObject.SetActive (false); //Do this so we can click buttons
    }

    private void buttonCallBack (Button buttonPressed) {
        if (buttonPressed == strtBtn) {
            SceneManager.LoadScene (GameManager.LevelNumb);
        } else if (buttonPressed == quitBtn) {
            Application.Quit ();
            System.Diagnostics.Process.GetCurrentProcess ().Kill ();
        }
    }
}