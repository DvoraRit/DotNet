using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ComponentModel;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using BE;

namespace DAL
{
    public class DAL_XML : IDAL
    {
        XElement TraineeRoot;
        string TraineePath = "..//..//..//XML//TraineesXML.xml";

        XElement TestersRoot;
        string TestersPath = "..//..//..//XML//TestersXML.xml";

        XElement TestsRoot;
        string TestsPath = "..//..//..//XML//TestsXML.xml";

        XElement Counter;
        string CounterPath = "..//..//..//XML//counter.xml";

        public DAL_XML()//constractor
        {
            if (!File.Exists(TestsPath))
            {
                TestsRoot = new XElement("Tests");
                TestsRoot.Save(TestsPath);
            }

            if(!File.Exists(TraineePath))
            {
                TraineeRoot = new XElement("Trainees");
                TraineeRoot.Save(TraineePath);
            }

            if (!File.Exists(TestersPath))
            {
                TestersRoot = new XElement("Testers");
                TestersRoot.Save(TestersPath);
            }

            else//exist
                LoadData();
        }

        private void CreateFile()
        {
            TraineeRoot = new XElement("Trainees");
            TraineeRoot.Save(TraineePath);

            TestersRoot = new XElement("Testers");
            TestersRoot.Save(TestersPath);

            TestsRoot = new XElement("Tests");
            TestsRoot.Save(TestsPath);

            Counter = new XElement("Counter");//for code
            Counter.Save(CounterPath);
        }

        private void LoadData()
        {
            TraineeRoot = XElement.Load(TraineePath);
            TestersRoot = XElement.Load(TestersPath);
            TestsRoot = XElement.Load(TestsPath);
            //Counter = XElement.Load(CounterPath);
        }


        #region Trainees func

        public void AddTrainee(Trainee t)
        {
            TraineeRoot = XElement.Load(TraineePath);//load Trainees
            //check if the trainee does not exist
            if (FindTrainee(t.Trainee_Id) != null)
                throw new Exception("קיים כבר תלמיד עם תעודת זהות זהה");
            else
            {
                XElement id = new XElement("trainee_id", t.Trainee_Id);
                XElement lastName = new XElement("last_name", t.Last_Name);
                XElement firstName = new XElement("first_name", t.First_Name);
                XElement DateOfBitrth = new XElement("dateOfBirth", t.DateOfBitrth.ToString());
                XElement tellNum = new XElement("tell_num", t.Tell_Number);
                XElement schoolName = new XElement("school_name", t.School_Name);
                XElement typeOfCar = new XElement("Type_of_car", t.Type_Of_Car.ToString());
                XElement teacherName = new XElement("teacher_name", t.Teachers_Name);
                XElement numOfDrivingClasses = new XElement("num_of_drivig_classes", t.Num_Of_Driving_Classes.ToString());
                XElement Gender = new XElement("Gender", t.Gender.ToString());
                XElement typeOfGearBox = new XElement("Type_of_gearBox", t.Type_Of_GearBox.ToString());
                //Address
                XElement street = new XElement("street", t.Address.street);
                XElement houseNum = new XElement("house_num", t.Address.houseNum);
                XElement city = new XElement("city", t.Address.city.ToString());
                XElement address = new XElement("address", city, street, houseNum);

                //creating a new Trainee
                XElement newTrainee = new XElement("Trainee", id, lastName, firstName, DateOfBitrth,
                    tellNum, schoolName, typeOfCar, typeOfGearBox, teacherName, numOfDrivingClasses,
                    Gender, address);

                TraineeRoot.Add(newTrainee);
                TraineeRoot.Save(TraineePath);
            }
        }

        public void RemoveTrainee(Trainee t)
        {
            //load Trainee data
            TraineeRoot = XElement.Load(TraineePath);

            XElement traineeToRemove;
            //find the trainee to remove
            traineeToRemove = (from traineeElement in TraineeRoot.Elements()
                               where traineeElement.Element("trainee_id").Value == t.Trainee_Id
                               select traineeElement).FirstOrDefault();

            //check that this trainee realy exist
            if (traineeToRemove == null)
                throw new Exception("התלמיד למחיקה לא נמצא");
            else
            {
                traineeToRemove.Remove();
                TraineeRoot.Save(TraineePath);
            }

        }

