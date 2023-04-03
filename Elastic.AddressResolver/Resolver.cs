using DnsClient;
using System.Net;

namespace Elastic.AddressResolver
{
    public class Resolver
    {
        ILookupClient Client;
        public List<string> Results;

        public Resolver()
        {
            Client = new LookupClient();
            Results = new List<string>();
        }
        public Resolver(string dns)
        {
            Client = new LookupClient(IPAddress.Parse(dns));
            Results = new List<string>();

        }

        public void Resolve(IEnumerable<string> domains)
        {
            Results.Clear();
            var result = Parallel.ForEach(domains, domain =>
            {
                Resolve(domain);
            });

        }

        private void Resolve(string domain)
        {
            try
            {

                var records = Client.Query(domain, QueryType.MX);
                var answerServer = records.NameServer.ToString().Split(':')[0];
                foreach (var record in records.Answers)
                {
                    var elements = record.ToString().Split(' ');
                    if (elements[3] == "MX")
                    {
                        Results.Add(String.Join(' ', domain, "MX preference = ", elements[4], ",mail exchanger = ", elements[5], answerServer));
                    }
                }
            }
            catch (DnsResponseException e)
            {
                Results.Add(String.Join(' ', "Errror!," + e.Message));
            }
            catch (AggregateException f)
            {
                Results.Add(String.Join(' ', "Errror!," + f.Message));
            }
            catch (Exception g)
            {
                Results.Add(String.Join(' ', "Errror!," + g.Message));
            }

        }

    }
}
