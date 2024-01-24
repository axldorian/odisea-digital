using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Dialogue : MonoBehaviour
{
	public TextMeshProUGUI dialogueText;
	public String[] sentences;
	private int index = 0;
	public float typingSpeed = 0.02f;
	private GameObject dialogBox;
	private bool isTyping = false;

	// Start is called before the first frame update
	void Start()
	{
		// obtener el objeto del dialogo y ocultarlo
		dialogBox = GameObject.Find("DialogFirst");
		dialogBox.SetActive(false);

		// esperar 3.82 segundos para empezar a escribir
		Invoke(nameof(StartDialogue), 4.5f);
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

	void StartDialogue()
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
		yield return new WaitForSeconds(0.8f);

		index++;
		isTyping = false;
	}
}
