using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "NewSettings", order = 2, menuName = "BAHMAN Unity Assets/New Game Settings")]
public class GameSettingInfo : ScriptableObject
{
    private static GameSettingInfo gameSetting;
    public static GameSettingInfo Instance
    {

        get
        {
            if (gameSetting == null)
            {
                gameSetting = Resources.Load<GameSettingInfo>("GameSettings");
            }
            return gameSetting;
        }
    }
    const string SOUNDFXTAG = "SoundFXTag", ANTIALIASINGTAG = "AntiAliasingTag", MUSICTAG = "MUSICTag";
    public static event UnityAction<bool> OnSoundFXChange;
    public static event UnityAction<bool> OnMusicChange;
    public static event UnityAction<bool> OnAntialiasingChange;

    [SerializeField] bool antiAliasing = true;
    [SerializeField] bool soundFX = true;
    [SerializeField] bool music = true;
    public bool SoundFX
    {
        get
        {
            return BAHMANLogicLayer.Tools.IntToBool(PlayerPrefs.GetInt(SOUNDFXTAG, BAHMANLogicLayer.Tools.BoolToInt(soundFX)));

        }
        set
        {
            OnSoundFXChange?.Invoke(value);
            PlayerPrefs.SetInt(SOUNDFXTAG, BAHMANLogicLayer.Tools.BoolToInt(value));
        }
    }
    public bool Music
    {
        get
        {
            return BAHMANLogicLayer.Tools.IntToBool(PlayerPrefs.GetInt(MUSICTAG, BAHMANLogicLayer.Tools.BoolToInt(music)));

        }
        set
        {
            OnMusicChange?.Invoke(value);
            PlayerPrefs.SetInt(MUSICTAG, BAHMANLogicLayer.Tools.BoolToInt(value));
        }
    }
    public bool AntiAliasing
    {
        get
        {

            return BAHMANLogicLayer.Tools.IntToBool(PlayerPrefs.GetInt(ANTIALIASINGTAG, BAHMANLogicLayer.Tools.BoolToInt(antiAliasing)));

        }
        set
        {
            OnAntialiasingChange?.Invoke(value);
            PlayerPrefs.SetInt(ANTIALIASINGTAG, BAHMANLogicLayer.Tools.BoolToInt(value));
        }
    }


    const string GameLevelSaveTag= "GameLevelSaveTag";


    public bool IsGameWon;
    public int ThisRunScore;

    [SerializeField] int currentGameLevel;

    public int CurrentGameLevel
    {
        get
        {
            return PlayerPrefs.GetInt(GameLevelSaveTag, currentGameLevel);
        }
        set
        {
            PlayerPrefs.SetInt(GameLevelSaveTag, value);
        }
    }
    
    

}


