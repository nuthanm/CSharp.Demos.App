////int i = 0;
////try
////{
////    i = 1;
////}
////catch (Exception e)
////{
////    i = 2;
////}
////finally
////{
////    i = 3;
////}

////Console.WriteLine($"Current value of i is {i}");

////int j = 0;
////try
////{
////    j = j / 0;
////}
////catch (Exception e)
////{
////    j = 2;
////}
////finally
////{
////    j = 3;
////}

////Console.WriteLine($"Current value of j is {j}");


////int k = 0;
////try
////{
////    k = k / 0;
////}
////catch (Exception e)
////{
////    k = 2;
////    throw;
////}
////finally
////{
////    k = 3;
////}

////Console.WriteLine($"Current value of k is {k}");


////Console.WriteLine($"How many times a person voted for a president: {CalculatePresident(18)}");
////Console.WriteLine($"How many times a person voted for a president: {CalculatePresident(32)}");

////try
////{
////    DoSomething();// Line 10
////}
////catch (Exception ex)
////{
////    Console.WriteLine(ex.StackTrace);// Line 14
////}

////void DoSomething()
////{
////    try
////    {
////        int number = 20;
////        int division = 0;
////        Console.WriteLine(number / division);// Line 23
////    }
////    catch (Exception ex)
////    {
////        throw ex;// Line 27
////    }
////}

//// Input for test purposes. Change the values to see
//// exception handling behavior.
////double a = 98, b = 0;
////double result;

////try
////{
////    result = SafeDivision(a, b);
////    Console.WriteLine("{0} divided by {1} = {2}", a, b, result);
////}
////catch (DivideByZeroException)
////{
////    Console.WriteLine("Attempted divide by zero.");
////}

////static double SafeDivision(double x, double y)
////{
////    if (y == 0)
////        throw new DivideByZeroException();
////    return x / y;
////}

////Console.ReadLine();

////using CSharp.Demos.Console;

////int i = 123;
////string s = "Some string";
////object obj = s;

////try
////{
////    // Invalid conversion; obj contains a string, not a numeric type.
////    i = (int)obj;

////    // The following statement is not run.
////    Console.WriteLine("WriteLine at the end of the try block.");
////}
////finally
////{
////    // To run the program in Visual Studio, type CTRL+F5. Then
////    // click Cancel in the error dialog.
////    Console.WriteLine("\nExecution of the finally block after an unhandled\n" +
////        "error depends on how the exception unwind operation is triggered.");
////    Console.WriteLine("i = {0}", i);
////}

////Console.ReadLine();


////var radio = new Radio();
////radio.SetVolume(120);
////Console.ReadLine();

////public class Radio
////{
////    public int Volume { get; set; }
////    public string Station { get; set; }

////    public void SetVolume(int volume)
////    {
////        if (volume > 100)
////        {
////            throw new ArgumentOutOfRangeException(nameof(volume), "volume cannot be more than 100");
////        }

////        Volume = volume;
////    }

////    public void SetStation(string station)
////    {
////        if (string.IsNullOrEmpty(station))
////        {
////            throw new ArgumentNullException(nameof(station), "you cannot tune to an empty station");
////        }

////        Station = station;
////    }
////}


//// Demo on sealed class
////using CSharp.Demos.Console;

////SealedClassDemo sealedClassDemo = new SealedClassDemo();
////sealedClassDemo.Name = "Potti";
////Console.WriteLine($"Get name from sealed class method: \"{sealedClassDemo.GetName(sealedClassDemo.Name)}\"");
////Console.ReadLine();


////// Stack demo
////void DrawLine(int x, int y, int w, int h)
////{
////    // Stackoverflow exception
////    DrawLine(x, y, w, h);

////}

////void DrawSquare(int x, int y, int w, int h)
////{
////    int xw = x + w;
////    int yh = y + h;

////    DrawLine(x, y, xw, h);
////    DrawLine(xw, y, xw, yh);
////    DrawLine(xw, yh, w, yh);
////    DrawLine(x, yh, w, h);
////}

////Console.WriteLine("Drawing a square");
////DrawSquare(100,100,50,50);
////Console.WriteLine("End");


////// Heap Sample

