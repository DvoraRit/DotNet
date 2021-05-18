using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;
using DAL;

using System.IO;
using System.Net;
using System.Xml;

namespace BL

{
    public class BL_imp : IBL
    {
        DAL.IDAL dal = FactoryDAL.GetDAL();


        #region Trainees funcs

        /// <summary>
        ///הוספת נבחן למאגר הנבחנים במידה ועונה על כל הקריטריונים
        /// </summary>
        /// <param name="listOfTraineesToAdd">אוסף של נבחנים להוספה</param>
        public void AddTrainee(params Trainee[] listOfTraineesToAdd)
        { 
            foreach (var tr in listOfTraineesToAdd)
            {
                //case that newTraineeToAdd under 18 years
                if ((DateTime.Now - tr.DateOfBitrth).Days / 365 < Configuration.min_age_for_trainee)
                    throw new Exception("לא ניתן להוסיף תלמיד מתחת לגיל 18");
                //case that newTraineeToAdd didnt took enough lessons (under 20)
                if (tr.Num_Of_Driving_Classes < Configuration.min_lessons_to_be_tested)
                    throw new Exception("למבחן נהיגה ניתן לגשת רק אחרי 20 שיעורים");
                if (FindTrainee(tr.Trainee_Id) != null)
                    throw new Exception("קיים כבר תלמיד עם תעודת זהות זהה");

                //checking that the trainee didnt already passed this caind of test


                else dal.AddTrainee(tr);
            }
        }

        /// <summary>
        /// הפונקציה מחזירה תלמיד קיים על פי תעודת זהות
        /// </summary>
        /// <param name="idOfTrainee">תעודת הזהות של התלמיד לחיפוש</param>
        /// <returns></returns>
        public Trainee FindTrainee(string idOfTrainee)
        {
            return (dal.FindTrainee(idOfTrainee));
        }

        /// <summary>
        ///עדכון פרטי נבחן מסויים, 
        ///שלח לפונקציה נבחן מעודכן והפונקציה תחליף את הנבחן הישן בנבחן החדש המעודכן
        /// </summary>
        /// <param name="t">הנבחן אותו רוצים לעדכן</param>
        public void UpdateTrainee(Trainee t)
        {
            if (t == null)
                throw new Exception("התלמיד לא נמצא");
            dal.UpdateTrainee(t);
        }

        /// <summary>
        /// פונקציה זו מסירה נבחן ממאגר הנבחנים
        /// </summary>
        /// <param name="t">הנבחן אותו רוצים להסיר</param>
        public void RemoveTrainee(Trainee t)
        {
            dal.RemoveTrainee(t);
        }

        /// <summary>
        /// פונקציה המקבצת נבחנים על פי שם בית הספר לנהיגה בו למדו
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<string, Trainee>> GetTraineesBySchoolName()
        {
            var traineesByGear = from tr in GetTrainees()
                                 group tr by tr.School_Name into g
                                 select g;

            return traineesByGear;
        }

        /// <summary>
        /// פונקציה המקבצת נבחנים על פי שם המורה איתו למדו
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<string, Trainee>> GetTraineesByTeacherName()
        {
            var traineesByTeacher = from tr in GetTrainees()
                                    group tr by tr.Teachers_Name into g
                                    select g;

            return traineesByTeacher;
        }

        /// <summary>
       /// פונקציה המקבצת תלמידים על פי מספר מבחני נהיגה שעשו
       /// </summary>
       /// <returns></returns>
        public IEnumerable<IGrouping<int, Trainee>> GetTraineeByNumOfTest()
        {
            var TraineeByNumOfTest = from t in GetTrainees()
                                     group t by CountTraineeTest(t) into g
                                     select g;
            return TraineeByNumOfTest;
        }

        /// <summary>
        /// פונקציה המקבצת תלמידים לפי העיר בה הם גרים
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<string, Trainee>> GetTraineeByCity()
        {
            var traineesByCity = from tra in GetTrainees()
                                 group tra by tra.Address.city into g
                                 select g;

            return traineesByCity;
        }
       

