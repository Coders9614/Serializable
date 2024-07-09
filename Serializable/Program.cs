using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Serializable
{
    [Serializable]
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public double GPA { get; set; }
    }

    class Program
    {
        static void Main()
        {
            // Create a sample Student object
            var student = new Student
            {
                StudentId = 101,
                Name = "JK",
                GPA = 3.8
            };

            // Serialize to Binary format
            SerializeToBinary(student);

            // Serialize to XML format
            SerializeToXml(student);

            // Serialize to JSON format
            SerializeToJson(student);

            // Deserialize from Binary format
            var deserializedStudentBinary = DeserializeFromBinary();
            Console.WriteLine($"Deserialized (Binary): {deserializedStudentBinary.Name}, GPA: {deserializedStudentBinary.GPA}");

            // Deserialize from XML format
            var deserializedStudentXml = DeserializeFromXml();
            Console.WriteLine($"Deserialized (XML): {deserializedStudentXml.Name}, GPA: {deserializedStudentXml.GPA}");

            var deserializedStudentJson = DeserializeFromJson();
            Console.WriteLine($"Deserialized (JSON): {deserializedStudentJson.Name}, GPA: {deserializedStudentJson.GPA}");
            Console.ReadKey();
        }

        static void SerializeToBinary(Student student)
        {
            using (var stream = new FileStream("student.bin", FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, student);
            }
        }

        static Student DeserializeFromBinary()
        {
            using (var stream = new FileStream("student.bin", FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (Student)formatter.Deserialize(stream);
            }
        }

        static void SerializeToXml(Student student)
        {
            using (var writer = new StreamWriter("student.xml"))
            {
                var serializer = new XmlSerializer(typeof(Student));
                serializer.Serialize(writer, student);
            }
        }

        static Student DeserializeFromXml()
        {
            using (var reader = new StreamReader("student.xml"))
            {
                var serializer = new XmlSerializer(typeof(Student));
                return (Student)serializer.Deserialize(reader);
            }
        }

        static void SerializeToJson(Student student)
        {
            var json = JsonConvert.SerializeObject(student);
            File.WriteAllText("student.json", json);
        }

        static Student DeserializeFromJson()
        {
            var json = File.ReadAllText("student.json");
            return JsonConvert.DeserializeObject<Student>(json);
        }
    }
}
