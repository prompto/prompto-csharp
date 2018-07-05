using System;

namespace prompto.runtime.utils
{

    public class MyClass
    {
        static public bool booleanValue()
        {
            return true;
        }

        static public bool? booleanObject()
        {
            return true;
        }

        static public char characterValue()
        {
            return 'Z';
        }

        static public char? characterObject()
        {
            return 'Z';
        }

        static public int intValue()
        {
            return 123;
        }

        static public int? intObject()
        {
            return 123;
        }

        static public long longValue()
        {
            return 9876543210;
        }

        static public long? longObject()
        {
			return 9876543210;
        }

        static public float floatValue()
        {
            return 123;
        }

        static public float? floatObject()
        {
            return 123;
        }

        static public double doubleValue()
        {
            return 123;
        }

        static public double? doubleObject()
        {
            return 123;
        }

        String _id;
        String _name;

        public String id
        { 
            get 
            {
                return this._id; 
            } 
            set
            {
                this._id = value; 
                computeDisplay(); 
            } 
        }

        public String name
        { 
            get 
            {
                return this._name; 
            } 
            set
            {
                this._name = value; 
                computeDisplay(); 
            } 
        }

        public String display { get; set; }


        private void computeDisplay()
        {
            display = "/id=" + id + "/name=" + name;
        }


        public void printDisplay()
        {
            Console.Write(display);
        }

		public String getDisplay()
		{
			return display;
		}

    }

}