        /// <summary>
        /// הפונקציה מחזירה אוסף של כל הנבחנים על פי פרדיקט מסויים, 
        /// אם לא יישלח פרדיקט הפונקציה תחזיר את כל הנבחנים ללא תנאי
        /// </summary>
        /// <param name="func">התנאי להחזרת התלמידים, יכול להיות ריק</param>
        /// <returns></returns>
        public IEnumerable<Trainee> GetTrainees(Func<Trainee, bool> func = null)
        {
            return (dal.GetTrainees(func));
        }


        /// <summary>
        /// פונקציה הבודקת אם התלמיד רשאי לקבל רישיון נהיגה 
        /// </summary>
        /// <param name="criteriaOfTheTest">רשימת קריטריונים לקבלת רישון</param>
        /// <returns></returns>
        public bool IsPassedThisTest(CriteriaOfTheTest criteriaOfTheTest)
        {
            if (criteriaOfTheTest.compliance_traffic_signs == Enum_Success.עבר
               && criteriaOfTheTest.gave_priority == Enum_Success.עבר
               && criteriaOfTheTest.mirrors_checked == Enum_Success.עבר
               && criteriaOfTheTest.reverse_parking == Enum_Success.עבר
               && criteriaOfTheTest.saved_distance == Enum_Success.עבר
               && criteriaOfTheTest.signaled == Enum_Success.עבר
               && criteriaOfTheTest.speed == Enum_Success.עבר
               && criteriaOfTheTest.tester_break == Enum_Success.עבר
               && criteriaOfTheTest.tester_touch_weel == Enum_Success.עבר)
                return true;
            return false;
        }

        /// <summary>
        /// הפונקציה בודקת האם תלמיד זה כבר עבר מבחן נהיגה מאותו הסוג
        /// </summary>
        /// <param name="tr">התלמיד לבדיקה</param>
        /// <returns></returns>
        private bool alreadyPassedThisKindOfTest(Trainee tr)
        {
            IEnumerable<Test> tm = GetTests(h => h.Trainee_Id == tr.Trainee_Id);//tm is IEnumerable of all tests done by trainee tr
            if (tm == null)//if there is no tests at all - of course he did not already passed
                return false;

            //t is IEnumerable of the passed test in tr type of car or null
            var t = tm.Where(h => h.Result_Of_Test == Enum_Success.עבר)
                .Where(carT => carT.Type_Of_Car == tr.Type_Of_Car)
                .Where(gearT => gearT.Type_Of_GearBox == tr.Type_Of_GearBox);


            if (t.Count() == 0)//there is no test that the trainee passed in this type of car & this type of gear
                return false;
            else return true;

        }

        /// <summary>
        /// פונקציה הסופרת את מספר מבחני הנהיגה שעשה התלמיד
        /// </summary>
        /// <param name="tr">התלמיד לבדיקה</param>
        /// <returns></returns>
        public int CountTraineeTest(Trainee tr)
        {
            int count = 0;
            foreach (Test var in dal.GetTests(null))// DataSource.TestsList
            {
                if (tr.Trainee_Id == var.Trainee_Id)
                    return count++;

            }
            return count;
        }
        #endregion

        #region Testers Funcs

        //this func add tester if he answer the conditions
        public void AddTester(Tester newTestersToAdd)
        {     // check if the testers age under 40 years
            if ((DateTime.Now - newTestersToAdd.DateOfBitrth).Days / 365 < Configuration.min_age_for_tester)
                throw new Exception("בוחן חייב להיות מעל גיל 40");

            if (dal.FindTester(newTestersToAdd.Tester_Id) != null)
                throw new Exception("קיים כבר בוחן עם תעוזת זהות זו");

            else dal.AddTester(newTestersToAdd);

        }


