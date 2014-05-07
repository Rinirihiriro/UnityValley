using UnityEngine;
using System.Collections;

public class MoveCharacter : MonoBehaviour {

	enum SlopeType {
		slope,
		slope_up,
		slope_down,
		floor,
		floor_down,	// 아랫부분이 내려가는 경사
		floor_up,	// 아랫부분이 올라가는 경사
		half,
	};

	struct MoveData {
		public Vector3 dir;
		public GameObject destFloor;
		public MoveData(Vector3 dir, GameObject obj) {
			this.dir = dir;
			this.destFloor = obj;
		}
	};

	float moving = 0;
	float updown = 0;
	bool half_check = false;
	Vector3 moveDest = Vector3.zero;
	GameObject standingFloor = null;
	GameObject destFloor = null;
	MoveData[] moveList = null;
	int moveListIndex = 0;


	public GameObject StandingFloor {
		get {
			return standingFloor;
		}
	}

	Vector3 ArrangePos(Vector3 vec) {
		float x = vec.x;
		float y = vec.y;
		float z = vec.z;

		if (Mathf.Abs (transform.up.x) > 0.125) {
			vec.x = z;
			vec.y = -x;
			vec.z = -y;
		}
		else if (Mathf.Abs (transform.up.z) > 0.125) {
			vec.x = -y;
			vec.y = -z;
			vec.z = x;
		}

		return vec;
	}

	Vector3 ArrangeDir(Vector3 vec) {
		float x = vec.x;
		float y = vec.y;
		float z = vec.z;
		
		if (Mathf.Abs (transform.up.x) > 0.125) {
			vec.x = -y;
			vec.y = -z;
			vec.z = x;
		}
		else if (Mathf.Abs (transform.up.z) > 0.125) {
			vec.x = z;
			vec.y = -x;
			vec.z = -y;
		}
		
		return vec;
	}

	Vector3 FloorFunc(Vector3 pos) {
		Vector3 v = ArrangePos (pos);
		return new Vector3(v.x + v.y, v.z + v.y, v.y);
	}

	Vector3 FloorFuncWithoutArrange(Vector3 v) {
		return new Vector3(v.x + v.y, v.z + v.y, v.y);
	}
	
	Vector3 PlayerFloorValue() {
		Vector3 pos = transform.position;
		pos -= transform.up;
		if (updown != 0)
			pos += transform.up * 0.5f;

		return FloorFunc(pos);
	}

	Vector3 SlopeNormal(GameObject slope) {
		Mesh mesh = slope.GetComponent<MeshFilter>().mesh; 
		return slope.transform.rotation * mesh.normals[10];
	}
	/*
	Vector3 PlayerNormal() {
		return transform.up;
	}
	*/
	SlopeType GetSlopeType(GameObject slope) {
		Vector3 slope_normal = SlopeNormal (slope);
		Vector3 player_normal = transform.up;
		float dot = Vector3.Dot (slope_normal, player_normal);
		if (dot > 0.125f) {
			return SlopeType.slope;
		} else if (dot < -0.125f) {
			return SlopeType.floor;
		} else {
			return SlopeType.half;
		}
	}

	SlopeType GetSlopeType(GameObject slope, Vector3 move_dir) {
		Vector3 slope_normal = SlopeNormal (slope);
		Vector3 player_normal = transform.up;
		float dot1 = Vector3.Dot (slope_normal, player_normal);
		float dot2 = Vector3.Dot (slope_normal, move_dir);
		if (dot1 > 0.125f) {
			if (dot2 > 0.125f)
				return SlopeType.slope_down;
			else if (dot2 < -0.125f)
				return SlopeType.slope_up;
			else return SlopeType.half;
		} else if (dot1 < -0.125f) {
			if (dot2 > 0.125f)
				return SlopeType.floor_up;
			else if (dot2 < -0.125f)
				return SlopeType.floor_down;
			else return SlopeType.half;
		} else {
			return SlopeType.half;
		}
	}

	GameObject FindFloorWithFloorValue(Vector3 floor_pos, bool find_next = false) {
		GameObject[] floors = GameObject.FindGameObjectsWithTag("Floor");

		GameObject target = null;	// 찾는 위치
		Vector3 target_pos = Vector3.zero;

		GameObject lid = null;		// 찾는 위치 바로 위에 있는 바닥(뚜껑)
		Vector3 lid_pos = new Vector3(floor_pos.x + 1, floor_pos.y + 1, floor_pos.z + 1);

		foreach (GameObject obj in floors) {
			Vector3 pos = FloorFunc (obj.transform.position);
			if (floor_pos.x == pos.x && floor_pos.y == pos.y
			    && (!find_next || pos.z < floor_pos.z)
			    && (target == null || pos.z > target_pos.z)) {
				target = obj;
				target_pos = pos;
			}
			else if (lid_pos.x == pos.x && lid_pos.y == pos.y
			         && (lid == null || pos.z > lid_pos.z)) {
				lid = obj;
				lid_pos = pos;
			}
		}

		if (target == null || (lid && lid_pos.z == target_pos.z + 1)) {
			return null;
		}

		return target;
	}

