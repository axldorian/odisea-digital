using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	public Image portrait;
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;

	private Animator animator;
	private Queue<Dialogue> leftDialogues;
	private Queue<string> sentences;
	private bool isDialogueActive;

	static List<Sprite> portraits;
	static Sprite genericPortrait;

	private PlayableDirector _activeDirector;

	public bool isCutscenePlaying;

	// In the Start method, we initialize the sentences queue.
	void Start()
	{
		// Initialization
		sentences = new();
		isDialogueActive = false;
		animator = GetComponent<Animator>();

		// Load all the portraits if they are not already loaded
		if (portraits == null)
		{
			// Load the portraits
			portraits = new(Resources.LoadAll<Sprite>("Portraits"));

			// Remove the "_0" from the name of the portraits (if any)
			portraits.ForEach(sprite => sprite.name = sprite.name.Replace("_0", ""));

			// Load the generic portrait (used when the portrait of the speaker is not found)
			genericPortrait = portraits.Find(sprite => sprite.name == "generic");
		}
	}

	// if the dialogue is active and enter is pressed, we display the next sentence.
	void Update()
	{
		if (isDialogueActive && Input.GetKeyDown(KeyCode.Return))
		{
			DisplayNextSentence();
		}
	}

	/// <summary>
	/// Initialize a dialogue sequence.
	/// </summary>
	/// <param name="dialogues">
	/// The dialogues to display.
	/// (The dialogues are displayed in the order they are in the list.)
	/// </param>
	/// <param name="director">
	/// The director to pause when the dialogue is active.
	/// </param>
	public void InitializeDialogue(List<Dialogue> dialogues, PlayableDirector director)
	{
		// Set the active director
		_activeDirector = director;

		// Pause the director
		_activeDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);

		// Populate the left dialogues queue
		leftDialogues = new(dialogues);

		// Start the first dialogue block
		var dialogue = leftDialogues.Dequeue();
		StartDialogue(dialogue);
	}

	/// <summary>
	/// Start a dialogue in the sequence.
	/// </summary>
	/// <param name="dialogue">
	/// The dialogue to start playing.
	/// </param>
	private void StartDialogue(Dialogue dialogue)
	{
		// Start the dialogue animation
		animator.SetBool("IsOpen", true);

		// Clear the sentences queue
		sentences.Clear();

		// Set the name of the speaker
		nameText.text = dialogue.name;

		// Set the portrait of the speaker
		var portrait = portraits.Find(sprite => sprite.name == dialogue.sprite);

		if (portrait != null)
		{
			// if the portrait is found, set it
			this.portrait.sprite = portrait;
		}
		else
		{
			// if the portrait is not found, set the default portrait
			this.portrait.sprite = genericPortrait;
		}

		// Add each sentence from the dialogue to the sentences queue
		dialogue.sentences.ForEach(sentence => sentences.Enqueue(sentence));

		// Display the first sentence
		DisplayNextSentence();

		// Set the dialogue as active
		isDialogueActive = true;
	}

	/// <summary>
	/// Display the next sentence in the dialogue queue.
	/// </summary>
	private void DisplayNextSentence()
	{
		// If there are no more sentences, end the dialogue
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		// Display the next sentence
		var sentence = sentences.Dequeue();

		// Stop the previous coroutine if it's still running
		StopAllCoroutines();

		// Start the coroutine to type the sentence
		StartCoroutine(TypeSentence(sentence));
	}

	/// <summary>
	/// Type the sentence letter by letter after a short delay.
	/// </summary>
	/// <param name="sentence">
	/// The sentence to type.
	/// </param>
	/// <returns>
	/// A coroutine to wait for a short time before typing the next letter.
	/// </returns>
	private IEnumerator TypeSentence(string sentence)
	{
		// Clear the dialogue text
		dialogueText.text = "";

		// Type each letter of the sentence
		foreach (var letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.03f);
		}
	}

	/// <summary>
	/// End the dialogue sequence.
	/// </summary>
	private void EndDialogue()
	{
		// Set the dialogue as inactive
		isDialogueActive = false;

		// End the dialogue animation
		animator.SetBool("IsOpen", false);

		// If there are more dialogues, start the next one
		if (leftDialogues.Count > 0)
		{
			StartCoroutine(StartNextDialogue());
		}
		else
		{
			/* If there are no more dialogues, resume the director
			   Verifing if the PlayableGraph is not null */
			if (_activeDirector.playableGraph.IsValid())
				_activeDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
		}
	}

	/// <summary>
	/// Start the next dialogue in the sequence.
	/// </summary>
	/// <returns>
	/// A coroutine to wait for a short time before starting the next dialogue.
	/// </returns>
	private IEnumerator StartNextDialogue()
	{
		// Wait for a short time
		yield return new WaitForSeconds(0.5f);

		// Start the next dialogue
		var dialogue = leftDialogues.Dequeue();
		StartDialogue(dialogue);
	}
}
