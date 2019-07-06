using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TokBlitz {
    public static class StringExtensions

{
    static string[] ExludedWords =

{
    "this", "that", "the", "then", "of", "i", "for", "and", "i'll", "are", "you"
}

;
public static string GetEnvironmentVariable(string name) {
    return System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
}

/// <summary >
/// Returns the input string in lowercase alphanumeric format. All spaces are replaced with underscores.
/// </summary >
public static string ToIdFormat(this string item) {
    item = item?.Trim().ToLower().Replace("/", "").Replace(" ", "_").Replace("&", "and").Replace("é", "e");
    item = Regex.Replace(item, "[^0-9A-Za-z]", "");
    return item;
}

/// <summary >
/// Returns the input string with the first character converted to uppercase
/// </summary >
public static string FirstLetterToUpperCase(this string s) {
    if (string.IsNullOrEmpty(s)) throw new ArgumentException("There is no first letter");
    char [] a = s.ToCharArray();
    a [0] = char.ToUpper(a[0]);
    return new string(a);
}

/// <summary >
/// Converts DateTime to Integer
/// </summary >
public static long ToUnixTime(this DateTime dateTime) {
    DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, 0);
    return (int)Math.Round((dateTime - d).TotalSeconds);
}

/// <summary >
/// Converts integer to DateTime
/// </summary >
public static DateTime ToDateTime(this long dateTime) {
    return dateTime.ToDateTime();
}

public static bool CheckIfBan(this string word) {
    bool isBan = false;
    foreach (var banWord in ExludedWords)

{
    if (word.ToIdFormat().ToUpper().Equals(banWord.ToIdFormat().ToUpper()) || word.ToIdFormat().ToUpper().Contains(banWord.ToIdFormat().ToUpper()) )

{
    isBan = true;
    break;
}

else {
    isBan = false;
}

}
return isBan;
}
}
}
