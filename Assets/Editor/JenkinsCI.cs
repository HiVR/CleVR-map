// <copyright file="JenkinsCI.cs" company="HiVR">
//     HiVR All rights reserved.
// </copyright>
namespace Assets.Editor
{
    using UnityEditor;
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// JenkinsCI is used as en entry point for testing.
    /// </summary>
    public class JenkinsCI
    {
        #region Fields

        /// <summary>
        /// An array of all scenes.
        /// </summary>
        static string[] SCENES = FindEnabledEditorScenes();

        /// <summary>
        /// The name of the app.
        /// </summary>
        static string APP_NAME = "CleVR-map";

        /// <summary>
        /// Target directory.
        /// </summary>
        static string TARGET_DIR = "target";

        #endregion Fields

        #region Methods

        /// <summary>
        /// Entry point to start a build.
        /// </summary>
        [MenuItem("Custom/CI/Build on Jenkins CI")]
        static void PerformWindowsBuild()
        {
            string target_dir = APP_NAME + ".exe";
            GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.StandaloneWindows, BuildOptions.None);
        }

        /// <summary>
        /// Find all scenes.
        /// </summary>
        private static string[] FindEnabledEditorScenes()
        {
            List<string> EditorScenes = new List<string>();
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (!scene.enabled) continue;
                EditorScenes.Add(scene.path);
            }
            return EditorScenes.ToArray();
        }

        /// <summary>
        /// Start a build on all collected scenes, into the target folder.
        /// </summary>
        static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
            string res = BuildPipeline.BuildPlayer(scenes, target_dir, build_target, build_options);
            if (res.Length > 0)
            {
                throw new Exception("BuildPlayer failure: " + res);
            }
        }

        #endregion Methods
    }
}