        //input: a DateTime including date+houre.
        //output: all the testers that available in this date 
        //"available" means - working + not testing already in that date
        public List<Tester> GetTestersAvailable(Test newTest)
        {
            DateTime date = newTest.Date_Of_Test;
            int indexOfDay;
            int indexOfHour;

            //check that the hour of the test is in working hours of the testers
 
            indexOfHour = date.Hour - 9;

            //check that the day of the test is in working days of the testers
            if ((int)date.DayOfWeek > 4)
                throw new Exception("ביום זה הבוחנים לא עובדים");
            else
                indexOfDay = (int)date.DayOfWeek;

            List<Tester> testersAvailable = GetTesters(ts => ts.Days_of_work[indexOfDay, indexOfHour]).ToList();
            //remove all testers that the distamce is too big for them
            testersAvailable = testersAvailable.FindAll(tester => GetDistance(tester.Adress_Of_The_Tester.ToString(), newTest.Address_Of_StartOfTest.ToString()) <= tester.Max_distance_for_test);
            //מחיקה של כל הבוחנים שלא בוחנים על אותו סוג רכב ואותו סוג הילוכים
            testersAvailable = testersAvailable.FindAll(tester => tester.Type_Of_Car == newTest.Type_Of_Car && tester.Type_Of_GearBox == newTest.Type_Of_GearBox);

            if (testersAvailable.Count() == 0)//means there is no tester available at this date
            {
                return null;

            }
            else
            {//check that the testers available doesnt testing another trainee
                IEnumerable<Test> testList = GetTests(aTest => aTest.Date_Of_Test == date);
                if (testList == null)
                    return testersAvailable.ToList();
                foreach (Test test in GetTests(aTest => aTest.Date_Of_Test == date))//test Contains all the tests conducted in date
                {   //remove from testersAvailable all the tester that already testing in that date               
                    if (testersAvailable.Contains(FindTester(test.Tester_Id)))
                        testersAvailable.ToList().Remove(FindTester(test.Tester_Id));

                }
                //remove from testersAvailable list all the testers that got to maximum test in week
                int index = NumOfWeekInYearForTest(date);
                foreach (Tester ts in testersAvailable)
                {
                    if (ts.Current_weekly_tests_num[index] >= ts.Max_of_weekly_tests)
                        testersAvailable.ToList().Remove(ts);

                }

                return testersAvailable.ToList();//return only the testers available in that date and does not testing another trainee

            }

        }

        //input:nun
        //output: grouping the testers by the type of car he specialize in
        public IEnumerable<IGrouping<Enum_Type_Of_Car, Tester>> GetTestersByTypeOfCar()
        {
            var testersByCar = from t in GetTesters()
                               group t by t.Type_Of_Car into g
                               select g;

            return testersByCar;
        }

        //input:nun
        //output: grouping the testers by the type of gear he specialize in
        public IEnumerable<IGrouping<Enum_Type_Of_GearBox, Tester>> GetTestersByTypeOfGear()
        {
            var testersByGear = from t in GetTesters()
                                group t by t.Type_Of_GearBox into g
                                select g;
            return testersByGear;
        }

        public IEnumerable<IGrouping<int, Tester>> GetTestersByYearOfEx()
        {
            var testersByYearOfEx = from t in GetTesters()
                                group t by t.Years_of_expirience into g
                                select g;
            return testersByYearOfEx;
        }

        public Tester FindTester(string idOfTester)
        {
            return (dal.FindTester(idOfTester));
        }

        public IEnumerable<Tester> GetTesters(Func<Tester, bool> func = null)
        {
            List<Tester> t = new List<Tester>();
            t = dal.GetTesters(func).ToList();
            return t;
        }

        public void RemoveTester(Tester t)
        {
            dal.RemoveTester(t);
        }

        public void UpdateTester(Tester t)
        {
            dal.UpdateTester(t);
        }

      
        //this func returns the precent of the student that pass with this tester
        public int PrecentPassInTester(Tester t)
        {
            int fail_count = 0;
            int pass_count = 0;
            foreach (Test ts in dal.GetTests(null))
            {
                if (ts.Tester_Id == t.Tester_Id)
                {
                    if (ts.Result_Of_Test==Enum_Success.נכשל)
                        fail_count++;
                    if (ts.Result_Of_Test==Enum_Success.עבר)
                        pass_count++;
                }
            }
            if ((fail_count + pass_count) == 0)
                return 0;
            return (pass_count / (fail_count + pass_count)*100);
        }

