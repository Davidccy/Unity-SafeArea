using UnityEngine;
using TMPro;

public class ResolutionInfo : MonoBehaviour {
    #region Serialized Fields
    [SerializeField]
    private TextMeshProUGUI _textScreenOrientation = null;
    [SerializeField]
    private TextMeshProUGUI _textScreenInfo = null;
    [SerializeField]
    private TextMeshProUGUI _textSafeAreaInfo = null;
    [SerializeField]
    private TextMeshProUGUI _textRectCanvasInfo = null;
    [SerializeField]
    private TextMeshProUGUI _textRectSaveAreaInfo = null;

    [SerializeField]
    private RectTransform _rtCanvas = null;
    [SerializeField]
    private RectTransform _rtSafeArea = null;
    #endregion

    #region Mono Behaviour Hooks
    private void Update() {
        ShowResolutionInfo();
    }
    #endregion

    #region Internal Methods
    private void ShowResolutionInfo() {
        _textScreenOrientation.text = string.Format("Screen orientation = {0}", Screen.orientation);

        _textScreenInfo.text = string.Format("Unity Screen Size: W = {0}, H = {1}", Screen.width, Screen.height);
        _textSafeAreaInfo.text = string.Format("Unity Safe Area: rect = {0}", Screen.safeArea);

        _textRectCanvasInfo.text = string.Format("Rect Canvas Info: w = {0}, h = {1}", _rtCanvas.rect.width, _rtCanvas.rect.height);
        _textRectSaveAreaInfo.text = string.Format("Rect Safe Area Info: w = {0}, h = {1}", _rtSafeArea.rect.width, _rtSafeArea.rect.height);
    }
    #endregion
}
