using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

public class FileManager {

	static StreamReader file;

	public static List<string> readFromTextFile(string file_path) {
		List<string> file_contents = new List<string> ();

		if (System.IO.File.Exists (file_path)) {
			StreamReader file = new StreamReader (file_path);

			while (!file.EndOfStream) {
				string input_line = file.ReadLine ();
				file_contents.Add (input_line);
			}

			file.Close ();
		}
		return file_contents;
	}

	public static void deleteFile (string file_path) {
		if (System.IO.File.Exists (file_path)) {
			File.Delete (file_path);
		} else {
			Debug.LogError ("File " + file_path + " not found!");
		}
	}

	public static void createPrefsFile(string file_path) {
		if (!System.IO.File.Exists (file_path)) {
			using (StreamWriter sw = File.CreateText(file_path))
			{
				sw.WriteLine ("[ General ]");
				sw.WriteLine ("");
				sw.WriteLine ("[ Sound ]");
				sw.WriteLine ("sfx " + PlayerPrefs.id.sfx);
				sw.WriteLine ("music " + PlayerPrefs.id.music);
				sw.WriteLine (" ");
				sw.WriteLine ("[ Gameplay ]");
				sw.WriteLine ("");
				sw.Close();
			}

		} else {
			Debug.LogError ("The file " + file_path + " already exists!");
		}
	}

	public static void createSaveFile(string file_path) {
		/* do this if you want to create a new save based on time
		System.DateTime current_time = System.DateTime.Now;
		string time_format = current_time.ToString ();
		time_format = time_format.Replace ("/", "_");
		time_format = time_format.Replace (" ", "_");
		time_format = time_format.Replace (":", "_");
		string savefile = file_path + "/savegame_" + time_format + ".sav";*/
		string savefile = file_path + "/savegame" + ".sav";
		if (!System.IO.File.Exists (savefile)) {
			using (StreamWriter sw = File.CreateText(savefile))
			{
				sw.WriteLine ("sfx " + PlayerPrefs.id.sfx);
			}

		} else {
			Debug.LogError ("The file " + savefile + " already exists!");
		}
	}
}
