using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomObserverEventHandler : DefaultObserverEventHandler
{
    protected override void OnTrackingFound() {
        OnTargetFound?.Invoke();
    }

    protected override void OnTrackingLost() {
        OnTargetLost?.Invoke();
    }
}
