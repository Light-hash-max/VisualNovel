using System.Collections.Generic;
using UnityEngine;

public static class CharacterDatabase
{
    private static Dictionary<string, Dictionary<string, Sprite>> _characters = new Dictionary<string, Dictionary<string, Sprite>>();

    private static Dictionary<string, Sprite> valuePairs = new Dictionary<string, Sprite>();

    public static void Initialize()
    {
        valuePairs.Add("neutral", Resources.Load<Sprite>("Girl1/neutral"));
        valuePairs.Add("sad_closedeyes", Resources.Load<Sprite>("Girl1/sad_closedeyes"));
        _characters.Add("Girl1", valuePairs);

        valuePairs.Clear();
        valuePairs.Add("neutral", Resources.Load<Sprite>("Girl2/neutral"));
        valuePairs.Add("sad_closedeyes", Resources.Load<Sprite>("Girl2/sad_closedeyes"));
        _characters.Add("Girl2", valuePairs);
    }

    public static Sprite GetSprite(string characterID, string emotion) =>
        _characters[characterID][emotion];
}
