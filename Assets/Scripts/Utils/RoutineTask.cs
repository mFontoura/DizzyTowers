using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RoutineTask : MonoBehaviour
{
    [SerializeField] private float _timeout = 1f;
    [SerializeField] private UnityEvent _event = null;

    private Coroutine _routine;
    
    private void Start()
    {
        _routine = StartCoroutine(InvokeRoutineTask(_timeout));
    }

    private IEnumerator InvokeRoutineTask(float timeout)
    {
        while (true) {
            yield return new WaitForSeconds(timeout);
            _event.Invoke();
        }
    }

    public void StopTask()
    {
        StopCoroutine(_routine);
    }
}
