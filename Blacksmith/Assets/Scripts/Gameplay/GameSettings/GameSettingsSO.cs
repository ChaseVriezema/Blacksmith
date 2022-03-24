using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettingsSO : ScriptableObject
{
    public Color[] SuitColors;
    public Sprite[] SuitIcons;
    public BlackjackPresenter BlackjackPresenter;
}
