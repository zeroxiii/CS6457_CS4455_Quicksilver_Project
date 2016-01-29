/****************************************
	TileTool.js v1.0
	Copyright 2014 Unluck Software	
 	www.chemicalbliss.com																			
*****************************************/
//#pragma strict
import System.Collections.Generic;


class TileTool extends EditorWindow {

    var _go: GameObject;
    var prevPosition: Vector3;
    var doSnap: boolean = false;
    var snapValue: float = 0.5;
    var _warned: boolean = false;
    var _replaceObject: GameObject;
    var _styleGO: GameObject;
    var _selection: Transform[];
    var _autoSnapMax: int = 50;
    var _style: TileStyles;   
    var _tileCycleCounter:int = 0;
    var _index:int = -1;
    var styleNames:String[];
    var styles:TileStyles[];
    var _toggleStyle:boolean = false;
    var _toggleReplace:boolean = false;
    var _toggleRemove: boolean = false;
    var _toggleSnap: boolean = false;
    var _toggleGroup:boolean = false;
    
    @MenuItem("Window/TileTool")

    static
    function ShowWindow() {
        var win = EditorWindow.GetWindow(TileTool);
        win.title = "TileTool";
    }
	
	function ToggleAll () {
		_toggleStyle = _toggleReplace = _toggleRemove = _toggleSnap = _toggleGroup = false;
	}
	
	function OnEnable () {
		RefreshStyles ();
		
	}
	
	function RefreshStyles () {
		Resources.LoadAll("");
		styles = new Resources.FindObjectsOfTypeAll(TileStyles);
		//Debug.Log(styles.length);
      	styleNames = new String[styles.Length];
		for(var i:int;i< styleNames.length;i++){
			styleNames[i] = styles[i].name;
			_index=0;
		}
	}

