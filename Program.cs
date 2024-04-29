using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace csharp_lista_indirizzi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "C://Users//Programmazione//Downloads/addresses.csv";
            List<Address> newAddresses = GetAddress(path);
            PrintList(newAddresses);
            var newList= JsonConvert.SerializeObject(newAddresses);
            System.IO.File.WriteAllText(@"C://Users//Programmazione//Downloads/new_address_file.json", newList);
        }

        public static void PrintList(List<Address> list)
        {
            int counter = 0;
            Console.WriteLine($"ID, NAME, SURNAME, STREET , CITY, PROVINCE, ZIP");
            foreach (Address address in list)
            {
                counter++;
                Console.WriteLine($"{counter}) {address.Name}, {address.Surname}, {address.Street}, {address.City}, {address.Province}, {address.Zip}");
            }
        }
        //Return a list of addresses fromm a csv file
        public static List<Address> GetAddress(string path) 
        {
            List<Address> addresses = new List<Address>();
            var stream = File.OpenText(path);
            int counter = 0;

            while (!stream.EndOfStream)
            {
                var line = stream.ReadLine();
                //Console.WriteLine(line);

                counter++;
                if (counter <= 1)
                    continue;
                try
                {
                    string[] info = line.Split(",");
                    if (info.Length >= 7)
                    {
                        //Removes the element in position [2] in an array
                        info = info.Where((val, idx) => idx != 2).ToArray();
                    }
                    if (info.Length < 6)
                    {
                        throw new Exception("Some information is missed!");
                    }
                    if (info.Contains(""))
                    {
                        throw new Exception("There shouldn't be empty fields!");

                    }
                    string name = info[0];
                    string surname = info[1];
                    string street = info[2];
                    string city = info[3];
                    string province = info[4];
                    int zip = int.Parse(info[5]);

                    Address address = new Address(name, surname, street, city, province, zip);
                    addresses.Add(address);
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error: {e.Message} {e.StackTrace}");
                }
                
            }
            stream.Close();
            return addresses;
        }
    }
}
