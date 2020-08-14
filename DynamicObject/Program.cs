using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text;

namespace DynamicObject
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = Temp.CreateDynamicObject();
        }
    }

    class Temp
    {

        class Random
        {
            [Needed] public int Prop1 { get; set; }

            public double Prop2 { get; set; }

            public string Prop3 { get; set; }

            [Needed] public double Prop4 { get; set; }

        }

        public class NeededAttribute : Attribute
        {
        }

        public static object CreateDynamicObject()
        {
            Random random = new Random();
            IDictionary<string, object> dynamicObject = new ExpandoObject();
            var neededAttributeInstance = Activator.CreateInstance(random.GetType());
            foreach (var propertyInfo in random.GetType().GetProperties())
            {
                var neededAttribute = propertyInfo.GetCustomAttribute<NeededAttribute>();
                if (neededAttribute != null)
                {
                    string propertyName = propertyInfo.Name;
                    dynamicObject[propertyName] = propertyInfo.GetValue(neededAttributeInstance);
                }
            }
            return dynamicObject;
        }

    }

}
