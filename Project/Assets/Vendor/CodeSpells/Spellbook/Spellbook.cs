using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class Spellbook : MonoBehaviour {
        
                        
        public delegate void EventHandler(SpellbookPage page);
        public static event EventHandler PageTurnedForward;
        public static event EventHandler PageTurnedBackward;
        public static event EventHandler SpellCopied;

        
        Texture2D background_texture;
        
        GameObject previous_state;
        
        bool enabled = false;
        bool noCopyDisplay = false;
        bool canCopyAll = true;
        
        public GUIStyle button_style = new GUIStyle();
        public GUIStyle code_style = new GUIStyle();
        public GUIStyle half_code_style = new GUIStyle();
        private GUIStyle empty_style = new GUIStyle();
        
        public Texture2D button_up_texture;
        public Texture2D button_down_texture;
        
        public Texture2D prev_button_texture;
        public Texture2D next_button_texture;
        public Texture2D copy_button_texture;
        
        private Vector2 scroll_position = Vector2.zero;
        
        public Font code_font;
        
        IDE ide;
        
        int current_page = 0;
        
        public List<SpellbookPage> pages = new List<SpellbookPage>();
        
        Dictionary<string, int> copied_spells = new Dictionary<string,int>();

        
        IEnumerator Start()
        {
                background_texture = Resources.Load("SpellbookMock") as Texture2D;
                
                button_style.normal.background = button_up_texture;
                button_style.active.background = button_down_texture;
                button_style.normal.textColor = Color.white;
                button_style.active.textColor = new Color(0.75f,0.75f,0.75f);
                button_style.alignment = TextAnchor.MiddleCenter;
                button_style.fontSize = 30;
                
                //set code_style
                code_style.fontSize = 20;
                code_style.normal.textColor = Color.black;
                code_style.font = code_font;
                code_style.wordWrap = false;
                
                //set code_style for display of small spellbook
                half_code_style.fontSize = 20; //will be reset based on scaling
                half_code_style.normal.textColor = Color.black;
                half_code_style.font = code_font;
                half_code_style.wordWrap = false;
                
                ide = GameObject.Find("IDE").GetComponent<IDE>();
                
                return null;

        }
        
        public void Add(SpellbookPage page)
        {
                pages.Add(page);        
        }
        
        void OnGUI(){
                
                if(enabled){
                    
                        displayCurrentPage();

            if (GUI.Button (new Rect (Screen.width - 200,30,130,65), "Back", button_style))
            {        
                enabled = false;
                previous_state.active = true;
                setNoCopyDisplay(false);
            }
        
            if (!noCopyDisplay && (canCopyAll || currentPage().getName() == "MySpell" || currentPage().getName() == "Flame")) {
                GUIStyle copy_button_style = new GUIStyle();
                copy_button_style.normal.background = copy_button_texture;
                copy_button_style.normal.textColor = Color.black;
                copy_button_style.active.textColor = new Color(0.75f,0.75f,0.75f);
                copy_button_style.alignment = TextAnchor.MiddleCenter;
                copy_button_style.fontSize = 30;
        
                if (GUI.Button (new Rect (Screen.width * 0.17f, Screen.height * 0.86f, 210, 50), "Copy", copy_button_style)){
                    enabled = false;
                    previous_state.active = true;
            
                    givePlayerAScroll();
            
                    SpellCopied(currentPage());
                }
            }
                        //code to use Left and Right keys to move through spellbook
                        
                        if(current_page != 0 && Input.GetKey(KeyCode.LeftArrow))
                        {
                                current_page--;
                                PageTurnedBackward(currentPage());
                                logCurrentPage();
                        
                        }
                        if(current_page != pages.Count - 1 && Input.GetKey(KeyCode.RightArrow))
                        {
                                current_page++;
                                PageTurnedForward(currentPage());
                                logCurrentPage();
                        }
                                
                                
                        GUIStyle prev_button_style = new GUIStyle();
                        prev_button_style.normal.background = prev_button_texture;
                        if(current_page != 0 && GUI.Button (new Rect (Screen.width * 0.025f, Screen.height * 0.5f, 35, 35), "", prev_button_style))
                        {
                                current_page--;
                                PageTurnedBackward(currentPage());
                                logCurrentPage();
                        }
                        
                        
                        GUIStyle next_button_style = new GUIStyle();
                        next_button_style.normal.background = next_button_texture;

                        if(current_page != pages.Count - 1 && GUI.Button (new Rect (Screen.width * 0.95f, Screen.height * 0.5f, 35, 35), "", next_button_style))
                        {
                                current_page++;
                                PageTurnedForward(currentPage());
                                logCurrentPage();
                        }
                        
                        // make it so that we can't click through to the game
                        // NOTE: This must appear LAST in the OnGUI. Otherwise, other buttons won't work!
                    GUI.Button(new Rect(0,0,Screen.width,Screen.height), "", empty_style);
                }
        }
                
        public string copyBlankSpell() {
            int temp_curr = current_page;
            current_page = pages.FindIndex(
                delegate(SpellbookPage p)
            {
                return p.getName() == "MySpell";
            }
        );
            string fname = givePlayerAScroll();
            SpellCopied(currentPage());
            current_page = temp_curr;
            return fname;
        }
        
        public string getIncName(string name) {
            string root = name;
            Regex r = new Regex(@"[0-9]+");
        Match m = r.Match(name);
        if (m.Success) {
            Capture c = m.Groups[0].Captures[0];
            int thisnum = int.Parse(c.ToString());
            root = name.Replace(c.ToString(), "");
        }
            if (!copied_spells.ContainsKey(root))
                copied_spells.Add(root, 0);
            copied_spells[root]++;
            return root + copied_spells[root];
        }
        
        public void setNameCounter(string name) {
            string root = name;
        int thisnum = 0;
        Regex r = new Regex(@"[0-9]+");
        Match m = r.Match(name);
        if (m.Success) {
            Capture c = m.Groups[0].Captures[0];
            thisnum = int.Parse(c.ToString());
            root = name.Replace(c.ToString(), "");
        }
        if (!copied_spells.ContainsKey(root))
            copied_spells.Add(root, 0);
        int number_so_far = copied_spells[root];

        copied_spells[root] = Mathf.Max(number_so_far, thisnum);
        }
        
        public void addExistingSpell(string name, string code) {       
        CodeScrollItem item = GameObject.Find("Inventory").GetComponent<Inventory>().getCodeScrollItem(name);
        
        if (item == null) {

            GameObject initial_scroll = new GameObject();
            initial_scroll.name = "InitialScroll";
            initial_scroll.AddComponent<CodeScrollItem>();
            item = initial_scroll.GetComponent<CodeScrollItem>();
            item.item_name = "Blank";
            item.inventoryTexture = Resources.Load( "Textures/Scroll") as Texture2D;

            setNameCounter(name);
        
            CodeScrollItem code_scroll_item_component = initial_scroll.GetComponent<CodeScrollItem>();
            code_scroll_item_component.setCurrentFile(name + ".java");
            //code_scroll_item_component.getIDEInput().SetCode(currentPage().code.Replace(currentPage().getName(), currentPage().getName() + number));
            if (code != "")
                code_scroll_item_component.getIDEInput().SetCode(code);
        
            ProgramLogger.LogCode(name, code_scroll_item_component.getIDEInput().GetCode());
            GameObject.Find("Inventory").GetComponent<Inventory>().addItem(initial_scroll);
        
            //string fname = givePlayerAScroll();
            //SpellCopied(currentPage());
            
            } else {
                if (code != "")
                item.getIDEInput().SetCode(code);
        
            ProgramLogger.LogCode(name, item.getIDEInput().GetCode());
            }
        }
        
        string givePlayerAScroll()
        {
                CodeScrollItem item;

                GameObject initial_scroll = new GameObject();
                initial_scroll.name = "InitialScroll";
                initial_scroll.AddComponent<CodeScrollItem>();
                item = initial_scroll.GetComponent<CodeScrollItem>();
                item.item_name = "Blank";
                item.inventoryTexture = Resources.Load( "Textures/Scroll") as Texture2D;
                                
                if(!copied_spells.ContainsKey(currentPage().getName()))
                        copied_spells.Add(currentPage().name,0);
                
                
                int number_so_far = copied_spells[currentPage().getName()];
                int number = number_so_far + 1;
                
                copied_spells[currentPage().name]++;

                CodeScrollItem code_scroll_item_component = initial_scroll.GetComponent<CodeScrollItem>();
                code_scroll_item_component.setCurrentFile(currentPage().getName() + number + ".java");
                
                code_scroll_item_component.getIDEInput().SetCode(currentPage().code.Replace(currentPage().getName(), currentPage().getName() + number));
                
                ProgramLogger.LogCode(currentPage().getName() + number, code_scroll_item_component.getIDEInput().GetCode());
                SpellLogger.LogCode(currentPage().getName() + number, code_scroll_item_component.getIDEInput().GetCode());

                GameObject.Find("Inventory").GetComponent<Inventory>().addItem(initial_scroll);
                return currentPage().getName() + number + ".java";
        }
        
        void displayCurrentPage()
        {
                if(currentPage() == null || currentPage().texture == null || currentPage().code == null)
                {
                        Debug.Log("Current " + currentPage().texture + " " + currentPage().code);
                        
                        enabled = false;
                        previous_state.active = true;

                        return;        
                }
                
                GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),currentPage().texture);
                
                if(!currentPage().code.Equals(""))
                        showCode();
        }
        
        void showCode()
        {        
                GUILayout.BeginArea(new Rect(Screen.width * 0.07f, Screen.height * 0.14f, Screen.width * 0.383f, Screen.height * 0.725f));
                scroll_position = GUILayout.BeginScrollView (scroll_position, GUILayout.Width(Screen.width * 0.383f), GUILayout.Height(Screen.height * 0.725f)); // Should vary the size of the last rect by how much text we have??

                foreach (Tuple<Rect, Texture2D> tup in (new Highlight()).highlightPage(currentPage().code)) {
                        GUI.DrawTexture(tup.Item1, tup.Item2);
                }
                GUI.SetNextControlName("ReferenceCode");
                GUILayout.TextArea(currentPage().code, code_style);
                //GUILayout.Label(currentPage().code, code_style);
                //GUILayout.Box(currentPage().code, code_style);
                
        GUILayout.EndScrollView();
                GUILayout.EndArea();
        }
        
        public void displayScaledPage(HalfBook halfbook) {
            Rect textRect = halfbook.getTextRect();
            Rect bookRect = halfbook.getBookRect();
            
            half_code_style.fontSize = (int) (code_style.fontSize * bookRect.width / Screen.width);
            float fontScale = half_code_style.fontSize / ((float) code_style.fontSize);
            //float fontScale = bookRect.width / Screen.width;
            
            //GUI.SetNextControlName("SpellbookPage");
            GUI.DrawTexture(bookRect,currentPage().texture);
            //if (GUI.Button(bookRect,currentPage().texture))
            //    halfbook.setFocus();
            
            //GUI.SetNextControlName("ReferenceCode");
            GUILayout.BeginArea(textRect);
                scroll_position = GUILayout.BeginScrollView (scroll_position, GUILayout.Width(textRect.width), GUILayout.Height(textRect.height)); // Should vary the size of the last rect by how much text we have??

                foreach (Tuple<Rect, Texture2D> tup in (new Highlight()).highlightPage(currentPage().code)) {
                    //since we changed the font size, need to rescale the highlighting
                    Rect h = tup.Item1;
                    h.x = (h.x) * fontScale;
                    h.y = (h.y+2) /23 * half_code_style.lineHeight - 2*fontScale;
                    h.width *= fontScale;
                    h.height *= fontScale;
                        GUI.DrawTexture(h, tup.Item2);
                }
                
                GUILayout.TextArea(currentPage().code, half_code_style);
                //GUILayout.Label(currentPage().code, code_style);
                //GUILayout.Box(currentPage().code, code_style);
                
        GUILayout.EndScrollView();
                GUILayout.EndArea();
        }
        
        
        public void show(GameObject previous_state)
        {
                this.previous_state = previous_state;
                previous_state.active = false;
                enabled = true;
        }
        
        SpellbookPage currentPage(){
            Debug.Log(""+current_page);
                return pages[current_page];        
        }
        
        public void logCurrentPage() {
            ProgramLogger.LogKVtime("page", currentPage().getName());
        }
        
        public void pageChangeButtons(Rect prev, Rect next) {
            GUIStyle prev_button_style = new GUIStyle();
        prev_button_style.normal.background = prev_button_texture;
        if(current_page != 0 && GUI.Button (prev, "", prev_button_style))
        {
            current_page--;
            PageTurnedBackward(currentPage());
            logCurrentPage();
        }
        
        
        GUIStyle next_button_style = new GUIStyle();
        next_button_style.normal.background = next_button_texture;
        if(current_page != pages.Count - 1 && GUI.Button (next, "", next_button_style))
        {
            current_page++;
            PageTurnedForward(currentPage());
            logCurrentPage();
        }
        }
        
        public void setNoCopyDisplay(bool b) {
            noCopyDisplay = b;
        }
}
