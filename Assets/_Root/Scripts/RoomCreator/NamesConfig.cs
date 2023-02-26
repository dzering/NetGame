using UnityEngine;

[CreateAssetMenu(menuName = "Configs/" + nameof(NamesConfig), fileName = nameof(NamesConfig))]
public sealed class NamesConfig : ScriptableObject
{
    [field: SerializeField] public string[] FirstName { get; private set; }
    [field: SerializeField] public string[] SecondName { get; private set; }
}