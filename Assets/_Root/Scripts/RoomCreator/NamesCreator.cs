using Random = UnityEngine.Random;

public class NamesCreator
{
    private static string[] _firstName;
    private static string[] _secondName;

    public NamesCreator(NamesConfig namesConfig)
    {
        Init(namesConfig);
    }

    private void Init(NamesConfig namesConfig)
    {
        _firstName = new string[namesConfig.FirstName.Length + 1];
        _secondName = new string[namesConfig.SecondName.Length + 1];

        for (var i = 0; i < namesConfig.FirstName.Length; i++)
        {
            _firstName[i] = namesConfig.FirstName[i];
        }

        for (var i = 0; i < namesConfig.SecondName.Length; i++)
        {
            _secondName[i] = namesConfig.SecondName[i];
        }
    }

    public static string GetRandomName()
    {
        var firstName = Random.Range(0, _firstName.Length);
        var secondName = Random.Range(0, _secondName.Length);
        var result = firstName + " " + secondName;

        return result;
    }

}