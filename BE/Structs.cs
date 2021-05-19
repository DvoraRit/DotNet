using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public struct CriteriaOfTheTest
    {
        public Enum_Success tester_break;
        public Enum_Success tester_touch_weel;
        public Enum_Success saved_distance;
        public Enum_Success reverse_parking;
        public Enum_Success mirrors_checked;
        public Enum_Success signaled;
        public Enum_Success gave_priority;
        public Enum_Success compliance_traffic_signs;
        public Enum_Success speed;

        //public bool Tester_break { get => tester_break; set => tester_break = value; }

        public CriteriaOfTheTest(Enum_Success tb= Enum_Success.NoData, Enum_Success ttw = Enum_Success.NoData,
            Enum_Success sd= Enum_Success.NoData, Enum_Success rp = Enum_Success.NoData, 
            Enum_Success mc = Enum_Success.NoData, Enum_Success s = Enum_Success.NoData, 
            Enum_Success gp= Enum_Success.NoData, Enum_Success cts= Enum_Success.NoData,
            Enum_Success sp = Enum_Success.NoData)
        {
            tester_break =tb;
            tester_touch_weel=ttw;
            saved_distance=sd;
            reverse_parking=rp;
            mirrors_checked=mc;
            signaled=s;
            gave_priority=gp;
            compliance_traffic_signs=cts;
            speed=sp;
        }//constractor

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public struct Address
    {
        public string street;
        public int houseNum;
        public string city;
        public Address(string new_street = "", int new_houseNum = 0, string new_city = "")
        {
            street = new_street;
            houseNum = new_houseNum;
            city = new_city;
        }//constractor

        public override string ToString()
        {
            return (street + "," + houseNum + "," + city);
        }

    }


}