        //this func returned the tester Which passed the most students precent
        public Tester MaxPassPrecent()
        {

            int max_precent = 0;
            Tester temp = null;

            foreach (Tester t in dal.GetTesters(null))
            {
                if (PrecentPassInTester(t) > max_precent)
                {
                    max_precent = PrecentPassInTester(t);
                    temp = t;
                }
            }
            return temp;
        }


        //this func returned the tester Which passed the less students precent
        public Tester MinPassPrecent()
        {
            int min_precent = 0;
            Tester temp = null;

            foreach (Tester t in dal.GetTesters(null))
            {
                if (PrecentPassInTester(t) < min_precent)
                {
                    min_precent = PrecentPassInTester(t);
                    temp = t;
                }
            }
            return temp;
        }


        #endregion

        #region Tests Funcs

        //this func add test if uts answer the conditions
        public void AddTest(Test newTestToAdd)
        {
            newTestToAdd.Date_Of_Test = newTestToAdd.Date_Of_Test.AddHours(newTestToAdd.Time_Of_Test);
            //check that both id's of tester & trainee exist - if not - throw Exception 
            Trainee traineeTested = FindTrainee(newTestToAdd.Trainee_Id);
            if (traineeTested == null)
                throw new Exception("תעודת הזהות של הלמיד אינה נמצאה");
            //check that the date is in the feuter
            if (DateTime.Now > newTestToAdd.Date_Of_Test)
                throw new Exception("תאריך המבחן שבחרת עבר! נא לקבוע תאריך אחר");

            if(DateTime.Now.AddMonths(3) < newTestToAdd.Date_Of_Test)
                throw new Exception("לא ניתן לקבוע מבחן יותר משלושה חודשים מראש");

            foreach (Test test in GetTests(ts => ts.Trainee_Id == newTestToAdd.Trainee_Id))
                if (DateTime.Now.AddDays(-14) < newTestToAdd.Date_Of_Test && test.Type_Of_Car == newTestToAdd.Type_Of_Car
                    && test.Type_Of_GearBox == newTestToAdd.Type_Of_GearBox)
                    throw new Exception("עליך לחכות לפחות שבועיים על שתוכל לגשת למבחן נהיגה חוזר על " + newTestToAdd.Type_Of_Car);


            //segest a tester to the trainee - if find available
            List<Tester> testersAvl = GetTestersAvailable(newTestToAdd);

            int count = 0;
            if (testersAvl == null)
            {
                while (testersAvl == null)//means there is no tester available at this date
                {
                    count++;
                    newTestToAdd.Date_Of_Test = newTestToAdd.Date_Of_Test.AddDays(count);
                    testersAvl = GetTestersAvailable(newTestToAdd);

                }
                throw new Exception("לצערנו אין בוחן פנוי ל " + newTestToAdd.Type_Of_Car + newTestToAdd.Type_Of_GearBox + " בתאריך שביקשת נסה בתאריך" + newTestToAdd.Date_Of_Test.AddDays(count));
            }


            //checking that the trainee didnt already passed this kind of test
            if (alreadyPassedThisKindOfTest(FindTrainee(newTestToAdd.Trainee_Id)))
                throw new Exception("תלמיד זה כבר עבר מבחן נהיגה על " + newTestToAdd.Type_Of_Car + " עם תיבת הילוכים " + newTestToAdd.Type_Of_GearBox);
            //checking that this trainee doesnot testing in this time
            IEnumerable<Test> allTestOfTrainee = GetTests(ts => ts.Trainee_Id == traineeTested.Trainee_Id);
            newTestToAdd.Tester_Id = testersAvl[0].Tester_Id;
            Tester testerOfTheTest = FindTester(newTestToAdd.Tester_Id);
            int numOfWeek = NumOfWeekInYearForTest(newTestToAdd.Date_Of_Test);
            testerOfTheTest.Current_weekly_tests_num[numOfWeek - 1]++;
            dal.UpdateTester(testerOfTheTest);

            if (allTestOfTrainee==null)
            {
                
                dal.AddTest(newTestToAdd);
             
                return;
            }
            if (allTestOfTrainee.Count() > 0)//if he did atleast one test
                foreach (Test test in allTestOfTrainee)
                {
                    if (test.Date_Of_Test == newTestToAdd.Date_Of_Test && test.Time_Of_Test == newTestToAdd.Time_Of_Test)
                        throw new Exception("תלמיד זה כבר נבחן בתאריך ושעה הנבחרים - בחר תאריך אחר");
                    if (test.Type_Of_Car == newTestToAdd.Type_Of_Car && test.Type_Of_GearBox == newTestToAdd.Type_Of_GearBox
                       && test.Result_Of_Test == Enum_Success.NoData)
                        throw new Exception("כבר קבעת מבחן נהיגה על " + newTestToAdd.Type_Of_Car + " " + newTestToAdd.Type_Of_GearBox + "בתאריך");
                }
                     
           
            dal.AddTest(newTestToAdd);

        }


