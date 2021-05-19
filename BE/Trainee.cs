using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Trainee
    {
        #region variebels
        string trainee_Id;
        string last_Name;
        string first_Name;
        DateTime dateOfBitrth;
        string tell_Number;
        public Address address = new Address();
        Enum_Type_Of_Car type_Of_Car;
        Enum_Type_Of_GearBox type_Of_GearBox;
        Enum_Gender gender;
        string school_Name;
        string teachers_Name;
        int num_Of_Driving_Classes;
        #endregion

        #region properties
        public string Trainee_Id { get => trainee_Id; set => trainee_Id = value; }
        public string Last_Name { get => last_Name; set => last_Name = value; }
        public string First_Name { get => first_Name; set => first_Name = value; }
        public DateTime DateOfBitrth { get => dateOfBitrth; set => dateOfBitrth = value; }
        public string Tell_Number { get => tell_Number; set => tell_Number = value; }
        public Enum_Type_Of_Car Type_Of_Car { get => type_Of_Car; set => type_Of_Car = value; }
        public Enum_Type_Of_GearBox Type_Of_GearBox { get => type_Of_GearBox; set => type_Of_GearBox = value; }
        public Enum_Gender Gender { get => gender; set => gender = value; }
        public string School_Name { get => school_Name; set => school_Name = value; }
        public string Teachers_Name { get => teachers_Name; set => teachers_Name = value; }
        public int Num_Of_Driving_Classes { get => num_Of_Driving_Classes; set => num_Of_Driving_Classes = value; }
        public Address Address { get => address; set => address = value; }
        #endregion

       
        public override string ToString()
        {
            string str = "תעודת זהות: " + Trainee_Id + "\nשם משפחה: " + Last_Name + "\nשם פרטי: " + First_Name
                + "\nתאריך לידה: " + DateOfBitrth + "\nמין: " + Gender + "\nמספר טלפון: " + Tell_Number +
                "\nכתובת: " + Address + "\nלימוד על רכב: " + Type_Of_Car
                + "\nסוג תיבת הילוכים: " + Type_Of_GearBox + "\nשם בית הספר: "
                + School_Name + "\nשם מורה: " + Teachers_Name + "\nמספר שיעורי נהיגה: " +
                Num_Of_Driving_Classes;
            return str;
        }

    }//end class trainee

  
}
