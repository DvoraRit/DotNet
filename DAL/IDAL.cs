using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDAL
    {//in this interface we will wright only the signature of the functions

        #region signature funcs for Trainee

        void AddTrainee(Trainee t);
        void RemoveTrainee(Trainee t);
        void UpdateTrainee(Trainee t);
        Trainee FindTrainee(string idOfTrainee);
        IEnumerable<Trainee> GetTrainees(Func<Trainee, bool> func = null);
        #endregion

        #region signature funcs for Tester
        void AddTester(Tester t);
        void RemoveTester(Tester t);
        void UpdateTester(Tester t);
        Tester FindTester(string idOfTester);
        IEnumerable<Tester> GetTesters(Func<Tester, bool> func = null);


        #endregion

        #region signature funcs for Test
        void AddTest(Test t);
        void RemoveTest(Test t);
        void UpdateTest(Test t);
        Test FindTest(int idOfTest);
        IEnumerable<Test> GetTests(Func<Test, bool> func = null);
        #endregion


    }
}
