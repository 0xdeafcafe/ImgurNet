using System;
using System.IO;
using ImgurNet.Tests.Models;
using Newtonsoft.Json;

namespace ImgurNet.Tests.Helpers
{
	public static class VariousFunctions
	{
		public static string GetTestsDirectory()
		{
			return AppDomain.CurrentDomain.BaseDirectory + @"\";
		}

		public static string GetTestsAssetDirectory()
		{
			return AppDomain.CurrentDomain.BaseDirectory + @"\Assets\";
		}

		public static string GetTestsProjectDirectory()
		{
			var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
			if (directoryInfo != null)
				if (directoryInfo.Parent != null) 
					return directoryInfo.FullName + @"\";

			throw new Exception("dicks");
		}

		public static TestSettings LoadTestSettings()
		{
			if (!File.Exists(GetTestsDirectory() + "settings.json"))
				throw new InvalidOperationException("The settings file is not present. Rename example.settings.json to settings.json and fill it with the correct data.");
			
			return JsonConvert.DeserializeObject<TestSettings>(File.ReadAllText(GetTestsDirectory() + "settings.json"));
		}

		public static void SaveTestSettings(TestSettings settings)
		{
			var jsonString = JsonConvert.SerializeObject(settings);

			File.WriteAllText(GetTestsDirectory() + "settings.json", jsonString);
			File.WriteAllText(GetTestsProjectDirectory() + "settings.json", jsonString);
		}
	}
}
