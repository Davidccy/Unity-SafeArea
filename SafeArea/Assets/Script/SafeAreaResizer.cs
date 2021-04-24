using UnityEngine;

public class SafeAreaResizer : MonoBehaviour {
    public enum ResizeSource {
        ScreenSize,
        Customize,
    }

    #region Serialized Fields
    [SerializeField]
    private ResizeSource _rs;
    #endregion

    #region Internal Fields
    private RectTransform _rt = null;
    private ScreenOrientation _lastScreenOri;
    private Vector2 _lastResolution;
    #endregion

    #region Mono Behaviour Hooks
    private void Awake() {
        _rt = GetComponent<RectTransform>();
    }

    private void Update() {
        Refresh();
    }
    #endregion

    #region Internal Methods
    private void Refresh() {
        if (_rt == null) {
            return;
        }

        if (!IsScreenOrientationChanged() && !IsResolutionChanged()) {
            return;
        }

        _lastScreenOri = Screen.orientation;
        _lastResolution = new Vector2(Screen.width, Screen.height);

        if (_rs == ResizeSource.ScreenSize) {
            _rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, Screen.safeArea.x, Screen.safeArea.width);
            _rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, Screen.safeArea.y, Screen.safeArea.height);
        }
        else if (_rs == ResizeSource.Customize) {
            Rect scaledSafeArea = ResolutionUtility.GetScaledSafeArea();
            _rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, scaledSafeArea.x, scaledSafeArea.width);
            _rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, scaledSafeArea.y, scaledSafeArea.height);
        }        
    }

    private bool IsScreenOrientationChanged() {
        return _lastScreenOri != Screen.orientation;
    }

    private bool IsResolutionChanged() {
        return _lastResolution != new Vector2(Screen.width, Screen.height);
    }
    #endregion
}
