using UnityEngine;
using System.Collections;

public class TestAutoCompletionManager {

	public Suggestions getSuggestions()
	{
		Suggestions s = new Suggestions();
		
		s.put("Suggestion B");
		s.put("Suggestion A");
		s.put("Suggestion C");

		return s;
	}
}
