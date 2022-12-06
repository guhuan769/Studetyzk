namespace ConfigServices
{
    //提取环境变量中的配置 启用项目--调试打开URL
    public class EnvVarConfigService : IConfigService
    {
        public string GetValue(string name)
        {
            return Environment.GetEnvironmentVariable(name);
        }
    }
}