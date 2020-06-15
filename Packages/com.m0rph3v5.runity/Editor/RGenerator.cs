﻿using System.IO;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEditor.Animations;

namespace Packages.Editor {
	
	[InitializeOnLoad]
	public static class RGenerator {

		[MenuItem ("Tools/R.Unity/Regenerate")]
	    private static void WriteCodeFile() { 
		    Debug.Log("Writing new R.Unity");

	        // the path we want to write to
	        var path = string.Concat( Application.dataPath, Path.DirectorySeparatorChar, "Scripts/RGenerated.cs" );
	        if (File.Exists(path)) {
	            File.Delete(path);
	        }

	        try {
	            // opens the file if it allready exists, creates it otherwise
	            using (FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write)) {
	                using(StreamWriter writer = new StreamWriter(stream))
	                {
						var assetsDirectory = new DirectoryInfo(Application.dataPath);

	                    StringBuilder builder = new StringBuilder();
	                    builder.AppendLine("// ----- Generated by R.Unity ----- //");
	                    builder.AppendLine("using UnityEngine;\n");
	                    builder.AppendLine("namespace R {");

						builder.AppendLine("\tpublic static class Tag {");

						var tags = UnityEditorInternal.InternalEditorUtility.tags;
						tags.ToList().Sort();
						foreach(var tag in tags) {
							builder.AppendFormat("\t\tpublic static readonly string {0} = \"{1}\";\n", tag, tag);
						}

	                    builder.AppendLine("\t}\n");

						builder.AppendLine("\tpublic static class Scene {");

						var files = assetsDirectory.GetFiles("*.unity", SearchOption.AllDirectories);
						foreach(var fileInfo in files) {
							var filename = Path.GetFileNameWithoutExtension(fileInfo.Name);
							builder.AppendFormat("\t\tpublic const string {0} = \"{1}\";\n", SanitizedCSharpName(filename), filename);
						}

						builder.AppendLine("\t}\n");

	                    builder.AppendLine("\tpublic static class Layer {");

	                    foreach (var layer in UnityEditorInternal.InternalEditorUtility.layers) {
	                        builder.AppendFormat("\t\tpublic static readonly string {0} = \"{1}\";\n", SanitizedCSharpName(layer), layer);
	                        builder.AppendFormat("\t\tpublic static readonly LayerMask {0} = 1 << {1};\n", SanitizedCSharpName(layer + "Mask"), LayerMask.NameToLayer(layer));
	                    }

	                    builder.AppendLine("\t}\n");

	                    builder.AppendLine("\tpublic static class Animator {");

	                    var animators = assetsDirectory.GetFiles("*.controller", SearchOption.AllDirectories);

	                    foreach(var fileInfo in animators) {
	                        var c = AssetDatabase.LoadAssetAtPath<AnimatorController>(fileInfo.FullName.Replace(Application.dataPath.Replace("/", "\\"), "Assets"));
	                        if (c == null) continue;
	                        if (c.layers.Length <= 0) continue;

	                        var filename = Path.GetFileNameWithoutExtension(fileInfo.Name);
	                        builder.AppendFormat("\t\tpublic static class {0}Animator {{\n", SanitizedCSharpName(filename));
	                        foreach (var state in c.layers[0].stateMachine.states) {
	                            builder.AppendFormat("\t\t\tpublic static readonly string {0} = \"{1}\";\n", SanitizedCSharpName(state.state.name), state.state.name);
	                        }
	                        builder.AppendLine("\t\t}\n");
	                    }

	                    builder.AppendLine("\t}\n");

          						builder.AppendLine("\tpublic static class SortingLayer {");

          						var allSortingLayerNames = SortingLayer.layers.Select(layer => layer.name).ToArray();
          						foreach (var sortingLayer in allSortingLayerNames) {
          							builder.AppendFormat("\t\tpublic static readonly string {0} = \"{1}\";\n", SanitizedCSharpName(sortingLayer), sortingLayer);
          						}
          						var sanitizedNames = allSortingLayerNames.Select(layer => SanitizedCSharpName(layer)).ToArray();

          						builder.AppendLine("\t\tpublic static readonly string[] All = new string[]{ " + string.Join(", ", sanitizedNames) + " };");
          						builder.AppendLine("\t}\n");

	                    builder.AppendLine("}");
	                    writer.Write(builder.ToString());
	                }
	            }
	        }
	        catch(System.Exception e) {
	            Debug.LogException(e);
	            // if we have an error, it is certainly that the file is screwed up. Delete to be safe
	            if(File.Exists( path )) File.Delete( path );
	        }

			    AssetDatabase.Refresh(); // force compile
	    }

		public static string SanitizedCSharpName(string input) {
			var results = Regex.Split(input, @"[\p{P}\p{Z}\p{S}]");
			var result = results.Aggregate("", (acc, x) => acc + x);
			result = Regex.Replace(result, "^[0-9]+", "");
			return result;
		}
	}
}