    function RayCheck(gameObj: GameObject, auto: boolean) {
        if (gameObj.name.Contains("_SQ_") || gameObj.name.Contains("_PL_")) {
            var objectHit: RaycastHit;
            var colGo: Transform;
            colGo = gameObj.transform.FindChild("Collider");
            if (colGo) {
                SetActiveColliders(gameObj, false); //Make sure the ray doesn't hit collider in the raycaster
                //Check if -----FRONT-----  of mesh can be removed
                if (Physics.Raycast(colGo.transform.position, gameObj.transform.forward, objectHit, .65*colGo.transform.lossyScale.x)) {
                    if (!((gameObj.name.Contains("Bend") || gameObj.name.Contains("Arch")) && colGo.localScale.y * 2 != objectHit.collider.transform.localScale.y) &&
                        objectHit.collider.transform.parent && colGo && (objectHit.collider.transform.parent.gameObject.name.Contains("_SQ_") ||
                        objectHit.collider.transform.parent.gameObject.name.Contains("_PL_")) && colGo.localScale.y <= objectHit.collider.transform.localScale.y + .02)                     
                        {
	                        if(!gameObj.name.Contains("Liquid") && !objectHit.collider.transform.parent.gameObject.name.Contains("Liquid")|| 
	                        gameObj.name.Contains("Liquid") && objectHit.collider.transform.parent.gameObject.name.Contains("Liquid"))                       
	                        sidey(gameObj.transform.FindChild("Model").GetComponent(MeshFilter), "front",gameObj);
                        }
                }
                //Check if -----BACK----- of mesh can be removed
                if (Physics.Raycast(colGo.transform.position, gameObj.transform.forward * -1, objectHit, .65*colGo.transform.lossyScale.x) && !gameObj.name.Contains("Arch")) {
                    if (!((gameObj.name.Contains("Bend") || gameObj.name.Contains("Arch")) && colGo.localScale.y * 2 != objectHit.collider.transform.localScale.y) &&
                        objectHit.collider.transform.parent && colGo && (objectHit.collider.transform.parent.gameObject.name.Contains("_SQ_") || 
                        objectHit.collider.transform.parent.gameObject.name.Contains("_PL_")) && colGo.localScale.y <= objectHit.collider.transform.localScale.y + .02)
                        {
	                        if(!gameObj.name.Contains("Liquid") && !objectHit.collider.transform.parent.gameObject.name.Contains("Liquid")|| 
	                        gameObj.name.Contains("Liquid") && objectHit.collider.transform.parent.gameObject.name.Contains("Liquid"))
	                        sidey(gameObj.transform.FindChild("Model").GetComponent(MeshFilter), "back",gameObj);
                        }
                }
                //Check if -----LEFT----- of mesh can be removed				
                if (Physics.Raycast(colGo.transform.position, gameObj.transform.right, objectHit, .65*colGo.transform.lossyScale.x)) {
                    
                    if (objectHit.collider.transform.parent && colGo && (objectHit.collider.transform.parent.gameObject.name.Contains("_SQ_") || 
                    objectHit.collider.transform.parent.gameObject.name.Contains("_PL_")) && colGo.localScale.y <= objectHit.collider.transform.localScale.y + .02)
                        {
	                        if(!gameObj.name.Contains("Liquid") && !objectHit.collider.transform.parent.gameObject.name.Contains("Liquid")|| 
	                        gameObj.name.Contains("Liquid") && objectHit.collider.transform.parent.gameObject.name.Contains("Liquid"))
	                        sidey(gameObj.transform.FindChild("Model").GetComponent(MeshFilter), "left",gameObj); 
                        }
                }
                //Check if -----RIGHT----- of mesh can be removed
                if (Physics.Raycast(colGo.transform.position, gameObj.transform.right * -1, objectHit, .65*colGo.transform.lossyScale.x)) {
                    if (objectHit.collider.transform.parent && colGo && (objectHit.collider.transform.parent.gameObject.name.Contains("_SQ_") || 
                    objectHit.collider.transform.parent.gameObject.name.Contains("_PL_")) && colGo.localScale.y <= objectHit.collider.transform.localScale.y + .02)
                        {
	                        if(!gameObj.name.Contains("Liquid") && !objectHit.collider.transform.parent.gameObject.name.Contains("Liquid")|| 
	                        gameObj.name.Contains("Liquid") && objectHit.collider.transform.parent.gameObject.name.Contains("Liquid"))
	                        sidey(gameObj.transform.FindChild("Model").GetComponent(MeshFilter), "right",gameObj);
                        }
                }
                //Check if -----TOP----- of mesh can be removed
				if (Physics.Raycast(colGo.transform.position, gameObj.transform.up, objectHit, .65*colGo.transform.lossyScale.x)) {
                    if (objectHit.collider.transform.parent && (objectHit.collider.transform.parent.gameObject.name.Contains("_SQ_") || 
                    objectHit.collider.transform.parent.gameObject.name.Contains("_PL_")))
                        {
	                        if(!gameObj.name.Contains("Liquid") && !objectHit.collider.transform.parent.gameObject.name.Contains("Liquid")|| 
	                        gameObj.name.Contains("Liquid") && objectHit.collider.transform.parent.gameObject.name.Contains("Liquid"))
	                        sidey(gameObj.transform.FindChild("Model").GetComponent(MeshFilter), "top",gameObj);
                        }
                }
                //Check if -----BOTTOM----- of mesh can be removed
				if (Physics.Raycast(colGo.transform.position, gameObj.transform.up * -1, objectHit, .65*colGo.transform.lossyScale.x) && !gameObj.name.Contains("Arch")) {
                    if (objectHit.collider.transform.parent && (objectHit.collider.transform.parent.gameObject.name.Contains("_SQ_") || 
                    objectHit.collider.transform.parent.gameObject.name.Contains("_PL_")))
                        {
	                        if(!gameObj.name.Contains("Liquid") && !objectHit.collider.transform.parent.gameObject.name.Contains("Liquid")|| 
	                        gameObj.name.Contains("Liquid") && objectHit.collider.transform.parent.gameObject.name.Contains("Liquid"))
	                        sidey(gameObj.transform.FindChild("Model").GetComponent(MeshFilter), "bottom",gameObj);
                        }
                }
                SetActiveColliders(gameObj, true);
            }
        }
    }

    function SetActiveColliders(go: GameObject, a: boolean) {
        var colliders: Component[];
        colliders = go.GetComponentsInChildren(Collider);
        for (var collider: Collider in colliders) {
            collider.enabled = a;
        }
    }

