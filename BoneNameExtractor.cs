using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BoneNameExtractor
{

    public class BoneNameExtractor : MonoBehaviour { }


    #if UNITY_EDITOR
    [CustomEditor(typeof(BoneNameExtractor))]
    public class BoneNameExtractorUI : Editor
    {
        public Animator HumanoidAnimator;
        string logs;

        public override void OnInspectorGUI()
        {
            //元のInspector部分を表示
            base.OnInspectorGUI();

            //ボタンを表示
            if (GUILayout.Button("実行"))
            {
                BoneNameExtractor bnExtracotr = target as BoneNameExtractor;
                HumanoidAnimator = bnExtracotr.GetComponent<Animator>();

                if (HumanoidAnimator == null)
                {
                    Debug.Log("AnimatorIsNull");
                    return;
                }
                if (HumanoidAnimator.isHuman == false)
                {
                    Debug.Log("AvatarIsNotHuman");
                    return;
                }
                BoneNameExtract();
            }
        }

        private void BoneNameExtract()
        {

            logs = $"<NAME>{HumanoidAnimator.name}</NAME>\n";

            foreach (HumanBodyBones bone in Enum.GetValues(typeof(HumanBodyBones)))
            {
                Transform boneTf;
                try
                {
                    boneTf = HumanoidAnimator.GetBoneTransform(bone);
                }catch(IndexOutOfRangeException e)
                {
                    boneTf = null;
                }
                String boneID = bone.ToString();
                String boneName = boneTf != null ? boneTf.name : "NONE";

                logs = logs + $"<{boneID}>{boneName}</{boneID}>\n";
            }
            logs = logs +
                "------Options------\n" +
                "<RightBreastRoot>NONE</RightBreastRoot>\n" +
                "<RightBreastMid>NONE</RightBreastMid>\n" +
                "<RightBreastEnd>NONE</RightBreastEnd>\n" +
                "<LeftBreastRoot>NONE</LeftBreastRoot>\n" +
                "<LeftBreastMid>NONE</LeftBreastMid>\n" +
                "<LeftBreastEnd>NONE</LeftBreastEnd>\n";
            Debug.Log(logs);
        }
    }
    #endif
}
