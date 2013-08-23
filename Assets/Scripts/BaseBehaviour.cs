using UnityEngine;
using System;
using System.Collections;

public class BaseBehaviour : MonoBehaviour {

    public void Invoke(Action task, float time) {
        base.Invoke(task.Method.Name, time);
    }

    public new void Invoke(string methodName, float time) {
        UnsafeMethod();
        base.Invoke(methodName, time);
    }

    public void InvokeRepeating(Action task, float time, float repeatRate) {
        base.InvokeRepeating(task.Method.Name, time, repeatRate);
    }

    public new void InvokeRepeating(string methodName, float time, float repeatRate) {
        UnsafeMethod();
        base.InvokeRepeating(methodName, time, repeatRate);
    }

    public bool IsIvoking(Action task) {
        return base.IsInvoking(task.Method.Name);
    }

    public new bool IsInvoking(string methodName) {
        UnsafeMethod();
        return base.IsInvoking(methodName);
    }

    public void CancelInvoke(Action task) {
        base.CancelInvoke(task.Method.Name);
    }

    public new void CancelInvoke(string methodName) {
        UnsafeMethod();
        base.CancelInvoke(methodName);
    }

    public Coroutine StartCoroutine(Func<IEnumerator> task) {
        return base.StartCoroutine(task.Method.Name);
    }

    public Coroutine StartCoroutine<T>(Func<T, IEnumerator> task, T value) {
        return base.StartCoroutine(task.Method.Name, value);
    }

    public new void StartCoroutine(string methodName) {
        UnsafeMethod();
        base.StartCoroutine(methodName);
    }

    public new void StartCoroutine(string methodName, object value) {
        UnsafeMethod();
        base.StartCoroutine(methodName, value);
    }

    public I GetInterfaceComponent<I>() where I : class {
        return base.GetComponent(typeof(I)) as I;
    }

    public I GetInterfaceComponentInChildren<I>() where I : class {
        return base.GetComponentInChildren(typeof(I)) as I;
    }

    public I[] GetInterfaceComponents<I>() where I : class {
        return base.GetComponents(typeof(I)) as I[];
    }

    public I[] GetInterfaceComponentsInChildren<I>() where I : class {
        return base.GetComponentsInChildren(typeof(I)) as I[];
    }

    public I[] GetInterfaceComponentsInChildren<I>(bool includeInactive) where I : class {
        return base.GetComponentsInChildren(typeof(I), includeInactive) as I[];
    }

    public static I FindObjectOfInterface<I>() where I : class {
        return MonoBehaviour.FindObjectOfType(typeof(I)) as I;
    }

    public static I[] FindObjectsOfInterface<I>() where I : class {
        return MonoBehaviour.FindObjectsOfType(typeof(I)) as I[];
    }

    public static T Instantiate<T>(T original) where T : UnityEngine.Object {
        return MonoBehaviour.Instantiate(original) as T;
    }

    public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : UnityEngine.Object {
        return MonoBehaviour.Instantiate(original, position, rotation) as T;
    }

    public static new UnityEngine.Object Instantiate(UnityEngine.Object original) {
        UnsafeMethod();
        return MonoBehaviour.Instantiate(original);
    }

    public static new UnityEngine.Object Instantiate(UnityEngine.Object original, Vector3 position, Quaternion rotation) {
        UnsafeMethod();
        return MonoBehaviour.Instantiate(original, position, rotation);
    }

    private static void UnsafeMethod() {
        Log.Wrn("Use of unsafe method detected");
    }
}
