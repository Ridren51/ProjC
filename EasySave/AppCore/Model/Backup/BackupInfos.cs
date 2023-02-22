namespace AppCore.Model
{
    [Serializable]
    public class BackupInfos
    {
        public string BackupName;
        public string SourceDir;
        public string TargetDir;
        public string BackupType;
        public int Index;

        public BackupInfos(string backupName, string sourceDir, string targetDir, string backupType, int index)
        {
            BackupName = backupName;
            SourceDir = sourceDir;
            TargetDir = targetDir;
            BackupType = backupType;
            Index = index;
        }
    }
}
