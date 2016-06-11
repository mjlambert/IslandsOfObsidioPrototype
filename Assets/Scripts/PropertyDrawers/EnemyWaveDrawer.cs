using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

// TODO - Can make pretty when game works

//[CustomPropertyDrawer(typeof(EnemyWave))]
//public class EnemyWaveDrawer : PropertyDrawer
//{
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        EditorGUI.BeginProperty(position, label, property);

//        // Change label to Wave n
//        Regex elementIndex = new Regex(@"\d+$");
//        int index = int.Parse(elementIndex.Match(label.text).Value);
//        label.text = string.Format("Wave {0}", index + 1);

//        bool showWave = EditorGUI.Foldout(position, true, label);

//        if (showWave)
//        {
//            SerializedProperty enemyUnitProperty = property.FindPropertyRelative("enemyUnit");
//            SerializedProperty spawnRateProperty = property.FindPropertyRelative("spawnRate");
//            SerializedProperty spawnLimitProperty = property.FindPropertyRelative("spawnLimit");

//            Rect enemyUnitRect = new Rect(position.x, position.y + 10, position.width, position.height);

//            EditorGUI.PropertyField(enemyUnitRect, enemyUnitProperty);

//        }
        

//        EditorGUI.EndProperty();
//    }


//}
