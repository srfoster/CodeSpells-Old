using UnityEngine;



public class EditorPanel {
    private Rectangle backgroundRect;
    private Texture2D backgroundTexture;
    private Rectangle textAreaRect;
    private float scrollWidth, scrollHeight;
    private IDE ide;
    
    public EditorPanel(IDE idething) {
        ide = idething;
        //backgroundRect = new Rectangle(0,0,Screen.width*3/4,Screen.height);
        // half screen, on right
        //backgroundRect = new Rectangle(Screen.width*1/2, 0, Screen.width*1/2, Screen.height*4/5);
        backgroundRect = new Rectangle(Screen.width*3/8, 0, Screen.width*5/8, Screen.height*4/5);
        textAreaRect = new Rectangle();
        setTextAreaRect();
        setScrollDims();
    }
    
    private void setTextAreaRect() {
        textAreaRect.x = backgroundRect.x + ((float) 125/(Screen.width*3/4))*backgroundRect.w;
        textAreaRect.y = backgroundRect.y + ((float) 40/Screen.height)*backgroundRect.h;
        textAreaRect.w = backgroundRect.w - 2*(textAreaRect.x - backgroundRect.x);
        textAreaRect.h = backgroundRect.h - 2*(textAreaRect.y - backgroundRect.y);
    }
    
    public Rect getTextAreaRect() {
        return textAreaRect.getRect();
    }
    
    public Rect getEditorRect() {
        return backgroundRect.getRect();
    }
    
    public float getScrollWidth() {
        return scrollWidth;
    }
    
    public float getScrollHeight() {
        return scrollHeight;
    }
    
    private void setScrollDims() {
        scrollWidth = textAreaRect.w; //backgroundRect.w - ((float) 200 / (Screen.width*3/4));
        scrollHeight = textAreaRect.h; // backgroundRect.h - ((float) 60 / Screen.height);
    }
    
    public void setTexture(Texture2D bgTexture) {
        backgroundTexture = bgTexture;
    }
    
    public void draw(bool displayCode) {
        GUI.DrawTexture(backgroundRect.getRect(), backgroundTexture, ScaleMode.StretchToFill);
        if (displayCode) {
            ide.showHighlightedCode();	
		}
    }
    
    public void scale(float xscale, float yscale) {
        backgroundRect.w = backgroundRect.w * xscale;
        backgroundRect.h = backgroundRect.h * yscale;
        setTextAreaRect();
        setScrollDims();
    }
    
    public void move(float x, float y) {
        backgroundRect.x += x;
        backgroundRect.y += y;
        setTextAreaRect();
        setScrollDims();
    }
    
    public bool isWithin(Vector2 pos) {
        if (backgroundRect.x <= pos.x && backgroundRect.x+backgroundRect.w >= pos.x && backgroundRect.y <= pos.y && backgroundRect.y + backgroundRect.h >= pos.y)
            return true;
        return false;
    }
}