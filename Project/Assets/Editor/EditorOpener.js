

class EditorOpener extends EditorWindow {
    var myString = "Hello World";
    var groupEnabled = false;
    var myBool = true;
    var myFloat = 1.23;
    
    var last_script : MonoScript = null;
    
    // Add menu named "My Window" to the Window menu
    @MenuItem ("Window/EditorOpener")
    static function ShowWindow () {
      EditorWindow.GetWindow (EditorOpener);
    }
    
    function Update()
    {   
    
    	if(Fancy.current_behaviour == null)
    		return;
    		
    	var script : MonoScript = MonoScript.FromMonoBehaviour(Fancy.current_behaviour);
    	
    	if(script.Equals(last_script))
    	{
    		return;
    	}
    	
    	last_script = script;
    	
    	AssetDatabase.OpenAsset(script,Fancy.current_line_number);
    }
    
    function OnGUI()
    {
    		GUI.Label(Rect(50,Screen.height/2,Screen.width - 50, 50), ""); 
    }

}