using UnityEngine;

public class SaveScore : MonoBehaviour
{
    public void Save(float Score)
    {
        PlayerPrefs.SetFloat("Score", Score);
    }

    public float Load(float Score)
    {
        return PlayerPrefs.GetFloat("Score");
    }
}
