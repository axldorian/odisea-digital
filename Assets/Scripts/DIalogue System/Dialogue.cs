using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
	public string name;
	public string sprite;

	[TextArea(3, 10)]
	public List<string> sentences;
}