        public Trainee convertFromXmlToTrainee(XElement traineeElement)
        {
            if (traineeElement == null)
                return null;
            Trainee trainee = new Trainee();
            Address addressT = new Address();
            addressT.city = traineeElement.Element("address").Element("city").Value;
            addressT.houseNum = int.Parse(traineeElement.Element("address").Element("house_num").Value);
            addressT.street = traineeElement.Element("address").Element("street").Value;
            trainee.Address = addressT;
            trainee.Trainee_Id = traineeElement.Element("trainee_id").Value;
            trainee.Last_Name = traineeElement.Element("last_name").Value;
            trainee.First_Name = traineeElement.Element("first_name").Value;
            trainee.DateOfBitrth = DateTime.Parse(traineeElement.Element("dateOfBirth").Value);
            trainee.Tell_Number = traineeElement.Element("tell_num").Value;
            trainee.Type_Of_Car = ParseEnum<Enum_Type_Of_Car>(traineeElement.Element("Type_of_car").Value);
            trainee.School_Name = traineeElement.Element("school_name").Value;
            trainee.Teachers_Name = traineeElement.Element("teacher_name").Value;
            trainee.Num_Of_Driving_Classes = int.Parse(traineeElement.Element("num_of_drivig_classes").Value);
            trainee.Gender = ParseEnum<Enum_Gender>(traineeElement.Element("Gender").Value);
            trainee.Type_Of_GearBox = ParseEnum<Enum_Type_Of_GearBox>(traineeElement.Element("Type_of_gearBox").Value);

            return trainee;
        }

        public Trainee FindTrainee(string idOfTrainee)
        {
            //load Trainee data
            TraineeRoot = XElement.Load(TraineePath);
            
            XElement traineeFoundElement;
            traineeFoundElement = (from t in TraineeRoot.Elements()
                            where t.Element("trainee_id").Value == idOfTrainee
                            select t).FirstOrDefault();
            return convertFromXmlToTrainee(traineeFoundElement);
        }

        public void UpdateTrainee(Trainee t)
        {
            //load Trainee data
            TraineeRoot = XElement.Load(TraineePath);
            XElement traineeToUpdate;
            //find the trainee to update
            traineeToUpdate = (from traineeElement in TraineeRoot.Elements()
                               where traineeElement.Element("trainee_id").Value == t.Trainee_Id
                               select traineeElement).FirstOrDefault();

            if (traineeToUpdate == null)//means that the trainee does not exist
                throw new Exception("לא קיים תלמיד עם תעודת זהות זו");
            else
            {
                //updating the trainee values
                traineeToUpdate.Element("last_name").Value = t.Last_Name;
                traineeToUpdate.Element("first_name").Value = t.First_Name;
                traineeToUpdate.Element("dateOfBirth").Value = t.DateOfBitrth.ToString();
                traineeToUpdate.Element("tell_num").Value = t.Tell_Number;
                traineeToUpdate.Element("school_name").Value = t.School_Name;
                traineeToUpdate.Element("teacher_name").Value = t.Teachers_Name;
                traineeToUpdate.Element("num_of_drivig_classes").Value = t.Num_Of_Driving_Classes.ToString();
                traineeToUpdate.Element("Type_of_car").Value = t.Type_Of_Car.ToString();
                traineeToUpdate.Element("Type_of_gearBox").Value = t.Type_Of_GearBox.ToString();
                traineeToUpdate.Element("Gender").Value = t.Gender.ToString();

                XElement street = new XElement("street", t.Address.street);
                XElement houseNum = new XElement("house_num", t.Address.houseNum);
                XElement city = new XElement("city", t.Address.city);
                XElement address = new XElement("address", city, street, houseNum);
                traineeToUpdate.Element("address").ReplaceWith(address);
                //save
                TraineeRoot.Save(TraineePath);
            }
        }

        public IEnumerable<Trainee> GetTrainees(Func<Trainee, bool> func = null)
        {
            //load Trainee data
            TraineeRoot = XElement.Load(TraineePath);
            

            IEnumerable<Trainee> trainees = from t in TraineeRoot.Elements()
                                            select convertFromXmlToTrainee(t);
            if (func == null)
                return trainees;
            return trainees.Where(func);
        }

