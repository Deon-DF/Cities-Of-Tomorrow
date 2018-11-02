using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour {

	public static GUI id;
	public Text text_next_phase;

	public Canvas canvas_Event;
	public Canvas canvas_Opportunity;
	public Canvas canvas_Feedback;
	public Canvas canvas_Deployment;

	public Menus menu;

	public EventView event_view;


	#region phase_management

	public void button_next_turn() {
		switch (Game.id.current_phase) {
		case Enum_Phase.phase_Event:
			Game.id.current_phase = Enum_Phase.phase_Opportunity;
			text_next_phase.text = "Next: Feedback";
			refresh_gui_overlays ();
			break;
		case Enum_Phase.phase_Opportunity:
			Game.id.current_phase = Enum_Phase.phase_Feedback;
			text_next_phase.text = "Next: Deployment";
			refresh_gui_overlays ();
			break;
		case Enum_Phase.phase_Feedback:
			Game.id.current_phase = Enum_Phase.phase_Deployment;
			text_next_phase.text = "Next: Event";
			refresh_gui_overlays ();
			break;
		case Enum_Phase.phase_Deployment:
			Game.id.current_phase = Enum_Phase.phase_Event;
			text_next_phase.text = "Next: Opportunity";
			refresh_gui_overlays ();
			break;
		}
	}

	public void refresh_gui_overlays() {
		if (Game.id.current_menu == Enum_Menu.main) {
			GUI.id.menu.main_menu.gameObject.SetActive(true);
			canvas_Event.gameObject.SetActive (false);
			canvas_Opportunity.gameObject.SetActive (false);
			canvas_Feedback.gameObject.SetActive (false);
			canvas_Deployment.gameObject.SetActive (false);
		} else 	if (Game.id.current_phase == Enum_Phase.phase_Event) {
			GUI.id.menu.main_menu.gameObject.SetActive(false);
			canvas_Event.gameObject.SetActive (true);
			canvas_Opportunity.gameObject.SetActive (false);
			canvas_Feedback.gameObject.SetActive (false);
			canvas_Deployment.gameObject.SetActive (false);
		} else if (Game.id.current_phase == Enum_Phase.phase_Opportunity) {
			GUI.id.menu.main_menu.gameObject.SetActive(false);
			canvas_Event.gameObject.SetActive (false);
			canvas_Opportunity.gameObject.SetActive (true);
			canvas_Feedback.gameObject.SetActive (false);
			canvas_Deployment.gameObject.SetActive (false);
		} else if (Game.id.current_phase == Enum_Phase.phase_Feedback) {
			GUI.id.menu.main_menu.gameObject.SetActive(false);
			canvas_Event.gameObject.SetActive (false);
			canvas_Opportunity.gameObject.SetActive (false);
			canvas_Feedback.gameObject.SetActive (true);
			canvas_Deployment.gameObject.SetActive (false);
		} else if (Game.id.current_phase == Enum_Phase.phase_Deployment) {
			GUI.id.menu.main_menu.gameObject.SetActive(false);
			canvas_Event.gameObject.SetActive (false);
			canvas_Opportunity.gameObject.SetActive (false);
			canvas_Feedback.gameObject.SetActive (false);
			canvas_Deployment.gameObject.SetActive (true);
		}
	}

	#endregion

	#region main / escape_menu

	public void button_main_menu_play() {
		menu.main_menu.gameObject.SetActive (false);
		Game.id.current_menu = Enum_Menu.none;
		Game.id.start_game();

	}

	public void button_main_menu_settings() {
		menu.main_menu.gameObject.SetActive (false);
		menu.settings_menu.gameObject.SetActive (true);
		Game.id.current_menu = Enum_Menu.settings;
	}

	public void button_escape_menu_resume() {
		menu.escape_menu.gameObject.SetActive (false);
		Game.id.current_menu = Enum_Menu.none;
	}

	public void button_escape_menu_settings() {
		menu.escape_menu.gameObject.SetActive (false);
		menu.settings_menu.gameObject.SetActive (true);
		Game.id.current_menu = Enum_Menu.settings;
	}

	public void button_escape_menu_saveandexit(){
		FileManager.deleteFile (Game.id.save_path + "/savegame.sav");
		FileManager.createSaveFile (Game.id.save_path);
	}

	#endregion

	#region settings_menu

	public void update_settings_buttons_on_start() {
		
		Text button_text = menu.settings_menu.transform.Find ("Button_sfx").transform.Find ("Text").GetComponent<Text>();
		if (PlayerPrefs.id.sfx == 1) {
			button_text.text = "SFX: ON";
		} else {
			button_text.text = "SFX: OFF";
		}	

		Text button_text2 = menu.settings_menu.transform.Find ("Button_music").transform.Find ("Text").GetComponent<Text>();
		if (PlayerPrefs.id.music == 1) {
			button_text2.text = "Music: ON";
		} else {
			button_text2.text = "Music: OFF";
		}	
	}

	public void button_settings_menu_toggle_sfx() {
		Text button_text = menu.settings_menu.transform.Find ("Button_sfx").transform.Find ("Text").GetComponent<Text>();
		if (PlayerPrefs.id.sfx == 1) {
			PlayerPrefs.id.sfx = 0;
			button_text.text = "SFX: OFF";
		} else {
			PlayerPrefs.id.sfx = 1;
			button_text.text = "SFX: ON";
		}
	}

	public void button_settings_menu_toggle_music() {
		Text button_text = menu.settings_menu.transform.Find ("Button_music").transform.Find ("Text").GetComponent<Text>();
		if (PlayerPrefs.id.music == 1) {
			PlayerPrefs.id.music = 0;
			button_text.text = "Music: OFF";
		} else {
			PlayerPrefs.id.music = 1;
			button_text.text = "Music: ON";
		}
	}

	public void button_settings_menu_close ()
	{
		menu.settings_menu.gameObject.SetActive (false);
		PlayerPrefs.id.save_player_settings ();
		if (Game.id.game_started) {
			menu.escape_menu.gameObject.SetActive (true);
			Game.id.current_menu = Enum_Menu.esc;
		} else {
			menu.main_menu.gameObject.SetActive (true);
			Game.id.current_menu = Enum_Menu.main;
		}
	}

	#endregion

	#region event view

	public void button_event_generate(string location) {
		Events.generate_location_event (location);
		event_view.canvas.gameObject.SetActive (true);
	}

	public void button_event_close() {
		event_view.canvas.gameObject.SetActive (false);
	}

	#endregion


	// Use this for initialization
	void Awake() {
		if (id == null) {
			id = this;
			text_next_phase.text = "Next: Opportunity";
		} else {
			Debug.LogError ("Another instance of GUI is already initialized!");
		}
	}
}
