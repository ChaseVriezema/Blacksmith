using UnityEngine;

public class GameSettings : MonoBehaviour
{

    public GameSettingsSO Settings;
    public static GameSettings Instance;

    public void Awake()
    {
        Instance = this;
    }

}