        #endregion

        #region Testers Funcs

        XElement ConvertTesterToXML(Tester t)
        {
            if (t == null)
                return null;
            XElement id = new XElement("Tester_Id", t.Tester_Id);
            XElement lastName = new XElement("Last_Name", t.Last_Name);
            XElement firstName = new XElement("First_Name", t.First_Name);
            XElement DateOfBitrth = new XElement("DateOfBirth", t.DateOfBitrth);
            XElement tellNum = new XElement("Tell_Number", t.Tell_Number);
            XElement typeOfCar = new XElement("Type_Of_Car", t.Type_Of_Car);
            XElement Gender = new XElement("Gender", t.Gender);
            XElement typeOfGearBox = new XElement("Type_Of_GearBox", t.Type_Of_GearBox);
            XElement years_of_expirience = new XElement("Years_Of_Expirience", t.Years_of_expirience);
            XElement max_of_weekly_tests = new XElement("Max_of_weekly_tests", t.Max_of_weekly_tests);
            XElement max_distance_for_test = new XElement("Max_distance_for_test", t.Max_distance_for_test);
            //Address
            XElement street = new XElement("street", t.Adress_Of_The_Tester.street);
            XElement houseNum = new XElement("house_num", t.Adress_Of_The_Tester.houseNum);
            XElement city = new XElement("city", t.Adress_Of_The_Tester.city);
            XElement address = new XElement("Address_Of_The_Tester", city, street, houseNum);

            //creating a tester in xml
            XElement tester = new XElement("Tester", id, lastName, firstName, DateOfBitrth,
                tellNum, Gender, typeOfCar, typeOfGearBox, years_of_expirience, max_distance_for_test, max_of_weekly_tests, address);

            //convert the current num of test in every week array to string
            string strNumTestsInEveryWeek = "";
            int sizeOfArray = t.Current_weekly_tests_num.Length;//size of the array - Usually 52
            strNumTestsInEveryWeek += " " + sizeOfArray;
            for (int i = 0; i < sizeOfArray; i++)
                strNumTestsInEveryWeek += "," + t.Current_weekly_tests_num[i];

            tester.Add(new XElement("Num_Of_Tests_In_Every_Week", strNumTestsInEveryWeek));

            //convert the working hours of the tester to string
            string strWorkingHours = "";
            int sizeOfDaysOfWork = t.Days_of_work.GetLength(0);//save the length of first dimension size - the days of work = 5
            int sizeOfHours = t.Days_of_work.GetLength(1);//save the length of second dimension size - the hours of work = 7
            strWorkingHours += sizeOfDaysOfWork + "," + sizeOfHours;
            for (int i = 0; i < sizeOfDaysOfWork; i++)
                for (int j = 0; j < sizeOfHours; j++)
                    strWorkingHours += "," + t.Days_of_work[i, j];

            tester.Add(new XElement("Days_Of_Work", strWorkingHours));
            return tester;

        }

