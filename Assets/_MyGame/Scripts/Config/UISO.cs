using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/UI/" + nameof(UISO), fileName = nameof(UISO))]
public sealed class UISO : ScriptableObject
{
   [field: SerializeField] public GameObject MainMenuGo { get; private set; }
   [field:SerializeField] public  GameObject SettingMenuGo { get; private set; }
}
