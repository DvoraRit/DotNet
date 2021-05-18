using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {//in this interface we will wright only the signature of the functions

        #region signature funcs for trainee
        void AddTrainee(params Trainee[] t);
        void RemoveTrainee(Trainee t);
        void UpdateTrainee(Trainee t);
        Trainee FindTrainee(string idOfTrainee);
        #endregion

        #region signature funcs for Tester
        void AddTester( Tester t);
        void RemoveTester(Tester t);
        void UpdateTester(Tester t);
        Tester FindTester(string idOfTester);
        

        #endregion

        #region signature funcs for Test
        void AddTest(Test t);
        void UpdateTest(Test t, CriteriaOfTheTest cr);
        void UpdateTestTrainee(Test t);
        Test FindTest(int idOfTest);
        void RemoveTest(Test t);
        #endregion

        #region return lists
        IEnumerable<Trainee> GetTrainees(Func<Trainee, bool> func = null);
        IEnumerable<Test> GetTests(Func<Test, bool> func = null);
        IEnumerable<Tester> GetTesters(Func<Tester, bool> func = null);

        #endregion

        #region ourFuncs
        bool IsWorkingSunday(Tester ts);
        bool IsWorkingMonday(Tester ts);
        bool IsWorkingTuesday(Tester ts);
        bool isWorkingWednesday(Tester ts);
        bool isWorkingThursday(Tester ts);
        int BeginingHourOfTesterSunday(Tester ts);
        int BeginingHourOfTesterMonday(Tester ts);
        int BeginingHourOfTesterTuesday(Tester ts);
        int BeginingHourOfTesterWednesday(Tester ts);
        int BeginingHourOfTesterThursday(Tester ts);
        int EndingHourOfTesterSunday(Tester ts);
        int EndingHourOfTesterMonday(Tester ts);
        int EndingHourOfTesterTuesday(Tester ts);
        int EndingHourOfTesterWednesday(Tester ts);
        int EndingHourOfTesterThursday(Tester ts);
        IEnumerable<IGrouping<string, Trainee>> GetTraineesByTeacherName();
        IEnumerable<IGrouping<string, Trainee>> GetTraineesBySchoolName();
        IEnumerable<IGrouping<int, Trainee>> GetTraineeByNumOfTest();
        IEnumerable<IGrouping<string, Trainee>> GetTraineeByCity();

        IEnumerable<IGrouping<Enum_Type_Of_Car, Tester>> GetTestersByTypeOfCar();
        IEnumerable<IGrouping<Enum_Type_Of_GearBox, Tester>> GetTestersByTypeOfGear();

        IEnumerable<IGrouping<string, Test>> GetTestsByCity();
        IEnumerable<IGrouping<string, int>> GetNumOfTestsByCity();
        IEnumerable<IGrouping<Enum_Success, Test>> GetTestBySuccess();
        int CountNumFailed(int year);
        int CountNumPass(int year);
        int PrecentPassInTester(Tester t);
        IEnumerable<IGrouping<int, Tester>> GetTestersByYearOfEx();
        #endregion

    }
}