////void DrawPolygon(Line[] lines)
////{

////}

////void DrawSquare(int x, int y, int w, int h)
////{
////    int xw = x + w;
////    int yh = y + h;

////    Line[] lines = new Line[4];
////    lines[0] = new Line(x, y, xw, h);
////    lines[1] = new Line(xw, y, xw, yh);
////    lines[2] = new Line(xw, yh, xw, yh);
////    lines[3] = new Line(x, yh, xw, h);

////    DrawPolygon(lines);
////}

////Console.WriteLine("Drawing a square");
////DrawSquare(100, 100, 50, 50);
////Console.WriteLine("End");


////class Line
////{
////    int X1;
////    int X2;
////    int Y1;
////    int Y2;

////    public Line(int x1, int x2, int y1, int y2)
////    {
////        X1 = x1;
////        Y1 = y1;
////        X2 = x2;
////        Y2 = y2;
////    }
////}
////IMathNew obj = new Math();
////Console.WriteLine($"Output value is {obj.MathOperation(1, 2)}");

////IMathNew objNew = new Math();
////Console.WriteLine($"Output value is {objNew.MathOperation(2, 2)}");

////public interface IMath
////{
////    int MathOperation(int a, int b);
////}

////public interface IMathNew
////{
////    int MathOperation(int a, int b);
////}

//////// WOrking solution
//////class Math : IMath
//////{
//////    public int MathOperation(int a, int b)
//////    {
//////        return a + b;
//////    }
//////}

//////// WOrking without any issue
//////class Math : IMath, IMathNew
//////{
//////    public int MathOperation(int a, int b)
//////    {
//////        return a + b;
//////    }
//////}

//////// Working but not expected
//////class Math : IMath, IMathNew
//////{
//////    public int MathOperation(int a, int b)
//////    {
//////        return a + b;
//////    }

//////    int IMathNew.MathOperation(int a, int b)
//////    {
//////        return a * b;
//////    }
//////}

////// Working but not expected
////public class Math : IMath, IMathNew
////{
////    int IMath.MathOperation(int a, int b)
////    {
////        return a + b;
////    }

////    int IMathNew.MathOperation(int a, int b)
////    {
////        return a * b;
////    }
////}


//string name = null;

//#region Error
//// Convert.ToString(name);

////name.ToString(); // Throws NullReferenceException
//#endregion

//if (name is not null)
//{
//    name = name.ToString();
//}



//using System.Net;

//var webClient = new WebClient();
//var webHtml = webClient.DownloadString("http://techinuthan.blogspot.in");

//using (var streamWriter = new StreamWriter(@"D:\\temp\blog.html"))
//{
//    streamWriter.Write(webHtml);
//}

//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

//string originalJson = "{\"vid\":\"1\", \"vname\":{\"Name\":\"nani\"}}";
//JObject jObject = JObject.Parse(originalJson);

//string vid = jObject["vid"].ToString();
//string name = jObject["vname"]["Name"].ToString();

//JObject newJObject = new JObject();
//newJObject.Add(new JProperty(vid, new JObject(
//    new JProperty("vid", vid),
//    new JProperty("name", name)
//)));

//string newJson = newJObject.ToString(Formatting.None);
//Console.WriteLine(newJson);

////var customResponse = JsonSerializer.Deserialize<Class3>(originalJson);
////Console.WriteLine(customResponse.ToString());


//Console.ReadLine();

//public class Class1
//{
//    public string vid { get; set; }
//    public Class2 class2 { get; set; }
//}

//public class Class2
//{
//    public string Name { get; set; }
//}

//public class Class3
//{

//    // public string vid { get; set; }
//    // public Class2 class2 { get; set; }
//    public Dictionary<string, Class2> obj { get; set; }
//}


var employees = new List<Employee>()
{
    new Employee(){ Id = 1, Name = "Nani", Age = 1 },
    new Employee(){ Id = 1, Name = "Potti", Age = 12 },
};

var empData = from emp in employees select new Employee { Id = emp.Id, Name = "Test " + emp.Name, Age = emp.Age };

foreach(var emp in empData)
{
    Console.WriteLine("Employee Name: " + emp.Name);
}
Console.ReadLine();

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public int Age { get; set; }
}
