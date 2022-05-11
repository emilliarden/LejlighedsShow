using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

public class Program
{
    static void Main(string[] args)
    {
        List<string> fonde = new List<string>();
        fonde.Add("arendal"); 
        fonde.Add("enghaven");
        fonde.Add("solgaarden");
        fonde.Add("soendergaarden");
        fonde.Add("vestergaarden");
        fonde.Add("oestergaarden");
        
        fonde.Add("FrederiksbergBoligfond-Barfoeds-Gaard");
        fonde.Add("FrederiksbergBoligfond-bjarkeshus");
        fonde.Add("FrederiksbergBoligfond-Den-soenderjyske-by");
        fonde.Add("FrederiksbergBoligfond-Firkloveren");
        fonde.Add("FrederiksbergBoligfond-Lauritz-Soerensens-Gaard");
        fonde.Add("FrederiksbergBoligfond-Lindehuset");
        fonde.Add("FrederiksbergBoligfond-Lineagaarden");
        fonde.Add("FrederiksbergBoligfond-malthe-bruuns-gaard");
        fonde.Add("FrederiksbergBoligfond-Moellehuset");
        fonde.Add("FrederiksbergBoligfond-Moensterbo");
        fonde.Add("FrederiksbergBoligfond-rolfshus");
        fonde.Add("FrederiksbergBoligfond-Roskildegaarden");
        fonde.Add("FrederiksbergBoligfond-Solbjerggaard");
        fonde.Add("FrederiksbergBoligfond-Storkereden");
        fonde.Add("FrederiksbergBoligfond-Svalegaarden");
        fonde.Add("FrederiksbergBoligfond-Soenderjyllandsgaarden");
        fonde.Add("FrederiksbergBoligfond-Trekanten");
        fonde.Add("FrederiksbergBoligfond-Tvillingeaarden");
        fonde.Add("FrederiksbergBoligfond-Wilkenbo");
        

        foreach (var fond in fonde)
        {
            var currentContent = GetHTMLString(fond);
            
            var filePath = "/Files/" + fond + ".txt";
            
            
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, currentContent);
                Console.WriteLine("Første gang!");
            }
            else
            {
                var oldContent = File.ReadAllText(filePath);
                
                if (!oldContent.Equals(currentContent))
                {
                    SendSMS(fond);
                    Console.WriteLine(fond + " - ÅBEN!!!!");
                }
                else
                {
                    Console.WriteLine("Stadig lukket!");
                }
            }

        }

    }

    public static void SendSMS(string fond)
    {
        {
            string accountSid = "AC9c7c8817fcecfaf8d8b3dc14ded93a6b";
            string authToken = "b687fb56579bfac9f15ee8af5dbb5d3f";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "https://findbolig.nu/LandingPages/"+ fond + " ER ÅBEN!!!",
                from: new Twilio.Types.PhoneNumber("+19853138843"),
                to: new Twilio.Types.PhoneNumber("+4551908288")
            );

            Console.WriteLine(message.Sid);
        }
    }

    public static string GetHTMLString(string url)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://findbolig.nu/LandingPages/");
        var response = client.GetAsync(url).Result;
        var content = response.Content;
        var pageContent = content.ReadAsStringAsync().Result;
        return pageContent;
    }
    
}

