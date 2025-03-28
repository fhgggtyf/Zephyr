using System;
using System.IO;
using UnityEngine;

public static class FileManager
{
	private static string GetSavePath(string fileName)
	{
#if UNITY_EDITOR
		// Save inside the project directory in Editor
		return Path.Combine(Application.dataPath, "../Saves", fileName);
#else
        // Save in persistent data path in builds
        return Path.Combine(Application.persistentDataPath, fileName);
#endif
	}

	public static bool WriteToFile(string fileName, string fileContents)
	{
		var fullPath = GetSavePath(fileName);

		try
		{
			string savePath = Path.Combine(Application.dataPath, "../Saves/save.zyp");
			string directory = Path.GetDirectoryName(savePath);

			// Ensure the directory exists
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}

			// Write to the file
			File.WriteAllText(fullPath, fileContents);
			Debug.Log("Saved");
			return true;
		}
		catch (Exception e)
		{
			Debug.LogError($"Failed to write to {fullPath} with exception {e}");
			return false;
		}
	}

	public static bool LoadFromFile(string fileName, out string result)
	{
		var fullPath = Path.Combine(Application.persistentDataPath, fileName);
		if (!File.Exists(fullPath))
		{
			File.WriteAllText(fullPath, "");
		}
		try
		{
			result = File.ReadAllText(fullPath);
			return true;
		}
		catch (Exception e)
		{
			Debug.LogError($"Failed to read from {fullPath} with exception {e}");
			result = "";
			return false;
		}
	}

	public static bool MoveFile(string fileName, string newFileName)
	{
		var fullPath = Path.Combine(Application.persistentDataPath, fileName);
		var newFullPath = Path.Combine(Application.persistentDataPath, newFileName);

		try
		{
			if (File.Exists(newFullPath))
			{
				File.Delete(newFullPath);
			}

			if (!File.Exists(fullPath))
			{
				return false;
			}

			File.Move(fullPath, newFullPath);
		}
		catch (Exception e)
		{
			Debug.LogError($"Failed to move file from {fullPath} to {newFullPath} with exception {e}");
			return false;
		}

		return true;
	}
}
