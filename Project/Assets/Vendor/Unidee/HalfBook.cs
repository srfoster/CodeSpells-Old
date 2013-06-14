using UnityEngine;



public class HalfBook {
    private Rectangle bookRect;
    private Rectangle textRect;
    private Rectangle prevRect;
    private Rectangle nextRect;
    private Spellbook spellbook;
    
    public HalfBook() {
        //this.ide = ide;
        bookRect = new Rectangle(0, 0, Screen.width, Screen.height);
        resize(0.75f, 0.75f, "left", "bottom");
        textRect = new Rectangle(bookRect.x + bookRect.w * 0.07f, bookRect.y + bookRect.h * 0.14f, bookRect.w * 0.383f, bookRect.h * 0.725f);
        float buttonw = 35.0f * bookRect.w / Screen.width;
        float buttonh = 35.0f * bookRect.h / Screen.height;
        prevRect = new Rectangle(bookRect.x + bookRect.w * 0.025f, bookRect.y + bookRect.h * 0.5f, buttonw, buttonh);
        nextRect = new Rectangle(bookRect.x + bookRect.w * 0.95f / 2, bookRect.y + bookRect.h * 0.5f, buttonw, buttonh);
        spellbook = GameObject.Find("Spellbook").GetComponent<Spellbook>();
    }
    
    public void draw() {
        //GUI.BeginGroup(bookRect.getRect());
        //spellbook.displayCurrentPage();
        //GUI.EndGroup();
        spellbook.displayScaledPage(this);
        spellbook.pageChangeButtons(prevRect.getRect(), nextRect.getRect());
    }
    
    public void resize(float x, float y, string clampx, string clampy) {
        float oldw = bookRect.w;
        float oldh = bookRect.h;
        bookRect.w *= x;
        bookRect.h *= y;
        // clampx == "left" doesn't change x
        if (clampx == "right")
            bookRect.x += (oldw - bookRect.w);
        else if (clampx == "center")
            bookRect.x += (oldw - bookRect.w)/2;
        // clampy == "top" doesn't change y
        if (clampy == "bottom")
            bookRect.y += (oldh - bookRect.h);
        else if (clampy == "center")
            bookRect.y += (oldh - bookRect.h)/2;
    }
    
    public Rect getBookRect() {
        return bookRect.getRect();
    }
    
    public Rect getTextRect() {
        return textRect.getRect();
    }
    
    public void setFocus() {
        spellbook.show(GameObject.Find("IDE"));
    }
}