        //input: date
        //output: all the test that Are planned in that date
        public List<Test> GetTestInDay(DateTime date)
        {
            return GetTests(ts => ts.Date_Of_Test == date).ToList();
        }

        //input: month
        //output: all the test that Are planned in that month
        public List<Test> GetTestInMonth(DateTime date)
        {
            return GetTests(ts => ts.Date_Of_Test.Month == date.Month).ToList();
        }

        public Test FindTest(int idOfTest)
        {
            return (dal.FindTest(idOfTest));
        }

        public IEnumerable<Test> GetTests(Func<Test, bool> func = null)
        {
            return (dal.GetTests(func));
        }
        public void UpdateTestTrainee(Test t)
        {
            if (t.Date_Of_Test < DateTime.Now )
                throw new Exception("תאריך המבחן עבר");


            dal.UpdateTest(t);
        }

        public void UpdateTest(Test t, CriteriaOfTheTest cr)
        {
            //this func checks if some of the Criteria Of The Test are missing that the tester need to fill
            if (t.Result_Of_Test != Enum_Success.NoData)//means that this test alrady has a result
                throw new Exception("למבחן זה כבר יש תוצאה! לא ניתן לעדכן תוצאה נוספת");
            if (t.Date_Of_Test > DateTime.Now)
                throw new Exception("תאריך המבחן טרם הגיע, לא ניתן לעדכן עדיין תוצאות מבחן זה");

            t.CriteriaOfTheTest = cr;
            bool result= IsPassedThisTest(cr);
            if (result)
                t.Result_Of_Test = Enum_Success.עבר;
            else
                t.Result_Of_Test = Enum_Success.נכשל;
            dal.UpdateTest(t);
        }

        public void RemoveTest(Test t)
        {
            dal.RemoveTest(t);
        }


        public IEnumerable<IGrouping<Enum_Success, Test>> GetTestBySuccess()
        {
            var testBySuccess = from ts in GetTests()
                                group ts by ts.Result_Of_Test into g
                                select g;
            return testBySuccess;
        }
        //this func group all test by city 
        public IEnumerable<IGrouping<string, Test>> GetTestsByCity()
        {
            var testsByCity = from ts in GetTests()
                              group ts by ts.Address_Of_StartOfTest.city into g
                              select g;

            return testsByCity;
        }


        //this func group tests by their city, and contains the num of test naid in hat city
        //for excample: a member of the group contain - key: "jerusalem", int numOfTest = 4
        public IEnumerable<IGrouping<string, int>> GetNumOfTestsByCity()
        {
            var tests = from ts in GetTestsByCity()
                        group ts.Count() by ts.Key into g
                        select g;
            return tests;
        }


        //this func return the number of all the failed trinees in a year (input)
        public int CountNumFailed(int year)
        {
            return GetTests(ts => ts.Result_Of_Test == Enum_Success.נכשל)
                .Where(t => t.Date_Of_Test.Year == year).Count();
        }
        public int CountNumPass(int year)
        {
            return GetTests(ts => ts.Result_Of_Test == Enum_Success.עבר)
                .Where(t => t.Date_Of_Test.Year == year).Count();
        }

        #endregion

        # region helpFuncs