    function sidey(mf: MeshFilter, side: String, gameObj: GameObject) {
		Undo.RegisterCompleteObjectUndo (gameObj.transform, "TileTool: Destroy Side");
        var mesh: Mesh = mf.sharedMesh;
        if(mesh){
        var meshCopy: Mesh = Mesh.Instantiate(mesh) as Mesh;
        mesh = mf.mesh = meshCopy;
        var vertices: Vector3[] = mesh.vertices;
        var indices: List. < int > = new List. < int > (mesh.triangles);
        var count: int = indices.Count / 3;
        for (var i: int = count - 1; i >= 0; i--) {
            var V1: Vector3 = vertices[indices[i * 3 + 0]];
            var V2: Vector3 = vertices[indices[i * 3 + 1]];
            var V3: Vector3 = vertices[indices[i * 3 + 2]];
            if (side == "top") {
                if (V1.y > 0.49 && V2.y > 0.49 && V3.y > 0.49) {
                    indices.RemoveRange(i * 3, 3);
                }
            }
            if (side == "bottom") {
                if (V1.y < -0.49 && V2.y < -0.49 && V3.y < -0.49||gameObj.name.Contains("_PL_") && V1.y == 0 && V2.y == 0 && V3.y == 0) {
                    indices.RemoveRange(i * 3, 3);
                }
            }
            if (side == "back") {
                if (V1.z < -0.49 && V2.z < -0.49 && V3.z < -0.49) {
                    indices.RemoveRange(i * 3, 3);
                }
            }
            if (side == "front") {
                if (V1.z > 0.49 && V2.z > 0.49 && V3.z > 0.49) {
                    indices.RemoveRange(i * 3, 3);
                }
            }
            if (side == "left") {
                if (V1.x > 0.49 && V2.x > 0.49 && V3.x > 0.49) {
                    indices.RemoveRange(i * 3, 3);
                }
            }
            if (side == "right") {
                //		Debug.Log(V1 + V2 +V3);
                if (V1.x < -0.49 && V2.x < -0.49 && V3.x < -0.49) {
                    indices.RemoveRange(i * 3, 3);
                }
            }
        }
        mesh.triangles = indices.ToArray();
        //mesh.vertices = vertices;
        }
    }
	
	function CombineVector3Arrays (array1 : GameObject[], array2 : GameObject[]) : GameObject[] {
	    var array3 = new GameObject[array1.length + array2.length];
	    System.Array.Copy (array1, array3, array1.length);
	    System.Array.Copy (array2, 0, array3, array1.length, array2.length);
	    return array3;
    }

    function ReplaceStyles(type:String){
        if (Selection.transforms.Length > 0 && _style) { //Check if style is assigned
			var styleObjects:GameObject[]; 
			if(type == "Tile")
			styleObjects = _style._tiles;//CombineVector3Arrays(_style._tiles, _style._objects);
			else if(type == "Object")
			styleObjects = _style._objects;
			else if(type == "All")
			styleObjects = CombineVector3Arrays(_style._tiles, _style._objects);
			        
			if(Selection.activeTransform.GetComponent(TileToolGroup)){
				var glist:Transform[];
	        	glist = new Transform[Selection.activeTransform.childCount];
				var o:Transform = Selection.activeTransform;
				for (i = 0; i < Selection.activeTransform.childCount; i++){
					glist[i] = o.GetChild(i);
				}
        		_selection = glist;
        	}else{ 
        		_selection = Selection.transforms; //Add selection to array
        	}
            
            var olist:GameObject[];
	        olist = new GameObject[_selection.length];
            for (i = 0; i < _selection.Length; i++){
                
                var p: float = i;
                EditorUtility.DisplayProgressBar("Replacing GameObjects", "Any removed sides will be reset", p / _selection.Length);
                olist[i] = _selection[i].gameObject;
                var s: String = _selection[i].gameObject.name;
                var words = s.Split("_" [0]);
                
                if (words.Length > 2) {
                    s = _selection[i].gameObject.name.Replace(words[0], "");
                    for (var j: int = 0; j < styleObjects.Length; j++){
                        var newObjectTile: String = styleObjects[j].name.Replace(_style.gameObject.name.Split("_" [0])[0], "");
                        if (s == newObjectTile) {
                        	var newObject: GameObject;
                            newObject = PrefabUtility.InstantiatePrefab(styleObjects[j]);
                            var newT: Transform = newObject.transform;
                            newT.position = _selection[i].position;
                            newT.rotation = _selection[i].rotation;
                            newT.localScale = _selection[i].localScale;     
                            olist[i] = newObject;
                            newT.parent = _selection[i].transform.parent;                                       
                            Undo.RegisterCreatedObjectUndo(newObject, "ReplaceStyles");
                            Undo.DestroyObjectImmediate(_selection[i].gameObject);
                            
                            break;
                        }
                    }
                }
            }
            //if(!Selection.activeTransform.GetComponent(TileToolGroup)){
            	Selection.objects = olist;
            //}
        }
    }

