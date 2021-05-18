using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    public class DAL_imp : IDAL
    {
        #region Tester Func
        public void AddTester(Tester newTesterToAdd)
        {
            //checkind  that the newTesterToAdd is not exist already
            if (FindTester(newTesterToAdd.Tester_Id) != null)
                throw new Exception("קיים כבר בוחן עם תעודת זהות זהה");
            else DataSource.TestersList.Add(newTesterToAdd);
        }

        //this func searching the tester inthe list by id num
        public Tester FindTester(string idOfTester)
        {
            return (DataSource.TestersList.Find(h => h.Tester_Id == idOfTester));
        }

        public void RemoveTester(Tester TesterToRemove)
        {
            if (FindTester(TesterToRemove.Tester_Id) == null)
                throw new Exception("לא קיים בוחן עם תעודת זהות זו");
            else DataSource.TestersList.Remove(TesterToRemove);
        }

        public IEnumerable<Tester> GetTesters(Func<Tester, bool> func = null)
        {
            if (func == null)
            {
                var v = from item in DataSource.TestersList
                        select (new Tester(item));
                return v;
            }

            else
                return DataSource.TestersList.Where(func).AsEnumerable<Tester>();
        }

        public void UpdateTester(Tester updateTester)
        {
            //find the index of the tester in ihe testersList
            int index = DataSource.TestersList.FindIndex(ts => ts.Tester_Id == updateTester.Tester_Id);
            if (index == -1)//if the tester does not exist
                throw new Exception("לא קיים בוחן עם תעודת זהות זו");
            DataSource.TestersList[index] = updateTester;// update tester of DataSource
        }
        #endregion

        #region Trainee Funcs
        public void AddTrainee(Trainee newTraineeToAdd)
        {
            if (FindTrainee(newTraineeToAdd.Trainee_Id) != null)
                throw new Exception("קיים כבר תלמיד עם תעודת זהות זהה");
            else DataSource.TraineesList.Add(newTraineeToAdd);
        }

        public Trainee FindTrainee(string idOfTrainee)
        {
            return (DataSource.TraineesList.Find(h => h.Trainee_Id == idOfTrainee));
        }

        public IEnumerable<Trainee> GetTrainees(Func<Trainee, bool> func = null)
        {
            if (func == null)
                return DataSource.TraineesList.AsEnumerable<Trainee>();
            else
                return DataSource.TraineesList.Where(func).AsEnumerable<Trainee>();
        }

        public void RemoveTrainee(Trainee TraineeToRemove)
        {
            //this func checkes if the TraineeToRemove is not exist else its removes him
            if (FindTrainee(TraineeToRemove.Trainee_Id) == null)
                throw new Exception("לא קיים תלמיד עם תעודת זהות זו");
            else DataSource.TraineesList.Remove(TraineeToRemove);
        }

        public void UpdateTrainee(Trainee updateTrainee)
        {
            //find the index of the Trainee in ihe TraineesList
            int index = DataSource.TraineesList.FindIndex(tr => tr.Trainee_Id == updateTrainee.Trainee_Id);
            if (index == -1)//if the Trainee does not exist
                throw new Exception("לא קיים תלמיד עם תעודת זהות זו");
            DataSource.TraineesList[index] = updateTrainee;// update Trainee of DataSource
        }
        #endregion

        #region Test Funcs
        //this functions input a test/tester/trainee and add it to the List
        public void AddTest(Test newTestToAdd)
        {
            if (FindTest(newTestToAdd.Num_Of_Test) != null)//if the test already exist
                throw new Exception("this test already exist!");
            else
            {
                newTestToAdd.Num_Of_Test = ++Configuration.serial_Num_Of_Test_Id;
                DataSource.TestsList.Add(newTestToAdd);
            }
            
            
            
        }

        //this func searching the test with the id input and output the matchig test or null
        public Test FindTest(int idOfTest)
        {
            return DataSource.TestsList.Find(h => h.Num_Of_Test == idOfTest);
        }
        
        public void RemoveTest(Test t)
        {
            if (t == null)
                return;
            if (FindTest(t.Num_Of_Test) == null)
                throw new Exception("לא קיים מבחן כזה");
            DataSource.TestsList.Remove(t);
        }

        public IEnumerable<Test> GetTests(Func<Test,bool> func=null)
        {
            if (func==null)
                return DataSource.TestsList.AsEnumerable<Test>();
            else
                return DataSource.TestsList.Where(func).AsEnumerable<Test>();
        }
        
        public void UpdateTest(Test t)
        {
 
            //find the index of the test in ihe testsList
            int index = DataSource.TestsList.FindIndex(ts => ts.Num_Of_Test == ts.Num_Of_Test);
            if (index == -1)//if the test does not exist
                throw new Exception("this test does not exist");
            DataSource.TestsList[index] = t;// update test of DataSource
        }
        #endregion
    }
}