	void MoveTo(GameObject obj, Vector3 dir) {
		updown = 0;
		if (standingFloor.name[0] == 'S') {
			SlopeType type = GetSlopeType(standingFloor, dir);
			if (type == SlopeType.slope_up) {
				updown = 1;
			}
			else if (type == SlopeType.slope_down) {
				updown = -1;
			}
			else if (type == SlopeType.half) {
				return;
			}
		}

		transform.rotation = Quaternion.LookRotation(dir, transform.up);

		moveDest = obj.transform.position;
		moveDest += transform.up;
		destFloor = obj;
		moving = 1;
		float maxZ = CalcMaxZ (FloorFunc (destFloor.transform.position));
		if (maxZ > PlayerFloorValue().z) ChangeZ (maxZ);
	}

	void Move(Vector3 dir) {
		Vector3 object_pos = standingFloor.transform.position;
		Vector3 object_floor = FloorFunc (object_pos);
		
		Vector3 arranged_dir = ArrangeDir(dir);
		Vector3 floor_value_offset = FloorFuncWithoutArrange(dir);
		Vector3 next_floor = object_floor + floor_value_offset;
		next_floor.z = 1024;
		
		if (standingFloor.name[0] == 'S') {
			SlopeType type = GetSlopeType(standingFloor, arranged_dir);
			if (type == SlopeType.half) return;
			else if (type == SlopeType.slope_down) {
				next_floor.x -= 1;
				next_floor.y -= 1;
				object_floor.z -= 1;
			}
		}
		
		GameObject other;
		Vector3 other_floor = Vector3.zero;
		while (other = FindFloorWithFloorValue(next_floor, true)) {
			other_floor = FloorFunc(other.transform.position);
			
			if (other_floor.z > object_floor.z &&
			    next_floor.x + next_floor.y > object_floor.x + object_floor.y &&
			    !(other.name[0] == 'S' &&
			  GetSlopeType(other, arranged_dir) == SlopeType.floor_down)) {
				next_floor.z = other_floor.z;
				continue;
			}
			else if (other_floor.z < object_floor.z &&
			         next_floor.x + next_floor.y < object_floor.x + object_floor.y &&
			         !(standingFloor.name[0] == 'S' &&
			  GetSlopeType(standingFloor, arranged_dir) == SlopeType.floor_up)) {
				next_floor.z = other_floor.z;
				continue;
			}
			MoveTo(other, arranged_dir);
			return;
		}
		
		next_floor.x += 1;
		next_floor.y += 1;
		next_floor.z = 1024;
		while (other = FindFloorWithFloorValue(next_floor, true)) {
			other_floor = FloorFunc(other.transform.position);
			if (other.name[0] == 'S'
			    && GetSlopeType(other, arranged_dir) == SlopeType.slope_up) {
				MoveTo(other, arranged_dir);
				return;
			}
			next_floor.z = other_floor.z;
		}
			
	}

	public void StopMoving () {
		moving = 0;
		destFloor = null;
		moveList = null;
		moveListIndex = 0;
//		RecalcPlayerPos ();
	}

	void ChangeZ(float z) {
		float deltaZ = z - PlayerFloorValue ().z;
		if (deltaZ == 0) return;
		Vector3 pos = transform.position;
		transform.position = new Vector3(pos.x - deltaZ, pos.y + deltaZ, pos.z - deltaZ);
	}

	float CalcMaxZ(Vector3 floor_value) {
		GameObject left_top_obj;
		GameObject right_top_obj;

		Vector3 left_top_pos = new Vector3 (floor_value.x, floor_value.y + 1, 1024);
		Vector3 right_top_pos = new Vector3 (floor_value.x + 1, floor_value.y, 1024);

		float maxZ = floor_value.z;
		float tmpZ;

		while (left_top_obj = FindFloorWithFloorValue (left_top_pos, true)) {
			tmpZ = FloorFunc(left_top_obj.transform.position).z;
			if (tmpZ > maxZ
			    && left_top_obj.name[0] == 'S'
			    && GetSlopeType(left_top_obj, ArrangeDir (Vector3.forward)) == SlopeType.floor_down) {
				maxZ = tmpZ;
				break;
			}
			left_top_pos.z = tmpZ;
		}

		while (right_top_obj = FindFloorWithFloorValue (right_top_pos, true)) {
			tmpZ = FloorFunc(right_top_obj.transform.position).z;
			if (tmpZ > maxZ
			    && right_top_obj.name[0] == 'S'
			    && GetSlopeType(right_top_obj, ArrangeDir (Vector3.right)) == SlopeType.floor_down) {
				maxZ = tmpZ;
				break;
			}
			right_top_pos.z = tmpZ;
		}

		return maxZ;
	}

