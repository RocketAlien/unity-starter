using UnityEngine;

public class Log {

    public static void Msg(string text, params object[] args) {
        Debug.Log(Util.Format(text, args));
    }

    public static void Dbg(string text, params object[] args) {
        if (Debug.isDebugBuild) {
            Debug.Log(Util.GetTimeStamp() + " DBG: " + Util.Format(text, args));
        }
    }

    public static void Wrn(string text, params object[] args) {
        Debug.LogWarning(Util.GetTimeStamp() + " WRN: " + Util.Format(text, args));
    }

    public static void Err(string text, params object[] args) {
        Debug.LogError(Util.GetTimeStamp() + " ERR: " + Util.Format(text, args));
    }
}
