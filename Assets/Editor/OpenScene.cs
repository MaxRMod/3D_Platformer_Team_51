using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class OpenScene : MonoBehaviour
{
   

   
   //Desert, LevelOne,LevelThree, LevelTwo, MainMenu, Spiellevel, TestScene, SummerForest
   [MenuItem("OpenScene/MainMenu")]
   static void MainMenu(){
       EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
       EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity");
   }
   [MenuItem("OpenScene/Desert")]
   static void Desert(){
       EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
       EditorSceneManager.OpenScene("Assets/Scenes/Desert.unity");
   }
   [MenuItem("OpenScene/LevelOne")]
   static void LevelOne(){
       EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
       EditorSceneManager.OpenScene("Assets/Scenes/LevelOne.unity");
   }
[MenuItem("OpenScene/LevelTwo")]
   static void LevelTwo(){
       EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
       EditorSceneManager.OpenScene("Assets/Scenes/LevelTwo.unity");
   }
   [MenuItem("OpenScene/LevelThree")]
   static void LevelThree(){
       EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
       EditorSceneManager.OpenScene("Assets/Scenes/LevelThree.unity");
   }
    [MenuItem("OpenScene/Testscene")]
   static void TestScene(){
       EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
       EditorSceneManager.OpenScene("Assets/Scenes/TestScene.unity");
   }
   [MenuItem("OpenScene/SummerForest")]
   static void SummerForest(){
       EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
       EditorSceneManager.OpenScene("Assets/Scenes/SummerForest.unity");
   }
    [MenuItem("OpenScene/Spiellevel")]
   static void Spiellevel(){
       EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
       EditorSceneManager.OpenScene("Assets/Scenes/Spiellevel.unity");
   }
}
