using UnityEngine;



public class ButtonPanel {
    private Rectangle groupRect;
    private Rectangle backRect;
    private Rectangle removeRect;
    private Rectangle newSpellRect;
    private IDE ide;
    
    public ButtonPanel(IDE idething) {
        ide = idething;
        // old button location
        //groupRect = new Rectangle(Screen.width*3/4+5,0,Screen.width*1/4-5,100); //Screen.height);
        // top left corner
        groupRect = new Rectangle(5,0,Screen.width*3/8-5,Screen.height/4);
        // upper left
        //backRect = new Rectangle(10,15,130,65);
        //removeRect = new Rectangle(180,15,65,65);
        // upper right
        removeRect = new Rectangle(groupRect.w-75,15,64,64);
        backRect = new Rectangle(removeRect.x-170,15,130,64);
        //newSpellRect = new Rectangle(removeRect.x, removeRect.y+removeRect.h+15, 64, 64);
    }
    
    public void draw() {
        GUI.BeginGroup(groupRect.getRect());
        ide.checkBackButton(backRect.getRect());
        ide.checkRemoveButton(removeRect.getRect());
        //ide.checkNewSpellButton(newSpellRect.getRect());
        GUI.EndGroup();
    }
    
}
