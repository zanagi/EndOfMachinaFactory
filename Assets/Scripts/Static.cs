using UnityEngine;
using System.Collections;

public static class Static
{
    public static string ReplaceAt(this string input, int index, char newChar)
    {
        char[] chars = input.ToCharArray();
        chars[index] = newChar;
        return new string(chars);
    }

    public static bool IsBasic(this Resource resource)
    {
        return resource != Resource.XYZ;
    }
}
