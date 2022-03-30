using System;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionResizer : MonoBehaviour {
    public enum ResizeSource { 
        ScreenSize,     // System info
        Customize,
    }

    #region Serialized Fields
    [SerializeField]
    private ResizeSource _rs;

    [SerializeField]
    private CanvasScaler _cs = null;
    #endregion

    #region Exposed Fields
    public Action onChanged;
    #endregion

    #region Internal Fields
    private ResizeSource _lastRS;
    private ScreenOrientation _lastScreenOri;
    private Vector2 _lastResolution;
    #endregion

    #region Mono Behaviours Hooks
    private void Update() {
        Refresh();
    }
    #endregion

    #region Internal Methods
    private void Refresh() {
        if (_cs == null) {
            return;
        }

        if (!IsResizeSourceChanged() && !IsScreenOrientationChanged() && !IsResolutionChanged()) {
            return;
        }

        _lastRS = _rs;
        _lastScreenOri = Screen.orientation;
        _lastResolution = new Vector2(Screen.width, Screen.height);

        if (_rs == ResizeSource.ScreenSize) {
            _cs.referenceResolution = new Vector2(Screen.width, Screen.height);
            _cs.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            _cs.matchWidthOrHeight = Screen.width > Screen.height ? 0 : 1;
        }
        else if (_rs == ResizeSource.Customize) {
            Vector2 resolution = ResolutionUtility.GetCustomizedReferenceResolution();
            float match = ResolutionUtility.GetMatchWidthOrHeight();

            _cs.referenceResolution = new Vector2(resolution.x, resolution.y);
            _cs.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            _cs.matchWidthOrHeight = match;
        }

        if (onChanged != null) {
            onChanged();
        }
    }

    private bool IsResizeSourceChanged() {
        return _lastRS != _rs;
    }

    private bool IsScreenOrientationChanged() {
        return _lastScreenOri != Screen.orientation;
    }

    private bool IsResolutionChanged() {
        return _lastResolution != new Vector2(Screen.width, Screen.height);
    }
    #endregion
}
