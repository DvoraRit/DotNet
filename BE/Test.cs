using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{

    
    public class Test
    {
        public static int counter = 1;

        #region variables
        int num_Of_Test;
        string trainee_Id;
        string tester_Id;//הטסטר יבחר באופן רנדומלי כדי למנוע הטיות - מבין הטסטרים הפנויים בתאריך
        DateTime date_Of_Test = new DateTime();
        int time_Of_Test;
        Address address_Of_StartOfTest;
        Enum_Success result_Of_Test = Enum_Success.NoData;
        Enum_Type_Of_Car type_Of_Car;
        Enum_Type_Of_GearBox type_Of_GearBox;
        CriteriaOfTheTest criteriaOfTheTest;
         //Enum_Success criteriaOfTheTest_tester_break;
         //Enum_Success criteriaOfTheTest_tester_touch_weel;
         //Enum_Success criteriaOfTheTest_saved_distance;
         //Enum_Success criteriaOfTheTest_reverse_parking;
         //Enum_Success criteriaOfTheTest_mirrors_checked;
         //Enum_Success criteriaOfTheTest_signaled;
         //Enum_Success criteriaOfTheTest_gave_priority;
         //Enum_Success criteriaOfTheTest_compliance_traffic_signs;
         //Enum_Success criteriaOfTheTest_speed;
        string tester_notes = "";
        #endregion

        #region properties
        public Address Address_Of_StartOfTest { get => address_Of_StartOfTest; set => address_Of_StartOfTest = value; }
        public string Trainee_Id { get => trainee_Id; set => trainee_Id = value; }
        public string Tester_Id { get => tester_Id; set => tester_Id = value; }
        public CriteriaOfTheTest CriteriaOfTheTest { get => criteriaOfTheTest; set => criteriaOfTheTest = value; }
        public string Tester_notes { get => tester_notes; set => tester_notes = value; }
       
        public Enum_Success Result_Of_Test { get => result_Of_Test; set => result_Of_Test = value; }
        public Enum_Type_Of_Car Type_Of_Car { get => type_Of_Car; set => type_Of_Car = value; }
        public Enum_Type_Of_GearBox Type_Of_GearBox { get => type_Of_GearBox; set => type_Of_GearBox = value; }
        public int Num_Of_Test { get => num_Of_Test; set => num_Of_Test = value; }
        public DateTime Date_Of_Test { get => date_Of_Test; set => date_Of_Test = value; }
        public int Time_Of_Test { get => time_Of_Test; set => time_Of_Test = value; }

        #endregion

        public override string ToString()
        {
            string str = "מספר מבחן: " + Num_Of_Test + "\nתעודת זהות של התלמיד: " + Trainee_Id + "\nתעודת זהות של הבוחן: " + Tester_Id +
                "\nתאריך המבחן: " + date_Of_Test + "\nסוג הרכב עליו בוצע המבחן:" + Type_Of_Car
                + "\nסוג תיבת הילוכים" + Type_Of_GearBox + "\nכתובת תחילת המבחן " + Address_Of_StartOfTest + "\nתוצאת המבחן " + Result_Of_Test
                + "\nהערות של הבוחן: " + Tester_notes;

            return str;
        }


    }


}
