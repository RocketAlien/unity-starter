using UnityEngine;
using System;

//TODO: create swipe and gesture recognition component
public class TapDetector : BaseBehaviour {

    public bool UseWorldUnits = true;

    public Action<Vector3> OnTapped;

    private void Update() {
        //works on both desktop and mobile
        bool tapped = false;
        Vector3 position = Vector3.zero;
#if UNITY_EDITOR || UNITY_STANDALONE
        tapped   = Input.GetMouseButtonDown(0);
        position = Input.mousePosition;
#elif UNITY_IPHONE || UNITY_ANDROID
        if (Input.touchCount > 0) {
            TouchPhase phase = Input.touches[0].phase;
            //touches may appear in Moved or Ended states at low frame rates
            //TODO: test on iOS and Android mobile devices
            tapped = phase == TouchPhase.Began || phase == TouchPhase.Moved || phase == TouchPhase.Ended;
            position = Input.touches[0].position;
        }
#endif
        if (tapped && OnTapped != null) {
            //convert screen position to world units if necessary
            if (UseWorldUnits) {
                Camera cam = Camera.main;
                position = cam.ScreenToWorldPoint(position);
            }
            OnTapped(position);
        }
    }
}
