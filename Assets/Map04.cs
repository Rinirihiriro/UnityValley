using UnityEngine;
using System.Collections;

public class Map04 : IMapData {
	Vector3[] cubePos = new Vector3[]{
		new Vector3(3,0,10),
		new Vector3(3,0,9),
		new Vector3(3,0,8),
		new Vector3(3,0,7),
		new Vector3(3,0,6),
		new Vector3(3,0,5),
		new Vector3(3,0,4),
		new Vector3(3,0,3),

		new Vector3(7,3,3),

		new Vector3(7,6,7),
		new Vector3(6,6,7),
		new Vector3(5,6,7),
		new Vector3(4,6,7),
		new Vector3(3,6,7),
		new Vector3(2,6,7),
		new Vector3(1,6,7),
		new Vector3(0,6,7),
		new Vector3(-1,6,7),
		new Vector3(-2,6,7),
		new Vector3(-3,6,7),
		new Vector3(-3,6,6),
		new Vector3(-3,6,5),
	};
	
	Vector3[] slopePos = new Vector3[]{
		new Vector3(4,1,3),
		new Vector3(5,2,3),
		new Vector3(6,3,3),

		new Vector3(4,0,3),
		new Vector3(5,1,3),
		new Vector3(6,2,3),

		new Vector3(7,4,4),
		new Vector3(7,5,5),
		new Vector3(7,6,6),

		new Vector3(7,3,4),
		new Vector3(7,4,5),
		new Vector3(7,5,6),

		new Vector3(-3,6,4),
	};
	
	Quaternion[] slopeRot = new Quaternion[]{
		Quaternion.Euler(0,0,0),
		Quaternion.Euler(0,0,0),
		Quaternion.Euler(0,0,0),

		Quaternion.Euler(0,0,180),
		Quaternion.Euler(0,0,180),
		Quaternion.Euler(0,0,180),

		Quaternion.Euler(0,270,0),
		Quaternion.Euler(0,270,0),
		Quaternion.Euler(0,270,0),

		Quaternion.Euler(0,270,180),
		Quaternion.Euler(0,270,180),
		Quaternion.Euler(0,270,180),

		Quaternion.Euler(0,270,90),
	};
	
	RotatingObject[] rotObj = new RotatingObject[]{
	};
	
	Vector3 playerStart = new Vector3(3, 0, 3);
	
	public Vector3[] CubePos{ get{ return cubePos; } }
	public Vector3[] SlopePos{ get{ return slopePos; } }
	public Quaternion[] SlopeRot{ get{ return slopeRot; } }
	public RotatingObject[] RotObj{ get{ return rotObj; } }
	public Vector3 PlayerStart{ get{ return playerStart; } }
	
}
