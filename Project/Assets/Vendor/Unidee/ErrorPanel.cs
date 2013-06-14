using UnityEngine;
using System.Text.RegularExpressions;


//TODO: change to scrolling, uneditable text
//      add label with error count

public class ErrorPanel {
    private Rectangle errorRect;
    private Rectangle bgRect;
    private Rectangle labelRect;
    private IDE ide;
    private Vector2 scroll_position = Vector2.zero;
    Texture2D backgroundTexture;
    GUIStyle style = new GUIStyle();
    GUIStyle labelStyle = new GUIStyle();
    
    public ErrorPanel(IDE idething) {
        ide = idething;
        //errorRect = new Rectangle(Screen.width*3/4+15, 100, 230, 500);
        //errorRect = new Rectangle(Screen.width*1/2+15, Screen.height*4/5+5, Screen.width*1/2 - 30, Screen.height*1/5 - 10);
        bgRect = new Rectangle(Screen.width*7/16, Screen.height*4/5+5, Screen.width*1/2, Screen.height*1/5 - 10);
        setTextAreaRect();
        style.fontSize = 12;
		style.normal.textColor = Color.black;
		style.alignment = TextAnchor.UpperLeft;
		style.wordWrap = true;
		labelStyle.fontSize = 30;
		labelStyle.normal.textColor = Color.red;
		labelStyle.alignment = TextAnchor.LowerCenter;
		labelRect = new Rectangle(bgRect.x, bgRect.y + bgRect.h/4, errorRect.x - bgRect.x, bgRect.h / 2);
    }
    
    private void setTextAreaRect() {
        errorRect.x = bgRect.x + ((float) 125/(Screen.width*3/4))*bgRect.w;
        errorRect.y = bgRect.y + ((float) 40/Screen.height)*bgRect.h;
        errorRect.w = bgRect.w - 2*(errorRect.x - bgRect.x);
        errorRect.h = bgRect.h - 2*(errorRect.y - bgRect.y);
    }
    
    public void draw() {
        GUI.DrawTexture(bgRect.getRect(), backgroundTexture, ScaleMode.StretchToFill);
        
        // begin rotation
        GUIUtility.RotateAroundPivot (-90, new Vector2(bgRect.x + (errorRect.x - bgRect.x)/2, bgRect.y + (bgRect.h / 2)));
        GUI.Label(labelRect.getRect(), "Errors", labelStyle);
        GUIUtility.RotateAroundPivot (90, new Vector2(bgRect.x + (errorRect.x - bgRect.x)/2, bgRect.y + (bgRect.h / 2)));
        // end rotation
        
        GUILayout.BeginArea(errorRect.getRect());
        scroll_position = GUILayout.BeginScrollView (scroll_position, GUILayout.Width(errorRect.w), GUILayout.Height(errorRect.h));
        //GUIStyle style = GUI.skin.box;
		
		string pattern = @"^(.+) class (.+) is public, should be declared in a file named (.+)";
		string current_error = ide.getCurrentError();
		if (!Regex.IsMatch(current_error, pattern)) {
		    //GUILayout.TextArea(current_error, style);
			GUILayout.Box(/*errorRect.getRect(),*/ current_error, style);
		} else {
		    //GUILayout.TextArea("------------", style);
			GUILayout.Box(/*errorRect.getRect(),*/ "------------", style);
		}
		GUILayout.EndScrollView ();
        GUILayout.EndArea();
    }
    
    public void setTexture(Texture2D bgTexture) {
        backgroundTexture = bgTexture;
    }
    
//     public void draw() {
//         GUI.DrawTexture(bgRect.getRect(), backgroundTexture, ScaleMode.StretchToFill);
//         
//     }
    
    public void scale(float x, float y) {
        errorRect.w *= x;
        errorRect.h *= y;
    }
    
    public void move(float x, float y) {
        errorRect.x += x;
        errorRect.y += y;
    }
    
    public Rect getErrorRect() {
        return bgRect.getRect();
    }

}
