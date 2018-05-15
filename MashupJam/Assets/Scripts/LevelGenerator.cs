using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour {


	public Tilemap background, overBackground, platforms, overPlatforms;

	// Use this for initialization
	void Start () {
		//Clean ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Clean(){
		overPlatforms.ClearAllTiles ();
		platforms.ClearAllTiles ();
		overBackground.ClearAllTiles ();
	}

	public void GeneratePreset(){
		
	}

	public void GenerateBackground(){
		//overBackground.SetTile();

	}

	public void GenerateArena(){
		
	}

	public void GenerateMobs (){
		
	}

	public void GenerateLevel(){
		GenerateBackground ();
		GenerateArena ();
		GenerateMobs ();
	}
}
