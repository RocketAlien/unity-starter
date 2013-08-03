using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class Util {

    //replace whitespace characters with underscores
    public static string TrimWhiteSpace(string text) {
        return Regex.Replace(text.Trim(), "\\s+", "_");
    }

    public static string Format(string text, params object[] args) {
        return args.Length > 0 ? string.Format(text, args) : text;
    }

    public static string Base64Encode(string text) {
        return Convert.ToBase64String(Encoding.ASCII.GetBytes(text));
    }

    public static string Base64Decode(string text) {
        return Encoding.ASCII.GetString(Convert.FromBase64String(text));
    }

    public static string GetTimeStamp() {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff");
    }

    public static string GetStackTrace() {
        var trace = new System.Diagnostics.StackTrace(true);
        return trace.ToString();
    }

    public static string GetTempFilePath(string fileName) {
        return Path.Combine(Application.temporaryCachePath, fileName);
    }
}