        public double GetDistance(string statAddress, string desAddress)
        {
            string origin = statAddress;
            string destination = desAddress;
            string KEY = @"n26Ph1aOy7PcZWQzmFAA6HTCRc7AcBYW";

            string url = @"https://www.mapquestapi.com/directions/v2/route" +
                         @"?key=" + KEY +
                         @"&from=" + origin +
                         @"&to=" + destination +
                         @"&outFormat=xml" +
                         @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" +
                         @"&enhancedNarrative=false&avoidTimedConditions=false";

            //request from MapQuest service the distance between the 2 addresses
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            //the response is given in an XML format
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(responsereader);

            if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
            //we have the expected answer
            {
                //display the returned distance
                XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);
                return (distInMiles * 1.609344);
                
            }

            else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402")
            //we have an answer that an error occurred, one of the addresses is not found
            {
                throw new Exception("an error occurred, one of the addresses is not found. try again.");
            }
            else //busy network or other error...
            {
                throw new Exception("We have'nt got an answer, maybe the net is busy...");
            }


        }


        /// <summary>
        ///  this func return the week number of a certain date
        /// </summary>
        /// <param name="dateOfTest"> Date of a test
        /// <returns> the week number of that test </returns>
        /// 
        public int NumOfWeekInYearForTest(DateTime dateOfTest)
        {
            //Constants
            const int JAN = 1;
            const int DEC = 12;
            const int LASTDAYOFDEC = 31;
            const int FIRSTDAYOFJAN = 1;
            const int THURSDAY = 4;
            bool thursdayFlag = false;

            //Get the day number since the beginning of the year
            int dayOfYear = dateOfTest.DayOfYear;

            //Get the first and last weekday of the year
            int startWeekDay = (int)(new DateTime(dateOfTest.Year, JAN, FIRSTDAYOFJAN)).DayOfWeek;
            int endWeekDay = (int)(new DateTime(dateOfTest.Year, DEC, LASTDAYOFDEC)).DayOfWeek;

            //Calculate the number of days in the first week
            int daysInFirstWeek = 8 - (startWeekDay);

            //Year starting and ending on a thursday will have 53 weeks
            if (startWeekDay == THURSDAY || endWeekDay == THURSDAY)
                thursdayFlag = true;


            //We begin by calculating the number of FULL weeks between
            //the year start and our date. The number is rounded up so
            //the smallest possible value is 0.
            int fullWeeks = (int)Math.Ceiling((dayOfYear - (daysInFirstWeek)) / 7.0);
            int result = fullWeeks;

            //If the first week of the year has at least four days, the
            //actual week number for our date can be incremented by one.
            if (daysInFirstWeek >= THURSDAY)
            {
                result = result + 1;
            }

            //If the week number is larger than 52 (and the year doesn't
            //start or end on a thursday), the correct week number is 1.
            if (result > 52 && !thursdayFlag)
            {
                result = 1;
            }

            //If the week number is still 0, it means that we are trying
            //to evaluate the week number for a week that belongs to the
            //previous year (since it has 3 days or less in this year).
            //We therefore execute this function recursively, using the
            //last day of the previous year.
            if (result == 0)
            {
                result = NumOfWeekInYearForTest(new DateTime(dateOfTest.Year - 1, DEC, LASTDAYOFDEC));
            }

            return result;


        }

        /// <summary>
        /// this func returns if the tester is Working on Sundays
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public bool IsWorkingSunday(Tester ts)
        {
            bool working = false;
            for (int i = 0; i < 7; i++)
                if (ts.Days_of_work[0, i] == true)
                {
                    working = true;
                    break;
                }
            return working;
        }

        public bool IsWorkingMonday(Tester ts)
        {
            bool working = false;
            for (int i = 0; i < 7; i++)
                if (ts.Days_of_work[1, i] == true)
                {
                    working = true;
                    break;
                }
            return working;
        }

        public bool IsWorkingTuesday(Tester ts)
        {
            bool working = false;
            for (int i = 0; i < 7; i++)
                if (ts.Days_of_work[2, i] == true)
                {
                    working = true;
                    break;
                }
            return working;
        }