    function ReplaceGameObjects(){
        _selection = Selection.transforms;
        for (i = 0; i < _selection.Length; i++) {           
			var newObject: GameObject = PrefabUtility.InstantiatePrefab(_replaceObject);
			var newT: Transform = newObject.transform;
			newT.position = _selection[i].position;
			newT.rotation = _selection[i].rotation;
			newT.localScale = _selection[i].localScale;
			newT.parent = _selection[i].transform.parent; 
			Undo.RegisterCreatedObjectUndo(newObject, "ReplaceStyles");
			Undo.DestroyObjectImmediate(_selection[i].gameObject);         
        }
    }
    
	function RotateGameObjects(){
        _selection = Selection.transforms;
		Undo.RecordObjects (_selection, "TileTool: Rotate");
        for (i = 0; i < _selection.Length; i++) {
            _selection[i].Rotate(_selection[i].transform.up * 90);
        }     
    }
    
	function CycleGameObjects(next:boolean){
		if(_style){
	        _selection = Selection.transforms;
	        if(next)
	       	 	this._tileCycleCounter++;
			else
	  			this._tileCycleCounter--;
			if(_tileCycleCounter >= _style._tiles.length)
	       	 	_tileCycleCounter=0;
	        else if(_tileCycleCounter < 0)
	          	_tileCycleCounter=_style._tiles.length-1;
	        var s:GameObject[];
	        s = new GameObject[_selection.length];
	        for (i = 0; i < _selection.Length; i++) {
	            if (_selection[i].gameObject.name.Contains("_SQ_") || _selection[i].gameObject.name.Contains("_PL_")) {
	                var newObject: GameObject = PrefabUtility.InstantiatePrefab(_style._tiles[_tileCycleCounter]);
	                var newT: Transform = newObject.transform;
	                newT.position = _selection[i].position;
	                newT.rotation = _selection[i].rotation;
	                newT.localScale = _selection[i].localScale;
	                newT.parent = _selection[i].transform.parent; 
	                Undo.RegisterCreatedObjectUndo(newObject, "ReplaceStyles");
                	Undo.DestroyObjectImmediate(_selection[i].gameObject);
	               	s[i] = newObject;
	            }
	        }
	        Selection.objects = s;
  		}else{
  			Debug.Log("No Style");
  		}
    }


    function Update() {
        if (Selection.transforms.Length < _autoSnapMax && Selection.transforms.Length > 0 && !EditorApplication.isPlaying && doSnap && Selection.transforms[0].position != prevPosition)
            snap(true);
    }

    function snap(onlyTiles: boolean) {
    	_selection = Selection.transforms;
        try {
            for (i = 0; i < Selection.transforms.Length; i++) {
               
                if (onlyTiles && Selection.transforms[i].gameObject.name.Contains("_SQ_") || Selection.transforms[i].gameObject.name.Contains("_PL_") || !onlyTiles) {
                    if(!onlyTiles){
                   			Undo.RecordObjects (_selection, "TileTool: Snapping");
                    }
                    var t: Vector3 = Selection.transforms[i].transform.position;
                    t.x = round(t.x);
                    t.y = round(t.y);
                    t.z = round(t.z);
                    Selection.transforms[i].transform.position = t;
                }
            }
            prevPosition = Selection.transforms[0].position;
        } catch (e) {
            Debug.LogError("Nothing to move.  " + e);
        }
    }

    function round(input: float) {
        var snappedValue: float;
        snappedValue = snapValue * Mathf.Round((input / snapValue));
        return (snappedValue);
    }

    function Group() {
        _selection = Selection.transforms;     
        var sl:int = _selection.Length;
        if (_selection.Length > 0) {
            var newGo: GameObject = new GameObject();
            newGo.name = "_TileTool Group";
            newGo.AddComponent(TileToolGroup);
            for (i = 0; i < _selection.Length; i++) {
                var p: float = i;
                EditorUtility.DisplayProgressBar("Grouping" + p + "/" + sl, "", p / sl);
                if (!_selection[i].transform.parent || _selection[i].transform.parent.GetComponent(TileToolGroup)){
					_selection[i].gameObject.transform.parent = newGo.transform;
                }
            }        
            if (newGo.transform.childCount == 0) {
                DestroyImmediate(newGo);
                Debug.LogWarning("Group Failed: Objects already grouped");
            }
        } 
    }
    
