using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public Color[] SuitColors;
    public Sprite[] SuitIcons;

    public BlackjackPresenter BlackjackPresenter;

    public static GameSettings Instance;

    public void Awake()
    {
        Instance = this;
    }

}
