using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NEw Hint", menuName = "Hint")]
public class Hint : ScriptableObject
{
    public List<string> text;
    public List<Image> images;
    public bool textFirst;
}