        Tester ConvertXmlToTester(XElement testerElement)
        {
            if (testerElement == null)
            {
                return null;
            }
            Tester tester = new Tester();
            tester.Tester_Id = testerElement.Element("Tester_Id").Value;
            tester.Last_Name = testerElement.Element("Last_Name").Value;
            tester.First_Name = testerElement.Element("First_Name").Value;
            tester.DateOfBitrth = DateTime.Parse(testerElement.Element("DateOfBirth").Value);
            tester.Tell_Number = testerElement.Element("Tell_Number").Value;
            tester.Type_Of_Car = ParseEnum<Enum_Type_Of_Car>(testerElement.Element("Type_Of_Car").Value);
            tester.Type_Of_GearBox = ParseEnum<Enum_Type_Of_GearBox>(testerElement.Element("Type_Of_GearBox").Value);
            tester.Years_of_expirience = int.Parse(testerElement.Element("Years_Of_Expirience").Value);
            tester.Max_of_weekly_tests = int.Parse(testerElement.Element("Max_of_weekly_tests").Value);
            tester.Gender = ParseEnum<Enum_Gender>(testerElement.Element("Gender").Value);
            tester.Max_distance_for_test = int.Parse(testerElement.Element("Max_distance_for_test").Value);
            //address
            Address address = new Address();
            address.city = testerElement.Element("Address_Of_The_Tester").Element("city").Value;
            address.houseNum = int.Parse(testerElement.Element("Address_Of_The_Tester").Element("house_num").Value);
            address.street = testerElement.Element("Address_Of_The_Tester").Element("street").Value;
            tester.Adress_Of_The_Tester = address;

            //convert the current num of test in every week string to array
            string strCurrentNumOfTest = testerElement.Element("Num_Of_Tests_In_Every_Week").Value;
            string[] arrCurrentNumOfTest = strCurrentNumOfTest.Split(',');//convert the string to array of numbers
            int size = int.Parse(arrCurrentNumOfTest[0]);

            for (int i = 0; i < size; i++)
            {
                //because the arrCurrentNumOfTest[0] is the size we will copy from i+1
                tester.Current_weekly_tests_num[i] = int.Parse(arrCurrentNumOfTest[i + 1]);
            }

            //convert the working hours of the tester to string
            string strWorkingHours = testerElement.Element("Days_Of_Work").Value;
            string[] arrWorkingHours = strWorkingHours.Split(',');
            int sizeOfDaysOfWork = int.Parse(arrWorkingHours[0]);
            int sizeOfHours = int.Parse(arrWorkingHours[1]);
            int index = 2;
            for (int i = 0; i < sizeOfDaysOfWork; i++)
                for (int j = 0; j < sizeOfHours; j++)
                {
                    tester.Days_of_work[i, j] = Convert.ToBoolean(arrWorkingHours[index]);
                    index++;
                }

            return tester;

        }

        public void AddTester(Tester t)
        {
            TestersRoot = XElement.Load(TestersPath);

            //check if the tester is already exist
            if (FindTester(t.Tester_Id) != null)
                throw new Exception("קיים כבר בוחן עם תעודת זהות זהה");
            else
            {
                XElement newTester = ConvertTesterToXML(t);
                TestersRoot.Add(newTester);
                TestersRoot.Save(TestersPath);
            }

        }

        public Tester FindTester(string id)
        {
            TestersRoot = XElement.Load(TestersPath);

            XElement testerElement = (from ts in TestersRoot.Elements()
                                      where ts.Element("Tester_Id").Value == id
                                      select ts).FirstOrDefault();
            return (ConvertXmlToTester(testerElement));


        }

        public void RemoveTester(Tester t)
        {
            TestersRoot = XElement.Load(TestersPath);

            XElement testerToRemove;
            testerToRemove = (from ts in TestersRoot.Elements()
                              where ts.Element("Tester_Id").Value == t.Tester_Id
                              select ts).FirstOrDefault();
            //check that this tester really exist
            if (testerToRemove == null)
                throw new Exception("לא קיים בוחן עם תעודת זהות זו");

            testerToRemove.Remove();
            TestersRoot.Save(TestersPath);
        }

        public void UpdateTester(Tester newTester)
        {
            if (newTester == null)
                throw new Exception("הבוחן לא נמצא");

            TestersRoot = XElement.Load(TestersPath);

            XElement oldTester = ((from t in TestersRoot.Elements()
                                   where (t.Element("Tester_Id").Value == newTester.Tester_Id)
                                   select t).FirstOrDefault());

            XElement xElementNewTester = ConvertTesterToXML(newTester);

            //check that this tester really exist
            if (oldTester == null)
                throw new Exception("לא קיים בוחן עם תעודת זהות זו");

            oldTester.ReplaceWith(xElementNewTester);
            TestersRoot.Save(TestersPath);
        }

        public IEnumerable<Tester> GetTesters(Func<Tester, bool> func = null)
        {
            List<Tester> testers;

            TestersRoot = XElement.Load(TestersPath);
            testers = (from ts in TestersRoot.Elements()
                       select ConvertXmlToTester(ts)).ToList();


            if (testers.Count == 0)//means that the list is empty
                return null;

            if (func == null)//means no predicate - return all the list of test 
                return testers.AsEnumerable();
            return testers.Where(func).AsEnumerable();
        }
        #endregion

