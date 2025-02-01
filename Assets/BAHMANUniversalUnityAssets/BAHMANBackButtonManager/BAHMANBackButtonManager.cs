/*
 BAHMAN Back Button Handler V.1.1
this module handles back button on mobile devices and Escape button on desktops
It should be loaded in the very first scene because it won't be destroyed on load.
use the prefab provided in this folder to modify the look of the message box.
*/

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


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
    const string TITLETAG = "", MESSAGETAG = "Do You Want To Quit Game Or Go To Home Screen?", EXITTAG = "Exit", HOMETAG = "Home";
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

    [SerializeField] InputAction _Escape;

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

        _Escape.Enable();
        _Escape.performed += _Escape_performed;
    }

    private void _Escape_performed(InputAction.CallbackContext obj)
    {
        if (!_isBackPanelActive)

        {
            _showPanel();
        }
        else
        {
            _closeClicked();
        }
    }

    //void Update()
    //{
    //    //_readKeyboad();
    //}
    /// <summary>
    /// check the keyboard input and search for the ESCP key
    /// </summary>
    //void _readKeyboad()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        /// if ESCP pressede decide to show the panel
    //        if (!_isBackPanelActive)

    //        {
    //            _isBackPanelActive = true;
    //            _ShowMenu();
    //            OnBackButtonMenuShowed?.Invoke();
    //        }
    //    }
    //}
    /// <summary>
    /// force to show the back buttom menu
    /// </summary>
    public void _ShowMenu()
    {
        _isBackPanelActive = true;
        BAHMANMessageBoxManager._INSTANCE._ShowConfirmBox("Exit?"
            , "Do You Want To Quit?"
            , "Exit"
            , true, true,
            _closeClicked,
            _Exit);
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
        BAHMANMessageBoxManager._INSTANCE._HideConfirmationPanel();
        OnBackButtonMenuHide?.Invoke();
    }
    void _showPanel()
    {
        _isBackPanelActive = true;
        _ShowMenu();
        OnBackButtonMenuShowed?.Invoke();
    }

}

