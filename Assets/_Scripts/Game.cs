using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
	
	public string save_path = "Saves";

	public bool game_started = false;
	public static Game id;
	public Enum_Phase current_phase = Enum_Phase.phase_Event;
	public Enum_Menu current_menu = Enum_Menu.none;
	public Enum_Level current_level = Enum_Level.level3;

	public Base player_base;
	public Controls controls;
	public City city;
	
	public List<Character> party;
	public List<Character> safehouse;

	void initialize() {
		current_menu = Enum_Menu.main;

		#region Load user preferences
		PlayerPrefs.id.load_player_settings ();
		GUI.id.update_settings_buttons_on_start ();
		#endregion

		GUI.id.refresh_gui_overlays();

		controls = new Controls ();
			
	}
	
	public void start_game () {
		player_base = new Base ();
		city = new City ();
		
		party = new List<Character>();
		safehouse = new List<Character>();

		current_phase = Enum_Phase.phase_Event;

		#region Create player character
		Character protagonist = new Character();
		protagonist.protagonist = true;
		protagonist.first_name = "Deon";
		protagonist.second_name = "Denton";
		protagonist.age = 32;
		
		protagonist.might = 1;
		protagonist.dexterity = 1;
		protagonist.charisma = 1;
		protagonist.intelligence = 1;
		
		party.Add(protagonist);
		game_started = true;
		#endregion
	}

	void Awake () {
		if (id == null) {
			id = this;
		} else {
			Debug.LogError ("Another instance of Game is already initialized!");
		}
	}

	void Start () {
		initialize ();
	}

	void Update () {
		controls.hotkeys ();
	}
}
