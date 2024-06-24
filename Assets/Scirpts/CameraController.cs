using System.Collections;
using UnityEngine;

public class Controller
{
    public IEnumerator MoveObjectToPosition(GameObject target, Vector3 targetPosition, float duration)
    {
        yield return Interpolate(target.transform.position, targetPosition, duration,
                                 t => target.transform.position = t);
    }

    public IEnumerator RotateObjectToRotation(GameObject target, Quaternion targetRotation, float duration)
    {
        yield return Interpolate(target.transform.rotation, targetRotation, duration,
                                 t => target.transform.rotation = t);
    }

    public void ToNextCamera(Camera currentCamera, Camera nextCamera)
    {
        nextCamera.gameObject.SetActive(true);
        nextCamera = Camera.main;
        currentCamera.gameObject.SetActive(false);
    }

    public void CameraZoomIn(Camera currentCamera)
    {

    }

    private IEnumerator Interpolate<T>(T startValue, T targetValue, float duration, System.Action<T> setter)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            setter.Invoke(InterpolateValue(startValue, targetValue, t));
            yield return null;
        }

        setter.Invoke(targetValue);
    }

    private T InterpolateValue<T>(T startValue, T targetValue, float t)
    {
        if (startValue is Vector3)
        {
            return (T)(object)Vector3.Lerp((Vector3)(object)startValue, (Vector3)(object)targetValue, t);
        }
        else if (startValue is Quaternion)
        {
            return (T)(object)Quaternion.Slerp((Quaternion)(object)startValue, (Quaternion)(object)targetValue, t);
        }
        else
        {
            return targetValue;
        }
    }
}
