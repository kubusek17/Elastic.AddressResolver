using Elastic.AddressResolver;

namespace MyProject;
class Program
{
    static void Main(string[] args)
    {
        var steeringArguments = new Arguments();
        var hostnames = args.ToList();
        steeringArguments.Dns = CommandLineHelper.ExtractParameter(args, "-dns");
        steeringArguments.Output = CommandLineHelper.ExtractParameter(args, "-output");
        steeringArguments.Input = CommandLineHelper.ExtractParameter(args, "-input");
        if (steeringArguments != null)
        {
            Resolver resolver;
            if (steeringArguments.Dns != null)
            {
                resolver = new Resolver(steeringArguments.Dns);
            }
            else
            {
                resolver = new Resolver();
            }
            if (steeringArguments.Input != null)
            {
                hostnames.AddRange(System.IO.File.ReadAllLines(steeringArguments.Input));
            }
            resolver.Resolve(hostnames);

            if (steeringArguments.Output != null)
            {
                System.IO.File.WriteAllLines(steeringArguments.Output, resolver.Results);
            }
            else
            {
                foreach (var result in resolver.Results)
                {
                    Console.WriteLine(result);
                }
            }
        }

    }
}