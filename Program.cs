namespace csharp_lista_indirizzi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "C://Users//Programmazione//Downloads/addresses.csv";
            GetAddress(path);

        }

        public static List<Address> GetAddress(string path) 
        {
            List<Address> addresses = new List<Address>();
            var stream = File.OpenText(path);
            int counter = 0;

            while (!stream.EndOfStream)
            {
                var line = stream.ReadLine();
                Console.WriteLine(line);

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
