using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class TileBrush3D : EditorWindow
{
    public GameObject TilePrefab; //Tile we are going to be "painting" goes here.
    public float CellSize = 1f; //Size of the cells on the grid (best to Leave it at 1, but you can go bigger if you want.)
    private GameObject Preview;
    private Quaternion PreviewRotation = Quaternion.identity; //Rotation that will be applied to the preview and placed tiles.
    public float YOffset = 0f;
    private GameObject LastPrefab;

    [MenuItem("Tools/3D Tile Brush")] //Adds this to the "Tool" tab at the top of the unity editor,
    public static void ShowWindow()
    {
        GetWindow<TileBrush3D>("3D TileBrush");
    }

    //When the window is created, hooks onto the scene view editor, allowing us to... well edit the scene, only triggers when the Tool is enabled.
    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI; //"Yes, I am infact open."
    }
    //Does the opposite of OnEnable, removes the hooks, turns off the previews, etc.
    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI; //"Yes, I am infact closed."

        if (Preview != null)
            DestroyImmediate(Preview); //Destroying the preview upon window being closed.
    }
    private void OnGUI()
    {
        EditorGUI.BeginChangeCheck(); // We are checking if the inserted prefab has been updated.
        TilePrefab = (GameObject)EditorGUILayout.ObjectField("Tile Prefab", TilePrefab, typeof(GameObject), false); //Creating a field in the new window, drag prefabs into this window.
        CellSize = EditorGUILayout.FloatField("Cell Size", CellSize);
        YOffset = EditorGUILayout.FloatField("Y Offset", YOffset);
        if (EditorGUI.EndChangeCheck())
        {
            if (Preview != null)
                DestroyImmediate(Preview);
            if (TilePrefab != null)
            {
                Preview = (GameObject)PrefabUtility.InstantiatePrefab(TilePrefab);
                Preview.hideFlags = HideFlags.HideAndDontSave;
                LastPrefab = TilePrefab;
            }
        }

        //Creating a bit of space, just for asthetics.
        GUILayout.Space(10);
        //Listing the control scheme.
        GUILayout.Label("Controls:", EditorStyles.boldLabel);
        GUILayout.Label("Left Click  = Place tile");
        GUILayout.Label("Right Click = Delete Tile");
        GUILayout.Label("R = Rotate 90° clockwise");
        GUILayout.Label("T = Rotate 90° counter-clockwise");
    }
    private void OnSceneGUI(SceneView sceneView)
    {
        if (TilePrefab == null)
            return;

        Event e = Event.current; //Used for our rayccasting, gets the current input, position of mouse, etc.
        //Creating the preview
        if (Preview == null)
        {
            Preview = (GameObject)PrefabUtility.InstantiatePrefab(TilePrefab);
            Preview.hideFlags = HideFlags.HideAndDontSave; //Ensures the preview doesnt save to the scene.
        }

        //Using Raycast, KEEP IN MIND, THIS WILL ENSURE WE CAN ONLY PLACE ON A "BASEPLATE" OR A "PLANE" WE CANNOT PLACE ON NOTHING
        Ray Raycast = HandleUtility.GUIPointToWorldRay(e.mousePosition);

        // if no prefab is selected, nothing happens.



        if (Physics.Raycast(Raycast, out RaycastHit hit))
        {
            Vector3 SnappedPos = SnapToGrid(hit.point); //Calculating the grid snap position based on the raycast.

            Preview.transform.position = SnappedPos; //Move title to the snapped position
            Preview.transform.rotation = PreviewRotation;

            HandleInput(e, SnappedPos); //Handle user input
        }
        SceneView.RepaintAll();
    }

    private Vector3 SnapToGrid(Vector3 pos)     //alligning the tiles to the centre of the grid.
    {
        float x = Mathf.Floor(pos.x / CellSize) * CellSize;
        float z = Mathf.Floor(pos.z / CellSize) * CellSize;
        float y = pos.y + YOffset;
        return new Vector3(x, y, z);
    }
    private void HandleInput(Event e, Vector3 Position) //Handling the clicks, user inputs, again.
    {
        if (e.type == EventType.KeyDown)
        {
            if (e.keyCode == KeyCode.R)
            {
                PreviewRotation *= Quaternion.Euler(0, 90, 0); //Rotate clockwise.
                e.Use();
            }
            else if (e.keyCode == KeyCode.T)
            {
                PreviewRotation *= Quaternion.Euler(0, -90, 0); //Rotate counter clockwise
                e.Use(); //This just prevents unity from repeating this event over and over
            }
        }
        if (e.type == EventType.MouseDown && e.button == 0) //On left click, place tile.
        {
            PlaceTile(Position);
            e.Use();
        }
        if (e.type == EventType.MouseDown && e.button == 1)
        {
            DeleteTile(Position);
            e.Use();
        }
    }
    private void PlaceTile(Vector3 Position)
    {
        GameObject NewTile = (GameObject)PrefabUtility.InstantiatePrefab(TilePrefab); //Create the relevant prefab, import all settings, etc.
        NewTile.transform.position = Position;
        NewTile.transform.rotation = PreviewRotation;
    }
    private void DeleteTile(Vector3 position)
    {
       
        foreach (var obj in GameObject.FindObjectsOfType<GameObject>()) //we are looking for any object in the clicked position
        {
            // Must be a prefab instance AND at the tile position
            if (obj.transform.position == position &&
                PrefabUtility.IsPartOfPrefabInstance(obj))
            {
                Undo.DestroyObjectImmediate(obj); //We are destroying the object thats in the selected grid.
                return;
            }
        }
    }
}

/* Documentation and functions being called, good to read up on.

https://docs.unity3d.com/ScriptReference/EditorWindow.html
https://docs.unity3d.com/ScriptReference/PrefabUtility.InstantiatePrefab.html
https://docs.unity3d.com/ScriptReference/SceneView-duringSceneGui.html
https://docs.unity3d.com/ScriptReference/PrefabUtility.GetPrefabInstanceStatus.html
https://docs.unity3d.com/ScriptReference/PrefabInstanceStatus.html
https://docs.unity3d.com/ScriptReference/Undo.DestroyObjectImmediate.html
https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
*/