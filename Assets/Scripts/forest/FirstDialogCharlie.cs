using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using System;

public class FirstDialogCharlie : MonoBehaviour
{
	public TextMeshProUGUI dialogueText;
	public PlayableDirector charlieOut;
	public String[] sentences;
	private int index = 0;
	public float typingSpeed = 0.02f;
	private GameObject dialogBox;
	private bool isTyping = false;

	void Start()
	{
		// obtener el objeto del dialogo y ocultarlo
		dialogBox = GameObject.Find("DialogCharlie");
		dialogBox.SetActive(false);
		// try
		// {
		// }
		// catch (Exception e)
		// {
		// 	Debug.Log("No se encontro el dialogo de Charlie");
		// }
	}

	// Update is called once per frame
	void Update()
	{
		// Si el dialogo esta activo y no se esta escribiendo, pasar al siguiente dialogo
		if (dialogBox.activeSelf && !isTyping)
		{
			NextSentence();
		}
	}

	public void StartDialogue()
	{
		// Activar el dialogo despues de 2 segundos
		Invoke(nameof(StartDialogueInvoke), 2f);
	}

	public void StartDialogueInvoke()
	{
		// Activar el dialogo
		dialogBox.SetActive(true);
		NextSentence();
	}

	void NextSentence()
	{
		if (index < sentences.Length)
		{
			isTyping = true;
			dialogueText.text = "";
			StartCoroutine(WriteSentence());
		}
		else
		{
			dialogBox.SetActive(false);

			// Reproducir la cinematica de salida de Charlie
			charlieOut.Play();
		}
	}

	IEnumerator WriteSentence()
	{
		foreach (char letter in sentences[index].ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(typingSpeed);
		}

		// esperar 1 segundo para pasar al siguiente dialogo
		yield return new WaitForSeconds(1f);

		index++;
		isTyping = false;
	}
}