	function UnGroup() {
        _selection = Selection.transforms;
        var sl:int = _selection.Length;
        if (_selection.Length > 0) {
            for (i = 0; i < _selection.Length; i++) {
                if (_selection[i].transform.parent && _selection[i].transform.parent.GetComponent(TileToolGroup)){
                var p: float = i;
                EditorUtility.DisplayProgressBar("UnGrouping " + p + "/" + sl, "", p / sl);
                    _selection[i].gameObject.transform.parent = null;	
                }
            }         
        }
    }
    
    function ResetToPrefab() {
        _selection = Selection.transforms;
        Undo.RecordObjects (_selection, "TileTool: Reset To Prefab");
        var sl:int = _selection.Length;
        if (_selection.Length > 0) {
            for (i = 0; i < _selection.Length; i++) {
                
                var p: float = i;
                EditorUtility.DisplayProgressBar("Tile Reset","", p / sl);
                if (_selection[i].gameObject.name.Contains("_SQ_") || _selection[i].gameObject.name.Contains("_PL_")) {         
                    Undo.RegisterCompleteObjectUndo(_selection[i], "PrefabReset");
                    var scale:Vector3 = _selection[i].localScale;
                    PrefabUtility.RevertPrefabInstance( _selection[i].gameObject);
                    _selection[i].localScale = scale;
                }
            }         
        }
    }
    
      function removey(side:String){
    	_selection = Selection.transforms; 	
    	for (i = 0; i < _selection.Length; i++) {
	            var p: float = i;
                EditorUtility.DisplayProgressBar("Editing Meshes", "", p / _selection.Length);
	            if (_selection[i].gameObject.name.Contains("_SQ_") || _selection[i].gameObject.name.Contains("_PL_")) {
	            
	            var s = side;
	            if(side=="back"){
	            	if(Mathf.FloorToInt(_selection[i].transform.eulerAngles.y) == 90){
	            		s = "left";	
	            	}else if(Mathf.FloorToInt(_selection[i].transform.eulerAngles.y) == 180){
	            		s = "front";	
	            	}else if(Mathf.FloorToInt(_selection[i].transform.eulerAngles.y) == 270){
	            		s = "right";	
	            	}
	            }else if(side=="left"){
	            	if(Mathf.FloorToInt(_selection[i].transform.eulerAngles.y) == 90){
	            		s = "front";	
	            	}else if(Mathf.FloorToInt(_selection[i].transform.eulerAngles.y) == 180){
	            		s = "right";	
	            	}else if(Mathf.FloorToInt(_selection[i].transform.eulerAngles.y) == 270){
	            		s = "back";	
	            	}
	            }else if(side=="front"){
	            	if(Mathf.FloorToInt(_selection[i].transform.eulerAngles.y) == 90){
	            		s = "right";	
	            	}else if(Mathf.FloorToInt(_selection[i].transform.eulerAngles.y) == 180){
	            		s = "back";	
	            	}else if(Mathf.FloorToInt(_selection[i].transform.eulerAngles.y) == 270){
	            		s = "left";	
	            	}
	            }else if(side=="right"){
	            	if(Mathf.FloorToInt(_selection[i].transform.eulerAngles.y) == 90){
	            		s = "back";	
	            	}else if(Mathf.FloorToInt(_selection[i].transform.eulerAngles.y) == 180){
	            		s = "left";	
	            	}else if(Mathf.FloorToInt(_selection[i].transform.eulerAngles.y) == 270){
	            		s = "front";	
	            	}
	            }            	
                	sidey(_selection[i].transform.FindChild("Model").GetComponent(MeshFilter), s ,_selection[i].gameObject);
			}
		}
    }
    
