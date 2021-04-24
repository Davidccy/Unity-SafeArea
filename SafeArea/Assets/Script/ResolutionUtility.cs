using UnityEngine;

public static class ResolutionUtility {
    public static bool IsScreenPortrait() {
        return Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown;
    }

    public static Vector2 GetCustomizedReferenceResolution() {
        bool isScreenPortrait = IsScreenPortrait();
        float customizedWidth = isScreenPortrait ? Define.CUSTOMIZED_SCREEN_WIDTH : Define.CUSTOMIZED_SCREEN_HEIGHT;
        float customizedHeight = isScreenPortrait ? Define.CUSTOMIZED_SCREEN_HEIGHT : Define.CUSTOMIZED_SCREEN_WIDTH;

        return new Vector2(customizedWidth, customizedHeight);
    }

    public static float GetMatchWidthOrHeight() {
        Vector2 customizedResolution = GetCustomizedReferenceResolution();

        float screenWidthScale = (float) Screen.width / customizedResolution.x;
        float screenHeightScale = (float) Screen.height / customizedResolution.y;
        float match = screenWidthScale > screenHeightScale ? 1 : 0;

        return match;
    }

    public static Rect GetScaledSafeArea() {
        Vector2 customizedResolution = GetCustomizedReferenceResolution();

        float screenWidthScale = (float) Screen.width / customizedResolution.x;
        float screenHeightScale = (float) Screen.height / customizedResolution.y;
        float scale = screenWidthScale > screenHeightScale ? screenHeightScale : screenWidthScale;

        Rect safeArea = Screen.safeArea;
        return new Rect(safeArea.x / scale, safeArea.y / scale, safeArea.width / scale, safeArea.height / scale);
    }
}
