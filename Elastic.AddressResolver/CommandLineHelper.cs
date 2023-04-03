namespace Elastic.AddressResolver
{
    internal static class CommandLineHelper
    {
        public static string ExtractParameter(string[] args, string name)
        {

            int index = args.ToList().FindIndex(a => a == name);
            if (index >= 0)
            {
                return args[index + 1];
            }
            else
            {
                return null;
            }

        }
    }
}
