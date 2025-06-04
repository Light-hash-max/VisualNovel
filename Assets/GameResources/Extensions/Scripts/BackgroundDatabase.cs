using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundDatabase : MonoBehaviour
{
    private static Dictionary<string, Sprite> _backgrounds = new Dictionary<string, Sprite>();

    public static void Initialize()
    {
        _backgrounds.Add("Day", Resources.Load<Sprite>("Background/Day"));
        _backgrounds.Add("Night", Resources.Load<Sprite>("Background/Night"));
    }

    public static Sprite GetSprite(string background) =>
        _backgrounds[background];
}