        public bool isWorkingWednesday(Tester ts)
        {
            bool working = false;
            for (int i = 0; i < 7; i++)
                if (ts.Days_of_work[3, i] == true)
                {
                    working = true;
                    break;
                }
            return working;
        }

        public bool isWorkingThursday(Tester ts)
        {
            bool working = false;
            for (int i = 0; i < 7; i++)
                if (ts.Days_of_work[4, i] == true)
                {
                    working = true;
                    break;
                }
            return working;
        }

        /// <summary>
        /// this func returns the starting hour of the tester in sunday
        /// if the tester doesnt work on sunday the func will return 0
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public int BeginingHourOfTesterSunday(Tester ts)
        {
            if (IsWorkingSunday(ts))//if the tester is working on sunday
                for (int i = 0; i < 7; i++)//go over all the hours of work
                    if (ts.Days_of_work[0, i])
                        return i + 9;   //retrn the first hour that is true +9 
            return 0;
        }

        public int EndingHourOfTesterSunday(Tester ts)
        {
            if (IsWorkingSunday(ts))//if the tester is working on sunday
                for (int i = 6; i > 0; i--)//go over all the hours of work
                    if (ts.Days_of_work[0, i])
                        return i + 9;   //retrn the first hour that is true +9 
            return 0;
        }

        public int BeginingHourOfTesterMonday(Tester ts)
        {
            if (IsWorkingMonday(ts))//if the tester is working on sunday
                for (int i = 0; i < 7; i++)//go over all the hours of work
                    if (ts.Days_of_work[1, i])
                        return i + 9;   //retrn the first hour that is true +9 
            return 0;
        }

        public int EndingHourOfTesterMonday(Tester ts)
        {
            if (IsWorkingMonday(ts))//if the tester is working on Monday
                for (int i = 6; i > 0; i--)//go over all the hours of work
                    if (ts.Days_of_work[1, i])
                        return i + 9;   //retrn the first hour that is true +9 
            return 0;
        }

        public int BeginingHourOfTesterTuesday(Tester ts)
        {
            if (IsWorkingTuesday(ts))//if the tester is working on Tuesday
                for (int i = 0; i < 7; i++)//go over all the hours of work
                    if (ts.Days_of_work[2, i])
                        return i + 9;   //retrn the first hour that is true +9 
            return 0;
        }

        public int EndingHourOfTesterTuesday(Tester ts)
        {
            if (IsWorkingTuesday(ts))//if the tester is working on Tuesday
                for (int i = 6; i > 0; i--)//go over all the hours of work
                    if (ts.Days_of_work[2, i])

                        return i + 9;   //retrn the first hour that is true +9 
            return 0;
        }

        public int BeginingHourOfTesterWednesday(Tester ts)
        {
            if (isWorkingWednesday(ts))//if the tester is working on Wednesday
                for (int i = 0; i < 7; i++)//go over all the hours of work
                    if (ts.Days_of_work[3, i])
                        return i + 9;   //retrn the first hour that is true +9 
            return 0;
        }

        public int EndingHourOfTesterWednesday(Tester ts)
        {
            if (isWorkingWednesday(ts))//if the tester is working on Wednesday
                for (int i = 6; i > 0; i--)//go over all the hours of work
                    if (ts.Days_of_work[3, i])
                        return i + 9;   //retrn the first hour that is true +9 
            return 0;
        }

        public int BeginingHourOfTesterThursday(Tester ts)
        {
            if (isWorkingThursday(ts))//if the tester is working on Thursday
                for (int i = 0; i < 7; i++)//go over all the hours of work
                    if (ts.Days_of_work[4, i])
                        return i + 9;   //retrn the first hour that is true +9 
            return 0;
        }

        public int EndingHourOfTesterThursday(Tester ts)
        {
            if (isWorkingThursday(ts))//if the tester is working on Thursday
                for (int i = 6; i > 0; i--)//go over all the hours of work
                    if (ts.Days_of_work[4, i])
                        return i + 9;   //retrn the first hour that is true +9 
            return 0;
        }


        #endregion

    }
}
