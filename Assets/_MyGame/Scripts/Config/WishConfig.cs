using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(menuName = "Configs/Unility/" + nameof(WishConfig), fileName = nameof(WishConfig))]
public class WishConfig : ScriptableObject
{
    [SerializeField] private string _text;
    private string[] _wishes;


    private void OnEnable()
    {
        if(_text == null)
            return;
        _wishes = _text.Split('!');
    }

    public string GetRandomText()
    {
        var text =_wishes[Random.Range(0, _wishes.Length)];
        return text;
    }
}
