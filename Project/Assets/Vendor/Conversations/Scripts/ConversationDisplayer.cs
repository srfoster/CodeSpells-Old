using UnityEngine;
using System.Collections;

public class ConversationDisplayer : MonoBehaviour {
	
	public int height = 300;
	public int width = 300;
	
	public Font font;
	
	private Conversation conversation;
	
	void OnGUI(){
		if(conversation == null)
			return;
		
		GUIStyle guiStyle = GUI.skin.box;
		guiStyle.wordWrap = true;
		guiStyle.font = font;
		guiStyle.normal.background = Resources.Load("Textures/SyntaxHighlightBlue") as Texture2D;
		guiStyle.alignment = TextAnchor.MiddleCenter;
		guiStyle.fontSize = 17;
		
		GUI.Box(new Rect(Screen.width/2-width/2,Screen.height/2-height/2,width,height), conversation.GetText(), guiStyle);
		
		int response_height = 0;
		
		if(conversation.GetResponses() != null)
		{	
			foreach(object responseObject in conversation.GetResponses())
			{
				Response response = responseObject as Response;
				
				string response_text = response.getResponseText();
				
				GUIStyle buttonStyle = GUI.skin.box;
				buttonStyle.wordWrap = true;
				buttonStyle.font = font;
				buttonStyle.normal.background = Resources.Load("Textures/SyntaxHighlightGreen") as Texture2D;
				buttonStyle.alignment = TextAnchor.MiddleCenter;
		  		buttonStyle.fontSize = 17;
				
				if(GUI.Button(new Rect(Screen.width/2+width/2 + 10,Screen.height/2-height/2 + response_height,200,50), response_text, buttonStyle))
				{
					conversation.Respond(response);
					
					if(response.isExit())
					{
						conversation = null;
					}
				}
				response_height += 60;
			}
		}
	}
	
	public void Converse(Conversation conversation)
	{
		this.conversation = conversation;
	}
}
