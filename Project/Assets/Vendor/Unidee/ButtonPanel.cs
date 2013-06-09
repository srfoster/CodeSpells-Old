using UnityEngine;



public class ButtonPanel {
    private Rectangle groupRect;
    private Rectangle backRect;
    private Rectangle removeRect;
    private Texture2D spellbookTexture;
    private IDE ide;
    
    public ButtonPanel(IDE idething) {
        ide = idething;
        spellbookTexture = Resources.Load("SpellbookIcon") as Texture2D;
        // old button location
        //groupRect = new Rectangle(Screen.width*3/4+5,0,Screen.width*1/4-5,100); //Screen.height);
        // top left corner
        groupRect = new Rectangle(5,0,Screen.width*1/4-5,100);
        backRect = new Rectangle(10,15,130,65);
        removeRect = new Rectangle(180,15,65,65);
    }
    
    public void draw() {
        GUI.BeginGroup(groupRect.getRect());
        ide.checkBackButton(backRect.getRect());
        ide.checkRemoveButton(removeRect.getRect());
        GUI.EndGroup();
    }
    
}
