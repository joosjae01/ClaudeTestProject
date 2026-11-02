using UnityEngine;

[CreateAssetMenu(fileName = "ModeData", menuName = "Data/ModeData")]
public class ModeData : ScriptableObject
{
    [SerializeField, TextArea] private string _description;
    [SerializeField] private string _sceneName;

    public string Description => _description;
    public string SceneName => _sceneName;
}
