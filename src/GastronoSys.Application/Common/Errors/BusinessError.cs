namespace GastronoSys.Application.Common.Errors
{
    public class BusinessError
    {
        public string Key { get; set; }
        public object[] Args { get; set; }

        public BusinessError(string key, params object[] args)
        {
            Key = key;
            Args = args;
        }
    }
}
