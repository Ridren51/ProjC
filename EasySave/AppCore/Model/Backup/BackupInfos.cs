using System.Xml.Linq;

namespace AppCore.Model
{
    [Serializable]
    public class BackupInfos
    {
        public string BackupName;
        public string SourceDir;
        public string TargetDir;
        public string BackupType;

        public BackupInfos(string backupName, string sourceDir, string targetDir, string backupType)
        {
            BackupName = backupName;
            SourceDir = sourceDir;
            TargetDir = targetDir;
            BackupType = backupType;
        }
        public string Name
        {
            get => BackupName; set => BackupName = value;
        }
        public string SourceDirectory
        {
            get => SourceDir; set => SourceDir = value;
        }
        public string TargetDirectory
        {
            get => TargetDir; set => TargetDir = value;
        }
        public string Type
        {
            get => BackupType; set => BackupType = value;
        }
    }
}
