using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


/// <summary>
/// Class that triggers the dialogues and cutscenes.
/// This class is the only one that should be called to start a dialogue or a cutscene.
/// </summary>
public class DialogueTrigger : MonoBehaviour
{
	public DialogueManager dialogueManager;

	// The container of the cutscenes (the timelines)
	public GameObject cutscenesContainer;
	public string dialoguesFile;
	private Queue<List<Dialogue>> dialogues;
	private Queue<PlayableDirector> directors;
	private PlayableDirector currentDirector;


	// Start is called before the first frame update
	void Start()
	{

		// Load files with the dialogues from the JSON file
		dialogues = new();
		var filesDialogues = Resources.LoadAll<TextAsset>($"Dialogues/{dialoguesFile}");

		// For each dialogues set in files
		foreach (var file in filesDialogues)
		{
			// Deserialize the JSON
			var dialoguesFromFile = JsonUtility.FromJson<DialogueStructure>(file.text);

			// Enqueue the dialogues
			dialogues.Enqueue(dialoguesFromFile.dialogues);
		}

		// Get directors from the cutscenes container
		directors = new();
		foreach (Transform timeline in cutscenesContainer.transform)
		{
			var director = timeline.GetComponent<PlayableDirector>();

			// Enqueue the directors
			directors.Enqueue(director);
		}
	}

	/// <summary>
	/// Set the active director to the next one in the queue.
	/// </summary>
	public void NextCutscene()
	{
		// Get the next director
		currentDirector = directors.Dequeue();
	}

	/// <summary>
	/// Start the dialogue segment.
	/// </summary>
	public void StartDialogueSegment()
	{
		// Get the next dialogues set
		var dialogue = dialogues.Dequeue();

		// Start the dialogue
		dialogueManager.InitializeDialogue(dialogue, currentDirector);
	}

	/// <summary>
	/// Set the cutscene state.
	/// </summary>
	/// <param name="value">
	/// The value to set the cutscene state to.
	/// </param>
	public void SetCutsceneState(bool value)
	{
		dialogueManager.isCutscenePlaying = value;
	}
}
