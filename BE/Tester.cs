using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    [Serializable]
    public class Tester
    {
        #region variables
        string tester_Id;
        string last_Name;
        string first_Name;
        DateTime dateOfBitrth = new DateTime();
        string tell_Number;
        Address adress_Of_The_Tester = new Address();
        int years_of_expirience;
        int max_of_weekly_tests;//the maximum tests that the tester willing to do in a week
        int[] current_weekly_tests_num = new int[52];//array of number of weeks in a year to count the weekly test number
        bool[,] days_of_work = new bool[5, 7];//boolian matrice for work hours;
        Enum_Type_Of_Car type_Of_Car;
        Enum_Type_Of_GearBox type_Of_GearBox;
        Enum_Gender gender;
        double max_distance_for_test;//the maximum distance that the tester can make the test
        #endregion

        #region properties
        public string Tester_Id { get => tester_Id; set => tester_Id = value; }
        public string Last_Name { get => last_Name; set => last_Name = value; }
        public string First_Name { get => first_Name; set => first_Name = value; }
        public DateTime DateOfBitrth { get => dateOfBitrth; set => dateOfBitrth = value; }
        public string Tell_Number { get => tell_Number; set => tell_Number = value; }
        public int Years_of_expirience { get => years_of_expirience; set => years_of_expirience = value; }
        public Address Adress_Of_The_Tester { get => adress_Of_The_Tester; set => adress_Of_The_Tester = value; }
        public int Max_of_weekly_tests { get => max_of_weekly_tests; set => max_of_weekly_tests = value; }
        public Enum_Type_Of_Car Type_Of_Car { get => type_Of_Car; set => type_Of_Car = value; }
        public Enum_Gender Gender { get => gender; set => gender = value; }
        public double Max_distance_for_test { get => max_distance_for_test; set => max_distance_for_test = value; }
        public Enum_Type_Of_GearBox Type_Of_GearBox { get => type_Of_GearBox; set => type_Of_GearBox = value; }
        public int[] Current_weekly_tests_num { get => current_weekly_tests_num; set => current_weekly_tests_num = value; }

        [XmlIgnore]
        public bool[,] Days_of_work { get => days_of_work; set => days_of_work = value; }

        #endregion

        public override string ToString()
        {
            string str = "תעודת זהות: " + Tester_Id + "\nשם משפחה: " + Last_Name +
                 "\nשם פרטי: " + First_Name + "\nתאריך לידה: " + DateOfBitrth + "\nמין: " + Gender
                 + "\nמספר טלפון: " + Tell_Number + "\nכתובת: " + Adress_Of_The_Tester
                 + "\nמספר שנות ניסיון: " + Years_of_expirience + "\nמספר מקסימלי של טסטים בשבוע: " + Max_of_weekly_tests
                 + "\nהתמחות: " + Type_Of_Car + "\nמרחק מסימלי לטסט: " + Max_distance_for_test;

            return str;

        }

        public Tester() { }

      
        //copy constactor

        public Tester(Tester t)
        {
            Tester_Id = t.Tester_Id;
            Last_Name = t.Last_Name;
            First_Name = t.First_Name;
            DateOfBitrth = t.dateOfBitrth;
            Tell_Number = t.tell_Number;
            Years_of_expirience = t.years_of_expirience;
            Adress_Of_The_Tester = t.adress_Of_The_Tester;
            Max_of_weekly_tests = t.max_of_weekly_tests;
            Type_Of_Car = t.type_Of_Car;
            Type_Of_GearBox = t.type_Of_GearBox;
            Gender = t.gender;
            Max_distance_for_test = t.max_distance_for_test;
            Days_of_work = t.days_of_work;

        }
    }

}
