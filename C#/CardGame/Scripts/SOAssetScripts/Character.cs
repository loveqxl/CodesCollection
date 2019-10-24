using UnityEngine;
using System.Collections;

public enum CharClass{ HeZheng, BarbaBranca}
[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject 
{
	public CharClass Class;
	public string ClassName;
	public int MaxHealth = 30;
	public Sprite AvatarImage;
    public Sprite AvatarBGImage;
    public Sprite PortraitGlowImage;
    public Color32 AvatarBGTint;
    public Color32 ClassCardTint;
    public Color32 ClassRibbonsTint;
}
