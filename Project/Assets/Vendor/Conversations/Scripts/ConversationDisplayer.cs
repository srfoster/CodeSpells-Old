using UnityEngine;
using System.Collections;

public class ConversationDisplayer : MonoBehaviour {
	
	public int height = 300;
	public int width = 300;
	
	bool mouse_over_button = false;
	
	public Font font;
	
	private Conversation conversation;
	
	void Start(){
		font = Resources.Load("Erika Ormig") as Font;	
	}
	
	void OnGUI(){
		if(conversation == null)
			return;
		

		
		GUIStyle guiStyle = new GUIStyle();
		guiStyle.wordWrap = true;
		guiStyle.font = font;
		guiStyle.normal.background = Resources.Load("Textures/SyntaxHighlightBlue") as Texture2D;
		guiStyle.alignment = TextAnchor.MiddleCenter;
		guiStyle.fontSize = 20;
		guiStyle.normal.textColor = Color.black;
		
		GUI.Box(new Rect(Screen.width/2-width/2,Screen.height/2-height/2,width,height), conversation.GetText(), guiStyle);
		
		int response_height = 0;
		
		mouse_over_button = false;
		
		if(conversation.GetResponses() != null)
		{	
			foreach(object responseObject in conversation.GetResponses())
			{
				Response response = responseObject as Response;
				
				string response_text = response.getResponseText();
				
				GUIStyle buttonStyle = new GUIStyle();
				buttonStyle.wordWrap = true;
				buttonStyle.font = font;
				buttonStyle.normal.background = Resources.Load("Textures/SyntaxHighlightGreen") as Texture2D;
				buttonStyle.alignment = TextAnchor.MiddleCenter;
				buttonStyle.fontSize = 20;
				buttonStyle.normal.textColor = Color.black;
				
				
				//Check if the mouse is over a button
				Rect button_rect = new Rect(Screen.width/2+width/2 + 10,Screen.height/2-height/2 + response_height,200,50);
				
				if(button_rect.Contains(Input.mousePosition)){
					mouse_over_button = true;	
				}
				
				if(GUI.Button(button_rect, response_text, buttonStyle))
				{
					conversation.Respond(response);
					
					if(response.isExit())
					{
						exit();
					}
				}
				response_height += 60;
			}
		}
		
		
				
		if(Event.current.type == EventType.MouseDown && !mouse_over_button){
			Event.current.Use();
			exit();
			return;
		}
		
		
	}
	
	void exit()
	{
		conversation = null;
		Time.timeScale = 1;	
	}
	
	public void Converse(Conversation conversation)
	{
		this.conversation = conversation;
	}
}
