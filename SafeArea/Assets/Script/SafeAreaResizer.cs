using UnityEngine;

public class SafeAreaResizer : MonoBehaviour {
    public enum ResizeSource {
        ScreenSize,     // System info
        Customize,
    }

    #region Serialized Fields
    [SerializeField]
    private ResizeSource _rs;
    [SerializeField]
    private ResolutionResizer _rr = null;
    #endregion

    #region Internal Fields
    private RectTransform _rt = null;
    private ResizeSource _lastRS;
    private bool _rrChanged = false;
    #endregion

    #region Mono Behaviour Hooks
    private void Awake() {
        _rt = GetComponent<RectTransform>();
        _rr.onChanged += OnResolutionResizerChanged;
    }

    private void Update() {
        Refresh();
    }

    private void OnDestroy() {
        _rr.onChanged -= OnResolutionResizerChanged;
    }
    #endregion

    #region CallBack handlings
    private void OnResolutionResizerChanged() {
        _rrChanged = true;
    }
    #endregion

    #region Internal Methods
    private void Refresh() {
        if (_rt == null) {
            return;
        }

        if (!IsResizeSourceChanged() && !_rrChanged) {
            return;
        }

        _lastRS = _rs;

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

    private bool IsResizeSourceChanged() {
        return _lastRS != _rs;
    }
    #endregion
}
