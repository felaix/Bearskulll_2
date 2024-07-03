using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class Cheater : MonoBehaviour
{
    [SerializeField] private Transform _targetPosition;

    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }
    public async void Teleport()
    {
        Debug.Log("CHEAT TELEPORT ACTIVE");
        _player.GetComponent<Movement>().enabled = false;
        _player.position = _targetPosition.position;
        await Task.Delay(100);
        _player.GetComponent<Movement>().enabled = true;

    }

}



[CustomEditor(typeof(Cheater))]
public class CheatEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Cheater cheater = (Cheater)target;

        EditorGUILayout.LabelField("Cheater");
        EditorGUILayout.HelpBox("This is only for development", MessageType.Info);

        if (GUILayout.Button("Teleport"))
        {
            cheater.Teleport();
        }
    }
}
