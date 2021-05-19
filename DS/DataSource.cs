using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public class DataSource
    {
        private static List<Tester> testersList = new List<Tester>();

        private static List<Trainee> traineesList = new List<Trainee>();

        private static List<Test> testsList = new List<Test>();

        //properties
        public static List<Trainee> TraineesList { get => traineesList; set => traineesList = value; }
        public static List<Test> TestsList { get => testsList; set => testsList = value; }
        public static List<Tester> TestersList { get => testersList; set => testersList = value; }

   
    }

}
