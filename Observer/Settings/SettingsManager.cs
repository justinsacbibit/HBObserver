using Styx.WoWInternals;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;
using System.Text;

namespace Observer.Settings
{
    public class SettingsManager
    {
        public Settings Settings { get; private set; }

        public SettingsManager()
        {
            try
            {
                using (var fileStream = new FileStream(SettingsPath, FileMode.Open))
                using (var streamReader = new StreamReader(fileStream))
                {
                    var json = streamReader.ReadToEnd();
                    Settings = JsonConvert.DeserializeObject<Settings>(json);
                }
            }
            catch
            {
                Settings = new Settings();
            }
        }

        public void SaveSettings()
        {
            string json = JsonConvert.SerializeObject(Settings);
            byte[] bytes = Encoding.ASCII.GetBytes(json);

            using (var fileStream = new FileStream(SettingsPath, FileMode.Create))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }

        private static string SettingsPath
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Settings", CharSettingsFileName);
            }
        }

        private static string CharSettingsFileName
        {
            get
            {
                try
                {
                    return string.Format("{0}-{1}", Lua.GetReturnVal<string>("return GetUnitName(\"player\")", 0), Lua.GetReturnVal<string>("return GetRealmName()", 0));
                }
                catch
                {
                    return "NoName-NoRealm";
                }
            }
        }
    }
}