	void RecalcPlayerZ() {
		Vector3 player_pos = PlayerFloorValue ();
		ChangeZ (CalcMaxZ (player_pos));
	}

	public void RecalcPlayerPos() {
		Vector3 player_pos = standingFloor.transform.position;
		Vector3 angle = transform.rotation.eulerAngles;
		transform.rotation = Quaternion.Euler (Mathf.Round(angle.x), Mathf.Round(angle.y), Mathf.Round(angle.z));

		updown = 0;
		if (standingFloor.name[0] == 'S') {
			SlopeType type = GetSlopeType(standingFloor, transform.forward);
			if (type == SlopeType.slope_up) {
				updown = 1;
			}
			else if (type == SlopeType.slope_down) {
				updown = -1;
			}
		}

		player_pos += transform.up;
		if (updown != 0)
			player_pos -= transform.up * 0.5f;
		
		transform.position = player_pos;
		RecalcPlayerZ();
		// player_pos = transform.position;
		// transform.position = new Vector3 (Mathf.Round (player_pos.x), Mathf.Round (player_pos.y), Mathf.Round (player_pos.z));
	}

	// Use this for initialization
	void Start () {
	}

	public void Init() {
		moving = 0;
		updown = 0;
		half_check = false;
		moveDest = Vector3.zero;
		standingFloor = null;
		destFloor = null;
		moveList = null;
		moveListIndex = 0;

		transform.rotation = Quaternion.identity;
		standingFloor = FindFloorWithFloorValue (PlayerFloorValue());
		RecalcPlayerZ ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				GameObject obj = hit.transform.gameObject;
				FindPathTo(obj);
			}
		}

		if (moving > 0) {
			moving -= Time.deltaTime * 4;
			transform.Translate ((Vector3.forward + Vector3.up * updown) * Time.deltaTime * 4, Space.Self);
			if (moving <= 0) {
				transform.position = moveDest;
				standingFloor = destFloor;
				destFloor = null;
				half_check = false;
				RecalcPlayerPos();
				if (moveList != null) {
					moveListIndex += 1;
					if (moveListIndex < moveList.Length) {
						MoveTo(moveList[moveListIndex].destFloor, moveList[moveListIndex].dir);
					} else {
						moveList = null;
						moveListIndex = 0;
					}
				}
			}
			else if (!half_check && moving < 0.5) {
				half_check = true;
				updown = 0;
				if (destFloor.name[0] == 'S') {
					SlopeType type = GetSlopeType(destFloor, transform.forward);
					if (type == SlopeType.slope_up) {
						updown = 1;
					}
					else if (type == SlopeType.slope_down) {
						updown = -1;
					}
				}
			}
			return;
		}

		if (moveList != null) return;
		// ---------

		// --------------

		if (Input.GetKey(KeyCode.UpArrow)) {
			Move(Vector3.forward);
		}
		else if (Input.GetKey(KeyCode.DownArrow)) {
			Move(Vector3.back);
		}
		else if (Input.GetKey(KeyCode.LeftArrow)) {
			Move(Vector3.left);
		}
		else if (Input.GetKey(KeyCode.RightArrow)) {
			Move(Vector3.right);
		}
	}


	class AStarItem{
		GameObject obj;
		Vector3 floor_value;
		int weight;
		int GWeight, HWeight;
		public Vector3 dir;	// 이 타일로 오는 방향
		public AStarItem parent;

		public AStarItem(GameObject obj, Vector3 floor_value, int gweight, int hweight, Vector3 dir) {
			this.obj = obj;
			this.floor_value = floor_value;
			this.GWeight = gweight;
			this.HWeight = hweight;
			weight = gweight + hweight;
			this.dir = dir;
		}

		public GameObject Obj {
			get { return obj; }
		}

		public Vector3 FloorValue {
			get { return floor_value; }
		}

		public int Weight {
			get { return weight; }
		}

		public int G {
			get { return GWeight; }
			set { GWeight = value; weight = GWeight + HWeight; }
		}

		public int H {
			get { return HWeight; }
			set { HWeight = value; weight = GWeight + HWeight; }
		}
	};

	class ItemComparer : IComparer {
		public int Compare (object x, object y){
			return CompareItem ((AStarItem) x, (AStarItem) y);
		}

		public int CompareItem(AStarItem a, AStarItem b) {
			return a.Weight.CompareTo (b.Weight);
		}	
	}

	void FindPathTo(GameObject dest) {
		ArrayList open_list = new ArrayList ();
		ArrayList closed_list = new ArrayList ();
		IComparer compare = new ItemComparer ();
		open_list.Add (new AStarItem (standingFloor, FloorFunc (standingFloor.transform.position), 0, 0, Vector3.zero));

		Vector3 dest_floor = FloorFunc (dest.transform.position);
		AStarItem dest_item = null;

		while (open_list.Count > 0) {
			open_list.Sort(compare);
			AStarItem item = (AStarItem) open_list[0];
			open_list.RemoveAt(0);
			closed_list.Add (item);

			if (item.Obj == dest) {
				dest_item = item;
				break;
			}

			AStarItem[] near_items = FindNearFloors(item.Obj);
			for (int i = 0; i < near_items.Length; i++) {
				if (near_items[i] == null) continue;
				AStarItem near_item = near_items[i];
				bool ok = true;
				foreach (AStarItem it in closed_list) {
					if (it.Obj == near_item.Obj) {
						ok = false;
						break;
					}
				}
				if (!ok) continue;

				near_item.G = item.G + 1;
				near_item.H = (int) (Mathf.Abs(dest_floor.x - near_item.FloorValue.x) + Mathf.Abs(dest_floor.y - near_item.FloorValue.y));
				near_item.parent = item;

				for (int j = 0; j < open_list.Count; j++) {
					AStarItem it = (AStarItem) open_list[j];
					if (it.Obj == near_item.Obj) {
						ok = false;
						it.G = Mathf.Max (it.G, near_item.G);
						break;
					}
				}
				if (!ok) continue;

				open_list.Add(near_item);
			}
		}

		if (dest_item != null) {
			ArrayList move_list = new ArrayList();
			AStarItem item = dest_item;
			while (item.Weight > 0) {
				move_list.Add (new MoveData(item.dir, item.Obj));
				item = item.parent;
			}
			move_list.Reverse();

			if (moving > 0){
				StopMoving ();
				RecalcPlayerPos ();
			}

			moveList = new MoveData[move_list.Count];
			for (int i = 0; i < move_list.Count; i++) {
				moveList[i] = (MoveData)move_list[i];
			}

			MoveTo(moveList[0].destFloor, moveList[0].dir);
		}
	}

	AStarItem[] FindNearFloors(GameObject obj) {
		Vector3[] dirs = new Vector3[]{
			Vector3.forward,
			Vector3.right,
			Vector3.back,
			Vector3.left,
		};

		AStarItem[] items = new AStarItem[dirs.Length];

		Vector3 object_pos = obj.transform.position;
		Vector3 object_floor = FloorFunc (object_pos);

		for (int i = 0; i < dirs.Length; i++) {
			Vector3 dir = dirs[i];
			Vector3 arranged_dir = ArrangeDir(dir);
			Vector3 floor_value_offset = FloorFuncWithoutArrange(dir);
			Vector3 next_floor = object_floor + floor_value_offset;
			next_floor.z = 1024;

			if (obj.name[0] == 'S') {
				SlopeType type = GetSlopeType(obj, arranged_dir);
				if (type == SlopeType.half) continue;
				else if (type == SlopeType.slope_down) {
					next_floor.x -= 1;
					next_floor.y -= 1;
					object_floor.z -= 1;
				}
			}

			GameObject other;
			Vector3 other_floor = Vector3.zero;
			while (other = FindFloorWithFloorValue(next_floor, true)) {
				other_floor = FloorFunc(other.transform.position);

				if (other_floor.z > object_floor.z &&
				    next_floor.x + next_floor.y > object_floor.x + object_floor.y &&
				    !(other.name[0] == 'S' &&
				  	GetSlopeType(other, arranged_dir) == SlopeType.floor_down)) {
					next_floor.z = other_floor.z;
					continue;
				}
				else if (other_floor.z < object_floor.z &&
				         next_floor.x + next_floor.y < object_floor.x + object_floor.y &&
				         !(obj.name[0] == 'S' &&
			  			GetSlopeType(obj, arranged_dir) == SlopeType.floor_up)) {
					next_floor.z = other_floor.z;
					continue;
				}
				break;
			}

			if (other) {
				items[i] = new AStarItem(other, other_floor, 0, 0, arranged_dir);
				continue;
			}

			next_floor.x += 1;
			next_floor.y += 1;
			next_floor.z = 1024;
			while (other = FindFloorWithFloorValue(next_floor, true)) {
				other_floor = FloorFunc(other.transform.position);
				if (other.name[0] == 'S'
				    && GetSlopeType(other, arranged_dir) == SlopeType.slope_up) {
					break;
				}
				next_floor.z = other_floor.z;
			}

			if (other) {
				items[i] = new AStarItem(other, other_floor, 0, 0, arranged_dir);
				continue;
			}

		}

		return items;
	}

}
