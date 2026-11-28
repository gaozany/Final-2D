using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class SetupCatAnimations
{
    private const string SpritePath = "Assets/Sprites/characters/cat_fighter_updated.png";
    private const string FolderPath = "Assets/Animations/Cat";
    private const string ControllerPath = FolderPath + "/Cat.controller";

    static SetupCatAnimations()
    {
        EditorApplication.delayCall += Setup;
    }

    private static void Setup()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            return;
        }

        if (!AssetDatabase.IsValidFolder(FolderPath))
        {
            AssetDatabase.CreateFolder("Assets/Animations", "Cat");
        }

        Dictionary<string, Sprite> sprites = AssetDatabase.LoadAllAssetsAtPath(SpritePath)
            .OfType<Sprite>()
            .ToDictionary(sprite => sprite.name, sprite => sprite);

        if (sprites.Count == 0)
        {
            return;
        }

        AnimationClip idle = CreateClip("Cat_Idle", GetSprites(sprites,
            "cat_fighter_updated_0", "cat_fighter_updated_1", "cat_fighter_updated_2"), 6f, true);
        AnimationClip walk = CreateClip("Cat_Walk", GetSprites(sprites,
            "cat_walk_1", "cat_walk_2", "cat_walk_3"), 9f, true);
        AnimationClip jump = CreateClip("Cat_Jump", GetSprites(sprites,
            "cat_jumping"), 1f, true);
        AnimationClip kick = CreateClip("Cat_Kick", GetSprites(sprites,
            "cat_kick_1", "cat_kick_2", "cat_kick_3", "cat_kick_4", "cat_kick_5",
            "cat_kick_6", "cat_kick_7", "cat_kick_8", "cat_kick_9"), 18f, false);

        AnimatorController controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(ControllerPath);
        if (controller == null)
        {
            controller = AnimatorController.CreateAnimatorControllerAtPath(ControllerPath);
            controller.AddParameter("walking", AnimatorControllerParameterType.Bool);
            controller.AddParameter("jumping", AnimatorControllerParameterType.Bool);
            controller.AddParameter("kicking", AnimatorControllerParameterType.Bool);

            AnimatorStateMachine machine = controller.layers[0].stateMachine;
            AnimatorState idleState = machine.AddState("Idle");
            AnimatorState walkState = machine.AddState("Walking");
            AnimatorState jumpState = machine.AddState("Jumping");
            AnimatorState kickState = machine.AddState("Kicking");
            idleState.motion = idle;
            walkState.motion = walk;
            jumpState.motion = jump;
            kickState.motion = kick;
            machine.defaultState = idleState;

            AddTransition(idleState, walkState, "walking", true);
            AddTransition(walkState, idleState, "walking", false);
            AddTransition(idleState, jumpState, "jumping", true);
            AddTransition(walkState, jumpState, "jumping", true);
            AddTransition(jumpState, idleState, "jumping", false);
            AddTransition(jumpState, walkState, "walking", true, "jumping", false);

            AnimatorStateTransition kickTransition = machine.AddAnyStateTransition(kickState);
            kickTransition.hasExitTime = false;
            kickTransition.duration = 0f;
            kickTransition.AddCondition(AnimatorConditionMode.If, 0f, "kicking");

            AnimatorStateTransition kickReturn = kickState.AddTransition(idleState);
            kickReturn.hasExitTime = true;
            kickReturn.exitTime = 0.95f;
            kickReturn.duration = 0f;
            kickReturn.AddCondition(AnimatorConditionMode.IfNot, 0f, "kicking");
        }

        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            Animator animator = player.GetComponent<Animator>();
            if (animator == null)
            {
                animator = player.AddComponent<Animator>();
            }

            animator.runtimeAnimatorController = controller;
            EditorUtility.SetDirty(player);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        }

        AssetDatabase.SaveAssets();
    }

    private static Sprite[] GetSprites(Dictionary<string, Sprite> sprites, params string[] names)
    {
        return names.Where(sprites.ContainsKey).Select(name => sprites[name]).ToArray();
    }

    private static AnimationClip CreateClip(string name, Sprite[] frames, float frameRate, bool loop)
    {
        string path = FolderPath + "/" + name + ".anim";
        AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);
        if (clip != null)
        {
            return clip;
        }

        clip = new AnimationClip { frameRate = frameRate };
        ObjectReferenceKeyframe[] keys = new ObjectReferenceKeyframe[frames.Length];
        for (int i = 0; i < frames.Length; i++)
        {
            keys[i] = new ObjectReferenceKeyframe
            {
                time = i / frameRate,
                value = frames[i]
            };
        }

        EditorCurveBinding binding = EditorCurveBinding.PPtrCurve("", typeof(SpriteRenderer), "m_Sprite");
        AnimationUtility.SetObjectReferenceCurve(clip, binding, keys);
        AnimationClipSettings settings = AnimationUtility.GetAnimationClipSettings(clip);
        settings.loopTime = loop;
        AnimationUtility.SetAnimationClipSettings(clip, settings);
        AssetDatabase.CreateAsset(clip, path);
        return clip;
    }

    private static void AddTransition(AnimatorState from, AnimatorState to,
        string parameter, bool value, string secondParameter = null, bool secondValue = false)
    {
        AnimatorStateTransition transition = from.AddTransition(to);
        transition.hasExitTime = false;
        transition.duration = 0f;
        transition.AddCondition(value ? AnimatorConditionMode.If : AnimatorConditionMode.IfNot, 0f, parameter);
        if (!string.IsNullOrEmpty(secondParameter))
        {
            transition.AddCondition(secondValue ? AnimatorConditionMode.If : AnimatorConditionMode.IfNot,
                0f, secondParameter);
        }
    }
}