        #region Tests Func
        Test ConverXmlToTest(XElement xelementTest)
        {
            TestsRoot = XElement.Load(TestsPath);
            if (xelementTest == null)
                return null;
            Test test = new Test();
            test.Num_Of_Test = int.Parse(xelementTest.Element("Num_Of_Test").Value);
            test.Tester_Id = xelementTest.Element("Tester_Id").Value;
            test.Trainee_Id = xelementTest.Element("Trainee_Id").Value;
            test.Date_Of_Test = DateTime.Parse(xelementTest.Element("Date_Of_Test").Value);
            test.Result_Of_Test = ParseEnum<Enum_Success>(xelementTest.Element("Result_Of_Test").Value);
            test.Tester_notes = xelementTest.Element("Tester_notes").Value;
            test.Time_Of_Test = int.Parse(xelementTest.Element("Time_Of_Test").Value);
            test.Type_Of_Car = ParseEnum<Enum_Type_Of_Car>(xelementTest.Element("Enum_Type_Of_Car").Value);
            test.Type_Of_GearBox = ParseEnum<Enum_Type_Of_GearBox>(xelementTest.Element("Enum_Type_Of_GearBox").Value);

            CriteriaOfTheTest criteriaOfTheTest = new CriteriaOfTheTest();
            criteriaOfTheTest.compliance_traffic_signs = ParseEnum<Enum_Success>(xelementTest.Element("compliance_traffic_signs").Value);
            criteriaOfTheTest.gave_priority = ParseEnum<Enum_Success>(xelementTest.Element("gave_priority").Value);
            criteriaOfTheTest.mirrors_checked = ParseEnum<Enum_Success>(xelementTest.Element("mirrors_checked").Value);
            criteriaOfTheTest.reverse_parking = ParseEnum<Enum_Success>(xelementTest.Element("reverse_parking").Value);
            criteriaOfTheTest.saved_distance = ParseEnum<Enum_Success>(xelementTest.Element("saved_distance").Value);
            criteriaOfTheTest.signaled = ParseEnum<Enum_Success>(xelementTest.Element("signaled").Value);
            criteriaOfTheTest.speed = ParseEnum<Enum_Success>(xelementTest.Element("speed").Value);
            criteriaOfTheTest.tester_break = ParseEnum<Enum_Success>(xelementTest.Element("tester_break").Value);
            criteriaOfTheTest.tester_touch_weel = ParseEnum<Enum_Success>(xelementTest.Element("tester_touch_weel").Value);
            test.CriteriaOfTheTest = criteriaOfTheTest;

            Address address = new Address();
            address.city = xelementTest.Element("address").Element("city").Value;
            address.street = xelementTest.Element("address").Element("street").Value;
            address.houseNum = int.Parse(xelementTest.Element("address").Element("house_num").Value);
            test.Address_Of_StartOfTest = address;

            return test;


        }

        XElement convertTestToXml(Test test)
        {
            if (test == null)
                return null;
            XElement Num_Of_Test = new XElement("Num_Of_Test", test.Num_Of_Test.ToString());
            XElement Tester_Id = new XElement("Tester_Id", test.Tester_Id);
            XElement Trainee_Id = new XElement("Trainee_Id", test.Trainee_Id);
            XElement Date_Of_Test = new XElement("Date_Of_Test", test.Date_Of_Test.ToString());
            XElement Result_Of_Test = new XElement("Result_Of_Test", test.Result_Of_Test.ToString());
            XElement Tester_notes = new XElement("Tester_notes", test.Tester_notes);
            XElement Time_Of_Test = new XElement("Time_Of_Test", test.Time_Of_Test.ToString());
            XElement Enum_Type_Of_Car = new XElement("Enum_Type_Of_Car", test.Type_Of_Car.ToString());
            XElement Enum_Type_Of_GearBox = new XElement("Enum_Type_Of_GearBox", test.Type_Of_GearBox.ToString());

            //criteria
            XElement compliance_traffic_signs = new XElement("compliance_traffic_signs", test.CriteriaOfTheTest.compliance_traffic_signs);
            XElement gave_priority = new XElement("gave_priority", test.CriteriaOfTheTest.gave_priority);
            XElement mirrors_checked = new XElement("mirrors_checked", test.CriteriaOfTheTest.mirrors_checked);
            XElement reverse_parking = new XElement("reverse_parking", test.CriteriaOfTheTest.reverse_parking);
            XElement saved_distance = new XElement("saved_distance", test.CriteriaOfTheTest.saved_distance);
            XElement signaled = new XElement("signaled", test.CriteriaOfTheTest.signaled);
            XElement speed = new XElement("speed", test.CriteriaOfTheTest.speed);
            XElement tester_break = new XElement("tester_break", test.CriteriaOfTheTest.tester_break);
            XElement tester_touch_weel = new XElement("tester_touch_weel", test.CriteriaOfTheTest.tester_touch_weel);
            //address
            XElement street = new XElement("street", test.Address_Of_StartOfTest.street);
            XElement houseNum = new XElement("house_num", test.Address_Of_StartOfTest.houseNum);
            XElement city = new XElement("city", test.Address_Of_StartOfTest.city.ToString());
            XElement address = new XElement("address", city, street, houseNum);

            XElement xelementTest = new XElement("Test", Num_Of_Test, Tester_Id, Trainee_Id, Date_Of_Test, Result_Of_Test, Tester_notes, Time_Of_Test
                , Enum_Type_Of_Car, Enum_Type_Of_GearBox, compliance_traffic_signs, gave_priority, mirrors_checked, reverse_parking
                , saved_distance, signaled, speed, tester_break, tester_touch_weel, address);

            return xelementTest;
        }

