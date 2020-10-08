public class Config
{
    public enum DebugLevelEnum
    {
        Notice,
        Warning,
        Error,
    }
    
    public const DebugLevelEnum DebugLevel = DebugLevelEnum.Notice;
}