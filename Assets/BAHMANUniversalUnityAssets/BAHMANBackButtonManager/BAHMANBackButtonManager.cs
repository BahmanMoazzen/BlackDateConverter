/*
 BAHMAN Back Button Handler V.1.1
this module handles back button on mobile devices and Escape button on desktops
It should be loaded in the very first scene because it won't be destroyed on load.
use the prefab provided in this folder to modify the look of the message box.
*/

using UnityEngine;
using UnityEngine.Events;


public class BAHMANBackButtonManager : MonoBehaviour
{
    //events to fire 
    /// <summary>
    /// fires whenever a back button menu shows
    /// </summary>
    public static event UnityAction OnBackButtonMenuShowed;
    /// <summary>
    /// fires wheneever a backbutton menu hides
    /// </summary>
    public static event UnityAction OnBackButtonMenuHide;
    /// <summary>
    /// the constant tags uses for display purposes
    /// </summary>
    const string TITLETAG = "", MESSAGETAG = "Do You Want To Quit Game Or Go To Home Screen?", EXITTAG="Exit", HOMETAG = "Home";
    /// <summary>
    /// the instance needed for singletone use
    /// </summary>
    public static BAHMANBackButtonManager _Instance;
    /// <summary>
    /// if silent mode enables, no window will be displays. just events triggers
    /// </summary>
    [Tooltip("check this if you need just trigger the events")]
    [SerializeField] bool _SilentMode = false;
    /// <summary>
    /// the scene which is the home screen
    /// </summary>
    [Tooltip("the scene name of the home button")]
    [SerializeField] AllScenes _HomeSceneName;

    /// <summary>
    /// the boolean to check if the back button panel is active of not
    /// </summary>
    bool _isBackPanelActive = false;

    

    void Awake()
    {

        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        _readKeyboad();
    }
    /// <summary>
    /// check the keyboard input and search for the ESCP key
    /// </summary>
    void _readKeyboad()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            /// if ESCP pressede decide to show the panel
            if (!_isBackPanelActive)

            {
                _isBackPanelActive = true;
                _ShowMenu();
                OnBackButtonMenuShowed?.Invoke();
            }
        }
    }
    /// <summary>
    /// force to show the back buttom menu
    /// </summary>
    public void _ShowMenu()
    {
        _isBackPanelActive = true;
        BAHMANMessageBoxManager._INSTANCE._ShowYesNoBox("End Game","Do You Want To Quit Game Or Go To Home Screen?","Exit","Home",true,true,_closeClicked,_Exit,_Home);
        OnBackButtonMenuShowed?.Invoke();
    }
    /// <summary>
    /// force application to quit game
    /// </summary>
    public void _Exit()
    {
        Application.Quit();
    }
    /// <summary>
    /// force code to change scene to Home scene
    /// </summary>
    public void _Home()
    {
        _isBackPanelActive = false;
        BAHMANLoadingManager._INSTANCE._LoadScene(_HomeSceneName);

    }
    /// <summary>
    /// the cross image of the back panel is clicked
    /// </summary>
    void _closeClicked()
    {
        _isBackPanelActive = false;
        OnBackButtonMenuHide?.Invoke();
    }

}