    function OnGUI() {
    	EditorGUILayout.Space();
		
		if (GUILayout.Button("Tile Styles", EditorStyles.toolbarButton)) {
			
            _toggleStyle = !_toggleStyle;
        }
		if(_toggleStyle){
		EditorGUILayout.Space();
		if(_index >= 0){
        _index = EditorGUILayout.Popup(_index, styleNames);       
        _style = styles[_index];
        }

       

        if (GUILayout.Button("Replace Tile Style")) {      
                ReplaceStyles("Tile");
        }
        
        if (GUILayout.Button("Replace Object Style")) {      
                ReplaceStyles("Object");
        }
        
        if (GUILayout.Button("Replace All")) {      
                ReplaceStyles("All");
        }
        EditorGUILayout.BeginHorizontal();
         if (GUILayout.Button("Prev Tile", EditorStyles.miniButtonLeft)) {
				CycleGameObjects(false);
         }
        if (GUILayout.Button("Rotate", EditorStyles.miniButtonMid)) {
        	RotateGameObjects();
        }
         if (GUILayout.Button("Next Tile", EditorStyles.miniButtonRight)) {
				CycleGameObjects(true);
         }
       
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        }
        EditorGUILayout.Space();
        if (GUILayout.Button("Remove Hidden Sides", EditorStyles.toolbarButton)) {
            _toggleRemove = !_toggleRemove;
        }
        if (_toggleRemove) {
            EditorGUILayout.Space();
            _go = Selection.activeGameObject;
            
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Top", EditorStyles.miniButtonLeft)) {
				removey("top");
               
            }
            if (GUILayout.Button("Bottom", EditorStyles.miniButtonRight)) {
				
				removey("bottom");
				
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Front", EditorStyles.miniButtonLeft)) {
				removey("front");
                
            }
            if (GUILayout.Button("Back", EditorStyles.miniButtonRight)) {
				removey("back");

            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Left", EditorStyles.miniButtonLeft)) {
				removey("left");
                
            }
            if (GUILayout.Button("Right", EditorStyles.miniButtonRight)) {
				removey("right");
               
            }
            EditorGUILayout.EndHorizontal();
        
        
        
        
            
       
        
        if (GUILayout.Button("Auto Destroy Sides")) {

            _selection = Selection.transforms;
            Undo.RecordObjects (_selection, "TileTool: Auto Destroy Sides");
            for (i = 0; i < _selection.Length; i++) {

                var p: float = i;
                EditorUtility.DisplayProgressBar("Editing Meshes", "Auto Destroy is not 100% proof; check for missing triangles", p / _selection.Length);
                RayCheck(_selection[i].gameObject, true);
            }
        }
        
        if (GUILayout.Button("Reset Sides")) {        	
        	ResetToPrefab();	
        }
        
        EditorGUILayout.Space();
        }
		EditorGUILayout.Space();
		
		
		if (GUILayout.Button("Grouping", EditorStyles.toolbarButton)) {
            _toggleGroup = !_toggleGroup;
        }
		if(_toggleGroup){
		
		 if (GUILayout.Button("Group" )) {
            Group();
        }
		if (GUILayout.Button("UnGroup" )) {
            UnGroup();
        }

		EditorGUILayout.LabelField("Undo not supported for grouping", EditorStyles.miniLabel);
		}
		
		EditorGUILayout.Space();
		
		if (GUILayout.Button("Replace GameObjects", EditorStyles.toolbarButton)) {
            _toggleReplace = !_toggleReplace;
        }
		if(_toggleReplace){
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Please Select Prefab", EditorStyles.miniLabel);
        _replaceObject = EditorGUILayout.ObjectField(_replaceObject, GameObject, false);
        
        
        if (GUILayout.Button("Replace")) {
            if (_replaceObject) {
                ReplaceGameObjects();
            }else{
            	Debug.LogError("No Prefab assigned");
            }
        }
        EditorGUILayout.Space();
        }
        
		EditorGUILayout.Space();
		if (GUILayout.Button("Snapping", EditorStyles.toolbarButton)) {
            _toggleSnap = !_toggleSnap;
        }
		if(_toggleSnap){
		EditorGUILayout.Space();
        
        if (Selection.transforms.Length < _autoSnapMax)
            doSnap = EditorGUILayout.Toggle("Autosnap", doSnap);
        else
            doSnap = EditorGUILayout.Toggle("Autosnap (disabled)", doSnap);
        snapValue = EditorGUILayout.FloatField("Value", snapValue);
        _autoSnapMax = EditorGUILayout.IntField("Max Autosnap", _autoSnapMax);
        EditorGUILayout.Space();
        if (GUILayout.Button("Snap")) {
            
            snap(false);
        }
       
		}
       
        EditorGUILayout.LabelField("Do NOT rename style objects", EditorStyles.miniLabel);
        EditorGUILayout.LabelField("Selected Objects: " + Selection.transforms.Length, EditorStyles.miniLabel);
    }

    function OnInspectorUpdate() {
        Repaint();
        EditorUtility.ClearProgressBar();
        
        if(_index < 0){
        	
        	this.RefreshStyles();
        	
        }
        
    }
}