        public void AddTest(Test t)
        {
            TestsRoot = XElement.Load(TestsPath);

            //we will save the curent counter in XML file for feuter help

            if (!File.Exists(CounterPath))
            {
                Counter = new XElement("Counter",Test.counter.ToString());//for code
               
                Counter.Save(CounterPath);
            }
            else
                Counter = XElement.Load(CounterPath);

            //configure the num of test
            
            t.Num_Of_Test = int.Parse(Counter.Value);
            Test.counter = int.Parse(Counter.Value);//updating the counter ot tests
            Test.counter++;
            XElement newNum = new XElement("Counter", Test.counter.ToString());
            Counter = newNum;
            Counter.Save(CounterPath);

            XElement elementTest = convertTestToXml(t);
            TestsRoot.Add(elementTest);
            TestsRoot.Save(TestsPath);
        }

        public Test FindTest(int numOfTest)
        {
            TestsRoot = XElement.Load(TestsPath);

            XElement testElement = (from ts in TestsRoot.Elements()
                                    where int.Parse(ts.Element("Num_Of_Test").Value) == numOfTest
                                    select ts).FirstOrDefault();
            return (ConverXmlToTest(testElement));
        }

        public void UpdateTest(Test newTest)
        {
            TestsRoot = XElement.Load(TestsPath);
            XElement newTestElement = convertTestToXml(newTest);

            XElement oldTest = ((from ts in TestsRoot.Elements()
                                   where (int.Parse(ts.Element("Num_Of_Test").Value) == newTest.Num_Of_Test)
                                   select ts).FirstOrDefault());

            //check that this tester really exist
            if (oldTest == null)
                throw new Exception("המבחן לא נמצא");

            oldTest.ReplaceWith(newTestElement);
            TestsRoot.Save(TestsPath);

        }

        public void RemoveTest(Test t)
        {
            TestsRoot = XElement.Load(TestsPath);
            XElement testToRemove;
            testToRemove = (from ts in TestsRoot.Elements()
                            where int.Parse(ts.Element("Num_Of_Test").Value) == t.Num_Of_Test
                            select ts).FirstOrDefault();

            testToRemove.Remove();
            TestsRoot.Save(TestsPath);
        }

        public IEnumerable<Test> GetTests(Func<Test, bool> func = null)
        {
            List<Test> testsList = new List<Test>();
            TestsRoot = XElement.Load(TestsPath);
            testsList = (from ts in TestsRoot.Elements()
                       select ConverXmlToTest(ts)).ToList();


            if (testsList.Count == 0)//means that the list is empty
                return null;

            if (func == null)//means no predicate - return all the list of test 
                return testsList.AsEnumerable();
            return testsList.Where(func).AsEnumerable();

        }
        #endregion

        /// <summary>
        /// this func convert string to Enum value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }
    }


}
