using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Highlight {
	
	private Texture2D control_statement_syntax_highlight;
	private Texture2D expression_syntax_highlight;
	private Texture2D declaration_syntax_highlight;
	private Texture2D error_syntax_highlight;
	
	public Highlight() {
		control_statement_syntax_highlight = Resources.Load ("Textures/SyntaxHighlightYellow") as Texture2D;
		expression_syntax_highlight = Resources.Load ("Textures/SyntaxHighlightBlue") as Texture2D;
		declaration_syntax_highlight = Resources.Load ("Textures/SyntaxHighlightPurple") as Texture2D;
		error_syntax_highlight = Resources.Load ("Textures/SyntaxHighlightRed") as Texture2D;
	}
	
	public List<Tuple<Rect, Texture2D>> highlightPage(string code) {
		List<Tuple<Rect, Texture2D>> allHighlights = new List<Tuple<Rect, Texture2D>>();
		string[] lines = code.Split("\n"[0]);
		string line;
		for (int i=0; i < lines.Length; i++) {
			line = lines[i];
			allHighlights = getHighlights("if\\w*(.*)",control_statement_syntax_highlight, line, i, allHighlights);
			allHighlights = getHighlights("while\\w*(.*)",control_statement_syntax_highlight, line, i, allHighlights);
			allHighlights = getHighlights("for \\w*(.*)",control_statement_syntax_highlight, line, i,  allHighlights);
						
			allHighlights = getHighlights("Enchanted", declaration_syntax_highlight, line, i, allHighlights);
			allHighlights = getHighlights("EnchantedList", declaration_syntax_highlight, line, i, allHighlights);
			allHighlights = getHighlights("String", declaration_syntax_highlight, line, i, allHighlights);
			allHighlights = getHighlights("Location", declaration_syntax_highlight, line, i, allHighlights);
			allHighlights = getHighlights("getTarget", declaration_syntax_highlight, line, i, allHighlights);
			allHighlights = getHighlights("getByName", declaration_syntax_highlight, line, i, allHighlights);
			allHighlights = getHighlights("getLocation", declaration_syntax_highlight, line, i, allHighlights);
			
			allHighlights = getHighlights("true", expression_syntax_highlight, line, i, allHighlights);
			allHighlights = getHighlights("false", expression_syntax_highlight, line, i, allHighlights);			
		}
		return allHighlights;
	}
	
	public List<Tuple<Rect, Texture2D>> highlightErrors(string code, List<int> errorLines) {
		//Debug.Log ("In highlightErrors, #errorLines: "+errorLines.Count);
		List<Tuple<Rect, Texture2D>> allHighlights = new List<Tuple<Rect, Texture2D>>();
		string[] lines = code.Split("\n"[0]);
		foreach(int errLn in errorLines) {
			if(errLn < lines.Length)
				allHighlights = getHighlights(".*", error_syntax_highlight, lines[errLn], errLn, allHighlights);
		}
		return allHighlights;
	}
	
	private List<Tuple<Rect, Texture2D>> getHighlights(string token, Texture2D texture, string line, int count, List<Tuple<Rect, Texture2D>> currHighlights) {
		MatchCollection matches = Regex.Matches(line,token);
		for(int i = 0; i < matches.Count; i++) {
			Match match = matches[i];
			int offset = match.Groups[0].Index;
			int length = match.Groups[0].Length;
			currHighlights.Add(new Tuple<Rect, Texture2D>(new Rect(-2 + offset*12,-2 + count * 23, 5 + length * 12, 30), texture));
		}
		return currHighlights;
	}

	
}
