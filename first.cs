using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace RTest
{
public class Resturaunt
{
public string Name { get; set; }
public string Address { get; set; }
public int Zipcode { get; set; }
public Categories Category { get; set; }
public virtual string GetName()
{
return this.Name;
}
public override string ToString()
{
return string.Format("{0},{1},{2},{3}", this.Name, this.Address, this.Zipcode, this.Category);
}
}
public class Program
{
static Resturaunt[] resteraunts;
public static void Main()
{
resteraunts = ReadFile("r.txt");
var rand = new Random();
var t = Task.Run(() =>
{
while (true)
{
foreach (var r in GetByZip(rand.Next(98109, 98110)))
{
Console.WriteLine(r);
}
Thread.Sleep(5000);
}
});
　
while (true)
{
Thread.Sleep(500);
}
}
public static void GenerateFile(string fileName)
{
var rand = new Random();
var rest = new List<Resturaunt>();
for (int i = 0; i < 100; i++)
{
rest.Add(new Resturaunt
{
Address = rand.Next(100, 1000) + " Main",
Category = (Categories)rand.Next(1, 5),
Name = "Random",
Zipcode = rand.Next(98108, 98118)
});
}
using (var write = new StreamWriter("r.txt"))
{
foreach (var r in rest)
{
write.WriteLine(r);
}
}
}
public static Resturaunt[] ReadFile(string fileName)
{
List<Resturaunt> resteraunts = new List<Resturaunt>();
using (var reader = new StreamReader(fileName))
{
foreach (var line in reader.ReadToEnd().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
{
var r = line.Split(',');
var res = new Resturaunt
{
Name = r[0],
Address = r[1],
Zipcode = int.Parse(r[2]),
Category = (Categories)Enum.Parse(typeof(Categories), r[3])
};
resteraunts.Add(res);
}
}
return resteraunts.ToArray();
}
public static Resturaunt[] GetByZip(int zip)
{
return resteraunts.Where(x => x.Zipcode == zip).ToArray();
}
}
}
