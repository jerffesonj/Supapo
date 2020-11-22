using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    [SerializeField] int indexEnum = 0;
    [SerializeField] GameObject pt1Panel;
    [SerializeField] GameObject pt2Panel;
    [SerializeField] GameObject pt3Panel;
    [SerializeField] GameObject pt4Panel;
    [SerializeField] GameObject pt5Panel;
    [SerializeField] GameObject pt6Panel;
    [SerializeField] GameObject pt7Panel;
    [SerializeField] GameObject pt8Panel;
    [SerializeField] GameObject pt9Panel;
    [SerializeField] GameObject pt10Panel;
    [SerializeField] GameObject pt11Panel;
    [SerializeField] GameObject pt12Panel;
    [SerializeField] GameObject pt13Panel;
    [SerializeField] GameObject pt14Panel;
    [SerializeField] GameObject pt15Panel;
    [SerializeField] GameObject pt16Panel;
    [SerializeField] GameObject pt17Panel;
    [SerializeField] GameObject pt18Panel;

    public enum States
    {
        Pt1 = 0,
        Pt2,
        Pt3,
        Pt4,
        Pt5,
        Pt6,
        Pt7,
        Pt8,
        Pt9,
        Pt10,
        Pt11,
        Pt12,
        Pt13,
        Pt14,
        Pt15,
        Pt16,
        Pt17,
        Pt18

    }

    public States actualState;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        actualState = States.Pt1;
        ChangePanel();
    }

    public void GotoNextPt()
    {
        indexEnum += 1;

        actualState = (States)indexEnum;

        ChangePanel();
        
    }

    public void EndTutorial()
    {
        ChangeLoadScreenScene.instance.SetSceneName("Menu");
        GoToLoadScreen();
    }

    public void GoToLoadScreen()
    {
        SceneManager.LoadScene("LoadScreen");
    }

    void ChangePanel()
    {
        switch (actualState)
        {
            case States.Pt1:
                pt1Panel.SetActive(true);

                break;
            case States.Pt2:
                pt1Panel.SetActive(false);
                pt2Panel.SetActive(true);

                break;
            case States.Pt3:
                pt2Panel.SetActive(false);
                pt3Panel.SetActive(true);

                break;
            case States.Pt4:
                pt3Panel.SetActive(false);
                pt4Panel.SetActive(true);

                break;
            case States.Pt5:
                pt4Panel.SetActive(false);
                pt5Panel.SetActive(true);

                break;
            case States.Pt6:
                pt5Panel.SetActive(false);
                pt6Panel.SetActive(true);

                break;
            case States.Pt7:
                pt6Panel.SetActive(false);
                pt7Panel.SetActive(true);

                break;
            case States.Pt8:
                pt7Panel.SetActive(false);
                pt8Panel.SetActive(true);

                break;
            case States.Pt9:
                pt8Panel.SetActive(false);
                pt9Panel.SetActive(true);

                break;
            case States.Pt10:
                pt9Panel.SetActive(false);
                pt10Panel.SetActive(true);

                break;
            case States.Pt11:
                pt10Panel.SetActive(false);
                pt11Panel.SetActive(true);

                break;
            case States.Pt12:
                pt11Panel.SetActive(false);
                pt12Panel.SetActive(true);

                break;
            case States.Pt13:
                pt12Panel.SetActive(false);
                pt13Panel.SetActive(true);

                break;
            case States.Pt14:
                pt13Panel.SetActive(false);
                pt14Panel.SetActive(true);

                break;
            case States.Pt15:
                pt14Panel.SetActive(false);
                pt15Panel.SetActive(true);

                break;
            case States.Pt16:
                pt15Panel.SetActive(false);
                pt16Panel.SetActive(true);

                break;
            case States.Pt17:
                pt16Panel.SetActive(false);
                pt17Panel.SetActive(true);

                break;
            case States.Pt18:
                pt17Panel.SetActive(false);
                pt18Panel.SetActive(true);

                break;
        }
    }

}

